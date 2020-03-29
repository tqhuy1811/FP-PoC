using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;

namespace FunctionalProgramming
{
	public class PartialApplication
	{
		public void Test()
		{
			Func<Greeting, Name, PersonalizedGreeting> greet = (gr, name) => $"{gr},{name}";
			var names = new[] {"Tristan", "Ivan"};
			names
				.Select(n => greet("Hello", n))
				.ToList()
				
				.ForEach(v => Console.WriteLine(v));

			Func<Greeting, Func<Name, PersonalizedGreeting>> greetwith = gr => name => $"{gr}, {name}";
			
			var z = greet.Apply("Nice");
			var x = greetwith("Nice")("j"); 
			names
				.Select(n => greetwith(n))
				.ToList()
				.ForEach(Console.WriteLine);
		}
	}

	public static class PartialExt
	{
		public static Func<T2, TR> Apply<T1, T2, TR>(this Func<T1, T2, TR> f, T1 t1) => t2 => f(t1, t2);

		public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> f)
			=> t1 => t2 => f(t1, t2);
	}

	public class A
	{
		void Do(Func<string, string, int> x)
		{
			
		}

		void Test()
		{
			var x = "s";
			Do((s, s1) => Test2(s,s1,x));
		}

		int Test2(string a,
			string b,
			string c)
		{
			return 0;
		}
	}
}