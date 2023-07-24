namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start(200);
		//t.Start(new ThreadData(200, "Test", true));

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread {i}");
		}
	}

	static void Run(object obj)
	{
		if (obj is int x)
		{
			for (int i = 0; i < x; i++)
			{
                Console.WriteLine($"Side Thread {i}");
            }
		}
	}
}

public record ThreadData(int x, string y, bool z);