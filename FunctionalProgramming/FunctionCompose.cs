using System;
using System.Collections.Generic;
using static FunctionalProgramming.ListFunctionalExt;

namespace FunctionalProgramming
{
	public static class Compose
	{
		public static Func<T, TResult2> Then<T, TResult1, TResult2>(
			this Func<T, TResult1> first,
			Func<TResult1, TResult2> second)
			=> value => second(first(value));

		public static TResult Forward<T, TResult>(this T value,
			Func<T, TResult> f) => f(value);
	}

	/// <summary>
	/// If a function does not output list, have it output the result list;
	/// if a function has more parameter besides the list, swap the parameters so that the list parameter becomes the last parameter
	/// Finally curry the function
	/// </summary>
	public static class ListFunctionalExt
	{
		public static Func<List<T>, List<T>> Add<T>(T data)
		{
			return list =>
			{
				list.Add(data);
				return list;
			};
		}

		public static List<T> Clear<T>(List<T> list)
		{
			list.Clear();
			return list;
		}

		public static Func<List<T>, List<T>> FindAll<T>(Predicate<T> match)
		{
			return list => list.FindAll(match);
		}

		public static Func<List<T>, List<T>> ForEach<T>(Action<T> action)
		{
			return list =>
			{
				list.ForEach(action);
				return list;
			};
		}

		public static Func<List<T>, List<T>> RemoveAt<T>(int index)
		{
			return list =>
			{
				list.RemoveAt(index);
				return list;
			};
		}


		public static List<T> Reverse<T>(List<T> list)
		{
			list.Reverse();
			return list;
		}
	}

	public class FunctionCompose
	{
		public void Compose()
		{
			Func<List<int>, List<int>> removeAtWithIndex = RemoveAt<int>(0);

			Func<List<int>, List<int>> findAllWithPredicate = FindAll<int>(int32 => int32 > 0);

			Func<List<int>, List<int>> reverse = Reverse;

			Func<List<int>, List<int>> forEachWithAction = ForEach<int>(int32 => Console.Write(int32));

			Func<List<int>, List<int>> clear = Clear;

			Func<List<int>, List<int>> addWithValue = Add(1);


			Func<List<int>, List<int>> forwardComposition =
				removeAtWithIndex
					.Then(findAllWithPredicate)
					.Then(reverse)
					.Then(forEachWithAction)
					.Then(clear)
					.Then(addWithValue);
			forwardComposition(new List<int>() {12, 2, 3});
		}

		public void ForwardPipe()
		{
			var result = "2".Forward(int.Parse)
				.Forward(Math.Abs)
				.Forward(Convert.ToDouble)
				.Forward(Math.Sqrt);
			
			List<int> result2 = new List<int>() {-2, -1, 0, 1, 2}
				.Forward(RemoveAt<int>(1))
				.Forward(FindAll<int>(int32 => int32 > 0))
				.Forward(Reverse)
				.Forward(ForEach<int>(int32 => Console.WriteLine(int32)))
				.Forward(Clear)
				.Forward(Add(1));
		}
	}
}