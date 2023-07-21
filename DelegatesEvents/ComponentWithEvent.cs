namespace DelegatesEvents;

internal class ComponentWithEvent
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.Progress += (i) => Console.WriteLine($"Fortschritt: {i}");
		comp.ProcessCompleted += () => Console.WriteLine("Fertig");
		comp.StartProcess();
	}
}
