namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource();
		CancellationToken ct = cts.Token;

		Task t = new Task(Run, ct);
		t.Start();

		Thread.Sleep(500);

		cts.Cancel();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Task wirft Exception ist aber nicht sichtbar

				Console.WriteLine($"Side Task: {i}");
				Thread.Sleep(25);
			}
		}
	}
}
