namespace Multithreading;

internal class _07_Mutex
{
	static void Main(string[] args)
	{
		Mutex m = null;
		Mutex.TryOpenExisting("Threading", out m);
		if (m != null)
		{
            Console.WriteLine("Applikation ist bereits gestartet");
			//Environment.Exit(0);
        }
		else
		{
			m = new Mutex(true, "Threading");
            Console.WriteLine("Mutex geöffnet");
        }

		Console.ReadKey();
		m?.ReleaseMutex();
	}
}
