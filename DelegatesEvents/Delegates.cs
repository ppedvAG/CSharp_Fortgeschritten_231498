namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, speichert Referenzen auf Methoden (Funktionszeiger)

	static void Main(string[] args)
	{
		Vorstellungen v = new Vorstellungen(VorstellungDE); //Erstellung von Delegate mit Initialmethode
		v("Lukas"); //Ausführen des Delegates

		v += VorstellungEN; //Methode an das Delegate anhängen
		v += VorstellungDE; //Die selbe Methode kann mehrmals angehängt werden
		v("Max"); //Methoden werden von oben nach unten ausgeführt (Erste -> Letzte)

		v -= VorstellungEN; //Methode abnehmen
		v -= VorstellungEN; //Funktion die nicht angehängt ist gibt keine Fehlermeldung
		v -= VorstellungEN;
		v("Stefan");

		v -= VorstellungDE;
		v -= VorstellungDE; //Wenn die letzte Methode abgenommen wird, ist das Delegate null
		//v("Lukas");

		if (v is not null)
			v("Lukas");

		//Schneller Null-Check (Null Propagation)
		//Wenn v null ist, wird der restliche Code übersprungen
		v?.Invoke("Max");

		foreach (Delegate dg in v.GetInvocationList()) //Delegate anschauen
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name)
	{
        Console.WriteLine($"Hallo mein Name ist {name}");
    }

	static void VorstellungEN(string name)
	{
        Console.WriteLine($"Hello my name is {name}");
    }
}