namespace FunctionalProgramming
{
	public class Error
	{
		public virtual string Message { get; }
	}

	public class SomeError : Error
	{
		public override string Message => "Some Error Here";
	}

	public static class ErrorFac
	{
		public static SomeError SomeError => new SomeError();
	}
}