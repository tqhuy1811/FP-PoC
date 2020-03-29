using System;
using Unit = System.ValueTuple;

namespace FunctionalProgramming
{
	public static class ActionExt
	{
		public static Func<T, Unit> ToFunc<T>(this Action<T> action)
			=> (t) =>
			{
				action(t);
				return default;
			};
	}
}