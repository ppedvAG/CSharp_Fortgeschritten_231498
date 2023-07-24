namespace Multithreading;

internal class _01_ThreadStarten
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run); //Thread erstellen mit Methodenzeiger
		t.Start(); //Thread starten

		//Ab hier parallel

		t.Join(); //Blockiert den Main Thread bis der Side Thread fertig ist (ab hier sequenziell)

		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Main Thread: {i}");
        }
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Side Thread: {i}");
        }
	}
}