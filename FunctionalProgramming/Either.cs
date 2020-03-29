using System;
using System.Collections.Generic;
using FunctionalProgramming.Either;
using Unit = System.ValueTuple;
namespace FunctionalProgramming
{
	namespace Either
	{
		public static class F
		{
			public static Left<L> Left<L>(L l) => new Left<L>(l);
			public static Right<R> Right<R>(R r) => new Right<R>(r);
		}
	}
	public struct Left<TL>
	{
		internal TL Value { get; }

		internal Left(TL value)
		{
			Value = value;
		}

		public override string ToString() => $"Left({Value})";
	}


	public struct Right<R>
	{
		internal R Value { get; }
		internal Right(R value) { Value = value; }

		public override string ToString() => $"Right({Value})";

		public Right<RR> Map<L, RR>(Func<R, RR> f) => F.Right(f(Value));
		public Either<L, RR> Bind<L, RR>(Func<R, Either<L, RR>> f) => f(Value);
	}

	public class Either<L, R>
	{
		internal L Left { get; }
		internal R Right { get; }

		private bool IsRight { get; }
		private bool IsLeft => !IsRight;
		
		internal Either(L left)
		{
			IsRight = false;
			Left = left;
			Right = default(R);
		}

		internal Either(R right)
		{
			IsRight = true;
			Right = right;
			Left = default(L);
		}
		public static implicit operator Either<L, R>(L left) => new Either<L, R>(left);
		public static implicit operator Either<L, R>(R right) => new Either<L, R>(right);

		public static implicit operator Either<L, R>(Left<L> left) => new Either<L, R>(left.Value);
		public static implicit operator Either<L, R>(Right<R> right) => new Either<L, R>(right.Value);

		public TR Match<TR>(Func<L, TR> Left, Func<R, TR> Right)
			=> IsLeft ? Left(this.Left) : Right(this.Right);

		public Unit Match(Action<L> Left, Action<R> Right)
			=> Match(Left.ToFunc(), Right.ToFunc());

		public IEnumerator<R> AsEnumerable()
		{
			if (IsRight) yield return Right;
		}

		public override string ToString() => Match(l => $"Left({l})", r => $"Right({r})");
	}


	public static class EitherExt
	{
		public static Either<L, RR> Map<L, R, RR>
			(this Either<L, R> @this, Func<R, RR> f)
			=> @this.Match<Either<L, RR>>(
				l => F.Left(l),
				r => F.Right(f(r)));

		public static Either<LL, RR> Map<L, LL, R, RR>
			(this Either<L, R> @this, Func<L, LL> left, Func<R, RR> right)
			=> @this.Match<Either<LL, RR>>(
				l => F.Left(left(l)),
				r => F.Right(right(r)));

		public static Either<L, RR> Bind<L, R, RR>(this Either<L, R> @this,
			Func<R, Either<L, RR>> f)
			=> @this.Match(
				l => F.Left(l),
				r => f(r));
	}
}