namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Run);
		t.Start();
		Console.WriteLine(t.Result); //t.Result blockiert den Main Thread, 2 Lösungen: ContinueWith, await

		for (int i = 0; i < 1000; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Task t2 = Task.Run(() => { }); //Task mit anonymer Methode

		Task t3 = Task.Run(() =>
		{
            Console.WriteLine("Mehrzeilige");
            Console.WriteLine("anonyme");
            Console.WriteLine("Methode");
        });

		t2.Wait(); //Warte hier bis t2 fertig ist, führe den Code danach erst aus wenn der Task fertig ist
		Task.WaitAll(t, t2, t3); //Warte bis alle gegebenen Tasks fertig sind
		Task.WaitAny(t, t2, t3); //Warte bis einer der gegebenen Tasks fertig ist, gibt den Index des Tasks der zuerst fertig geworden ist zurück
	}

	static int Run()
	{
		int summe = 0;
		for (int i = 0; i < 1000; i++)
		{
			summe += i;
			Thread.Sleep(1);
		}
		return summe;
	}
}
