using System;
using System.Data;
using System.Linq;

namespace FunctionalProgramming
{
	public class HigherOrderFunction
	{
		public class Cache<T> where T : class
		{
			public T Get(Guid id) => default;
			// higher order function use case in caching
			public T Get(Guid id,
				Func<T> onMiss) => Get(id) ?? onMiss();
		}

	

		public static class TestAdapter
		{
			static void Test()
			{
				Func<int,int,int> divide = (x, y) => x / y;
				// adapt function to another function
				var divideSwapped = divide.SwapArgs();
				divideSwapped(2, 5);

			}
		}

		public static class FunctionFactory
		{
			public static void Factory()
			{
				// create a function
				Func<int, bool> isMod(int n) => i => i % n == 0;
				
				// that is input of another function
				Enumerable.Range(1, 2).Where(isMod(2));
				Enumerable.Range(1, 2).Where(isMod(3));
			}
		}

		public static class AvoidDuplication
		{
			public static TR Connect<TR>(string connectionString,
				Func<IDbConnection, TR> f)
			{
				using var conn = new SqlConnection(connectionString);
				return f(conn);
			}
			
			public static TR ConnectExtreme<TR>(string connStr, Func<IDbConnection, TR> f)
				=> F.Using(new SqlConnection(connStr), conn => { conn.Open(); return f(conn); });

			public static void Log(string message) => Connect("", c => c.BeginTransaction()); // or do whatever you want
		}
		
		public static class F
		{
			public static TR Using<TDisp, TR>(TDisp disposable
				, Func<TDisp, TR> f) where TDisp : IDisposable
			{
				using (disposable) return f(disposable);
			}
		}
	}

	public class SqlConnection : IDisposable, IDbConnection
	{
		public SqlConnection(string connectionString)
		{
		}

		public void Dispose()
		{
		}

		public IDbTransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			throw new NotImplementedException();
		}

		public void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public IDbCommand CreateCommand()
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
			throw new NotImplementedException();
		}

		public string ConnectionString { get; set; }
		public int ConnectionTimeout { get; }
		public string Database { get; }
		public ConnectionState State { get; }
	}

	public static class FunctionAdapter
	{
		public static Func<T2, T1, R> SwapArgs<T1, T2, R>(this Func<T1, T2, R> f) => (t2, t1) => f(t1, t2);
	}
	
	public static class Exercise
	{
		static Func<T1, bool> Negate<T1>(Predicate<T1> predicate)
		{
			return (t) => !predicate(t);
		}
	

		static void Test()
		{
			var result = Negate<int>(x => 2 == 2);
		}
	}
}