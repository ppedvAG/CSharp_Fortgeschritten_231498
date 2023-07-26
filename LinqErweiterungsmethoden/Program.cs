using System.Reflection.Emit;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

        Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		ints.First(); //Gibt das erste Element zurück, Exception wenn kein Element gefunden wurde
		ints.FirstOrDefault(); //Gibt das erste Element zurück, default wenn kein Element gefunden wurde

		ints.Last(); //Gibt das letzte Element zurück, Exception wenn kein Element gefunden wurde
		ints.LastOrDefault(); //Gibt das letzte Element zurück, default wenn kein Element gefunden wurde

        //Console.WriteLine(ints.First(e => e % 50 == 0)); //Finde das erste Element, dass restlos durch 50 teilbar ist (-> Exception)
        Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0)); //Finde das erste Element, dass restlos durch 50 teilbar ist (-> 0)
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region	Linq mit Objektliste
		//Alle Fahrzeuge mit mindestens 200km/h
		fahrzeuge.Where(e => e.MaxV >= 200);

		//Alle VWs mit MaxV >= 200
		fahrzeuge.Where(e => e.MaxV >= 200 && e.Marke == FahrzeugMarke.VW);

		//Autos nach Marken sortieren
		fahrzeuge.OrderBy(e => e.Marke);
		fahrzeuge.OrderByDescending(e => e.Marke);

		//Autos nach Marke UND MaxV sortieren
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//Order- und OrderDescending bei Numerischen- und Stringlisten (ab C# 11)
		ints.Order(); //ints.OrderBy(e => e);
		ints.OrderDescending(); //ints.OrderByDescending(e => e);

		//Select: Liste umformen
		//2 Beispiele
		//Häufigste Anwendungsfall (80%): Einzelnes Feld auf der Liste entnehmen
		fahrzeuge.Select(e => e.Marke); //Liste mit nur Marken holen (FahrzeugMarke)
		fahrzeuge.Select(e => e.Marke).Distinct(); //Alle Marken eindeutig machen (-> Welche Marken haben wir in unserem Autohaus?)
		fahrzeuge.Select(e => e.MaxV); //Liste mit nur Geschwindigkeiten (int)
		fahrzeuge.Select(e => e.MaxV).Average(); //Was ist die Durchschnittsgeschwindigkeit unserer Autos?

		//2. Fall (20%): Liste transformieren in eine andere Form
		//List<Fahrzeug> -> List<string>
		fahrzeuge.Select(e => $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV}km/h fahren.");

		//Praktisches Beispiel 2
		string[] pfade = Directory.GetFiles(@"C:\Windows\System32"); //Pfade mit Pfad + Erweiterung
		List<string> p = new();
		foreach (string s in pfade)
			p.Add(Path.GetFileNameWithoutExtension(s));

		//mit Select
		List<string> p2 = Directory.GetFiles(@"C:\Windows\System32").Select(e => Path.GetFileNameWithoutExtension(e)).ToList();

        Console.WriteLine(p.SequenceEqual(p2));

		//All & Any
		//Fahren alle Fahrzeuge mindestens 200km/h?
		fahrzeuge.All(e => e.MaxV >= 200);
		if (fahrzeuge.All(e => e.MaxV >= 200))
		{

		}

		//Fährt mindestens ein Fahrzeug über 200km/h?
		fahrzeuge.Any(e => e.MaxV > 200);

		fahrzeuge.Any(); //fahrzeuge.Count > 0

		//Wieviele VWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.VW);

		//Min, Max, MinBy, MaxBy
		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit (int)
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit (Fahrzeug)

		fahrzeuge.Max(e => e.MaxV); //Die größte Geschwindigkeit (int)
		fahrzeuge.MaxBy(e => e.MaxV); //Das Fahrzeug mit der größten Geschwindigkeit (Fahrzeug)

		//Skip & Take
		//Die Top 5 schnellsten Fahrzeuge
		fahrzeuge.OrderByDescending(e => e.MaxV).Take(5);

		//Linq vereinfachen
		fahrzeuge.Select(e => e.MaxV).Average();
		fahrzeuge.Average(e => e.MaxV);

		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW).Count();
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.VW);

		fahrzeuge.Where(e => e.MaxV >= 300).First();
		fahrzeuge.First(e => e.MaxV >= 300);

		//Chunk, SelectMany
		fahrzeuge.Chunk(5); //Teilt die Liste in 5 große Arrays auf (Rest im letzten Array)
		fahrzeuge.Chunk(5).SelectMany(e => e); //Glättet eine Liste von Listen auf eine einzelne Liste

		//GroupBy: Alle Objekte in der Liste nach einem Kriterium gruppieren
		fahrzeuge.GroupBy(e => e.Marke); //Audi-Gruppe, BMW-Gruppe, VW-Gruppe

		//ToDictionary: Ermöglicht, eine Collection zu einem Dictionary zu konvertieren. 2 Funcs: KeySelector, ValueSelector
		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList());
		//dict[FahrzeugMarke.BMW]

		//Was ist das schnellste Fahrzeug pro Marke?
		Dictionary<FahrzeugMarke, Fahrzeug> schnellstes = fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.MaxBy(e => e.MaxV)!); //!: Explizit den Compiler anweisen, das dieses Objekt nicht null ist

		//Aggregate: Funktion auf jedes Element der Liste anwenden, hat einen Aggregator dabei, der bei jedem Element beschrieben werden kann
		//Der Aggregator ist der Rückgabewert der Funktion
		string output = fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n");
        Console.WriteLine(output);

		IEnumerable<string> outputList = fahrzeuge.Select(e => $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV}km/h fahren.");
		foreach (string s in outputList)
            Console.WriteLine(s);

        Console.WriteLine(string.Join('\n', outputList));
		#endregion

		#region Erweiterungsmethoden
		int y = 389;
		y.Quersumme();
        Console.WriteLine(328975.Quersumme());

		fahrzeuge.Shuffle();
		dict.Shuffle();
		new int[] { 1, 2, 3, 4, 5 }.Shuffle();
        #endregion
    }
}

public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke { Audi, BMW, VW }