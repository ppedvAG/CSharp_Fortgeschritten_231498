namespace DelegatesEvents;

internal class Events
{
	//Event: Statischer Punkt (muss nicht static sein), an den Methoden angehängt werden können
	//Zweigeteilte Entwicklung
	//Entwickler der Komponente, dieser stellt das Event bereit und ruft es auf
	//Benutzer der Komponente, dieser hängt an das Event Funktionen an und kann dadurch die Funktionsweise der Komponente beeinflussen
	//Kann nicht instanziert werden
	static event EventHandler TestEvent;

	static event EventHandler<TestEventArgs> ArgsEvent;

	static event EventHandler<int> IntEvent
	{
		add
		{
            Console.WriteLine($"Methode angehängt {value.Method.Name}");
        }
		remove
		{
			Console.WriteLine($"Methode abgenommen {value.Method.Name}");
		}
	}

	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent;
		TestEvent?.Invoke(null, EventArgs.Empty); //Normalerweise würde beim Sender immer this stehen

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs() { Status = "Ausgeführt" });

		IntEvent += Events_IntEvent;
		//IntEvent(null, 10);
	}

	private static void Events_IntEvent(object? sender, int e)
	{
        Console.WriteLine("Die Zahl ist: " + e);
    }

	private static void Events_ArgsEvent(object? sender, TestEventArgs e)
	{
        Console.WriteLine($"Status: {e.Status}");
    }

	private static void Events_TestEvent(object? sender, EventArgs e)
	{
        Console.WriteLine("TestEvent wurde ausgeführt");
    }
}

internal class TestEventArgs : EventArgs
{
	public string Status { get; set; }
}