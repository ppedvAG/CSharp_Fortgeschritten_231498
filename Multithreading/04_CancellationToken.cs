namespace Multithreading;

internal class _04_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Sender, erzeugt die Tokens und sendet das Cancel Signal an die Tokens, wenn notwendig
		CancellationToken ct = cts.Token; //Empfänger, wird kopiert wenn mehrmals angegriffen

		Thread t = new Thread(Run);
		t.Start(ct);

		Thread.Sleep(500);

		cts.Cancel();
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					ct.ThrowIfCancellationRequested(); //Cancel Anfrage von der Source verarbeiten
					Thread.Sleep(100);
					Console.WriteLine($"Side Thread: {i}");
				}
			}
		}
		catch (OperationCanceledException e)
		{
            Console.WriteLine("Thread abgebrochen");
        }
	}
}
