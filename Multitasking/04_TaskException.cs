namespace Multitasking;

internal class _04_TaskException
{
	static void Main(string[] args)
	{
		try
		{
			Task t1 = Task.Run(Exc1);
			Task t2 = Task.Run(Exc2);
			Task t3 = Task.Run(Exc3);

			t1.Wait();
			t2.Wait();
			t3.Wait();

			//Task.WaitAll(t1, t2, t3);
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
				Console.WriteLine(e.Message);
			}
		}
	}

	static void Exc1()
	{
		Thread.Sleep(1000);
		throw new DivideByZeroException();
	}

	static void Exc2()
	{
		Thread.Sleep(2000);
		throw new StackOverflowException();
	}

	static void Exc3()
	{
		Thread.Sleep(3000);
		throw new OutOfMemoryException();
	}
}
