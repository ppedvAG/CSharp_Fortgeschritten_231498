namespace Multithreading;

internal class _06_Lock
{
	static int Zahl = 0;

	static object LockObject = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 100; i++)
			new Thread(ZahlPlusPlus).Start();
	}

	static void ZahlPlusPlus()
	{
		for (int i = 0; i < 100; i++)
		{
			lock (LockObject) //Dieser Codeblock wird gesperrt wenn ein Thread darauf zugreift
			{
				Zahl += 1;
				Console.WriteLine(Zahl); //Irreguläre Muster
			} //Lock wird geöffnet

			Monitor.Enter(LockObject); //Dieser Codeblock wird gesperrt wenn ein Thread darauf zugreift
			Zahl += 1;
			Console.WriteLine(Zahl); //Irreguläre Muster
			Monitor.Exit(LockObject); //Lock wird geöffnet

			//Monitor und Lock haben 1:1 den selben Code
		}
    }
}
