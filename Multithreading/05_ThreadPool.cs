namespace Multithreading;

internal class _05_ThreadPool
{
	static void Main(string[] args)
	{
		ThreadPool.QueueUserWorkItem(Methode1);
		ThreadPool.QueueUserWorkItem(Methode2);
		ThreadPool.QueueUserWorkItem(Methode3);

		Thread.Sleep(500);

		//Main Thread wird vor den Hintergrundthreads fertig

		Console.ReadKey(); //Main Thread blockieren
	}

	static void Methode1(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Methode 1: {i}");
			Thread.Sleep(25);
		}
	}

	static void Methode2(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Methode 2: {i}");
			Thread.Sleep(25);
		}
	}

	static void Methode3(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Methode 3: {i}");
			Thread.Sleep(25);
		}
	}
}
