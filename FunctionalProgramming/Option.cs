using System;
using System.Collections.Generic;
using static FunctionalProgramming.Option.F;
using Unit = System.ValueTuple;

namespace FunctionalProgramming
{
	namespace Option
	{
		public static class F
		{
			public static None None => None.Default;
			public static Option<T> Some<T>(T value) => new Some<T>(value);
		}
	}
	public struct None
	{
		internal static readonly None Default = new None();
	}

	public struct Some<T>
	{
		internal T Value { get; }

		internal Some(T value)
		{
			if(value == null) 
				throw new ArgumentException();

			Value = value;
		}
	}
	
	public struct Option<T>
	{
		private readonly bool isSome;
		private readonly T Value;

		private Option(
			T value)
		{
			isSome = true;
			Value = value;
		}

		public bool HasValue => isSome;
		
		public static implicit operator Option<T>(None _) => new Option<T>();
		
		public static implicit operator Option<T>(Some<T> some) => new Option<T>(some.Value);

		public static implicit operator Option<T>(T value) => value == null ? Option.F.None : Some(value);

		public IEnumerable<T> AsEnumerable()
		{
			if (isSome)
				yield return Value;
		}

		public TResult Match<TResult>(
			Func<TResult> none,
			Func<T, TResult> some) => isSome ? some(Value) : none();

		public TResult MatchSome<TResult>(Func<T, TResult> some) => isSome ? some(Value) : throw new ArgumentException();
	}

	public static class OptionExt
	{
		/*
		 * Map : (C<T>, (T → R)) → C<R> functor make easy
		 * C<T> is some generic container that wraps some inner value of type T
		 */
		public static Option<TResult> Map<T, TResult>(this Option<T> optT,
			Func<T, TResult> f)
			=> optT.Match(
				() => Option.F.None,
				(t) => Some(f(t)));
		
		public static Option<Unit> ForEach<T>
			(this Option<T> opt, Action<T> action)
			=> Map(opt, action.ToFunc());
		// (C<T>, (T → C<R>)) → C<R> Monad baby. Avoid Option<Option<T>> or C<C<T>>
		public static Option<TResult> Bind<T, TResult>(this Option<T> optT, Func<T, Option<TResult>> f)
			=> optT.Match(
				() => Option.F.None,
				t => f(t));
	}
}