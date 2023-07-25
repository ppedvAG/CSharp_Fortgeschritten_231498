namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //1:1 wie bei Threads
		t.Start();

		Task.Factory.StartNew(Run); //ab .NET 4.0

		Task.Run(Run); //ab .NET 4.5

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Side Task: {i}");
		}
	}
}