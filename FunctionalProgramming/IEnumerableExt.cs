using System;
using System.Collections.Generic;

namespace FunctionalProgramming
{
	public static class IEnumerableExt
	{
		public static IEnumerable<TR> Bind<T, TR>
			(this IEnumerable<T> ts, Func<T, IEnumerable<TR>> f)
		{
			foreach (T t in ts)
			foreach (TR r in f(t))
				yield return r;
		}

		public static IEnumerable<TResult> Bind<T, TResult>(
			this IEnumerable<T> list,
			Func<T, Option<TResult>> f)
			=> list.Bind<T, TResult>(t => f(t).AsEnumerable());

		public static IEnumerable<TR> Bind<T, TR>(this Option<T> opt,
			Func<T, IEnumerable<TR>> f)
			=> opt.AsEnumerable().Bind(f);
	}
}