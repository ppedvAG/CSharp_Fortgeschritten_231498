namespace DelegatesEvents;

internal class ActionFunc
{
	static List<int> testList = new();

	static void Main(string[] args)
	{
		//Action, Func, Predicate: Vordefinierte Delegates die an verschiedenen Stellen in C# internem Code eingebaut sind
		//Essentiell für die Fortgeschrittene Programmierung
		//Können alles was in dem vorherigen Teil vorkommt

		//Action: Delegate mit Rückgabetyp void und bis zu 16 Parametern
		Action<int, int> action = Addiere;
		action(4, 5);
		action?.Invoke(4, 9);

		DoAction(3, 4, Addiere);
		DoAction(3, 4, Subtrahiere);
		DoAction(5, 6, action);

		testList.ForEach(Quadriere);
		void Quadriere(int x) => Console.WriteLine(x * x);

		//Func: Methode mit Rückgabetyp (T) und bis zu 16 Parametern
		Func<int, int, double> func = Multipliziere;
		double d = func(4, 7); //Ergebnis der Func in eine Variable schreiben
		double? d2 = func?.Invoke(6, 7); //double?: Nullable Double, funktioniert bei allen Typen
		double d3 = func?.Invoke(6, 7) ?? double.NaN;

		DoFunc(4, 5, Multipliziere);
		DoFunc(3, 6, Dividiere);
		DoFunc(8, 2, func);

		//Anonyme Methoden: Funktionen für den einmaligen Gebrauch, ohne sie anlegen zu müssen
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anwenden von Anonymen Funktionen
		testList.ForEach(e => Console.WriteLine(e)); //Durch Action wird hier ein Ausdruck benötigt, der void zurückgibt
		testList.Where(e => e % 2 == 0); //Durch Func wird hier ein Ausdruck benötigt, der bool zurückgibt und einen Parameter hat (e)
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine(x + y);

	static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region Func
	static double Multipliziere(int x, int y) => x * y;

	static double Dividiere(int x, int y) => (double) x / y;

	static double DoFunc(int x, int y, Func<int, int, double> func) => func?.Invoke(x, y) ?? double.NaN;
	#endregion
}
