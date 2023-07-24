namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		try
		{
			Thread t = new Thread(Run);
			t.Start();

			Thread.Sleep(2000);

			t.Interrupt();
		}
		catch (ThreadInterruptedException e)
		{
			//Exception muss im Thread selbst gefangen werden
        }
	}

	static void Run()
	{
		try
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(100);
				Console.WriteLine($"Side Thread: {i}");
			}
		}
		catch (ThreadInterruptedException e)
		{
			Console.WriteLine($"Thread beendet");
		}
	}
}
