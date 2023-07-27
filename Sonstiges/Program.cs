using System.Collections;
using System.Reflection;
using System.Text;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon a = new Wagon();
		Wagon b = new Wagon();
        Console.WriteLine(a == b);

		Zug z = new Zug();
		z += a;
		z += b;

		Zug z2 = new Zug();
		z2++;
		z2++;
		z2++;
		z2++;
		z += z2;

		foreach (Wagon x in z)
		{

		}

		Console.WriteLine(z[3]);
		z[3] = new Wagon();

		Console.WriteLine(z[30, "Rot"]);

		var y = new { ID = 1, Name = "Hans", Adresse = "Zuhaue" };
        Console.WriteLine(y.ID);

		var anon = z.Wagons.Select(e => new { e.AnzSitze, HC = e.GetHashCode() }); //Mehrere Felder mit Select durch den Code bewegen
		anon.Where(e => e.HC == 3257973);

		string str = "Ein Text"; //Ein Text im RAM
		string str2 = "Zwei Text"; //Zwei Text im RAM
		str += str2; //Kopie im RAM -> Ein TextZwei Text
		str	+= str2; //Kopie im RAM -> Ein TextZwei TextZwei Text
					 //Insgesamt 7 Texte

		StringBuilder sb = new StringBuilder();
		sb.Append(str);
		sb.Append(str2);
		sb.Append(str2);
        Console.WriteLine(sb.ToString()); //Strings werden zwischengespeichert in einer Liste, und am Ende zusammengebaut

		System.Timers.Timer time = new System.Timers.Timer();
		time.Interval = 1000;
		time.Start(); //Timer ist ein Task -> Main Thread aufhalten

		Console.ReadKey();
    }
}

public class Zug : IEnumerable
{
	public List<Wagon> Wagons = new();

	public IEnumerator GetEnumerator()
	{
		foreach (Wagon w in Wagons)
			yield return w; //Speichere die return Werte zwischen und gib sie am Ende der Funktion zurück

		//Selbiges wie oben nur ohne yield
		//List<Wagon> wagons = new();
		//foreach (Wagon w in Wagons)
		//	wagons.Add(w);
		//return wagons.GetEnumerator();

		//return Wagons.GetEnumerator();

	} //Hier finales return

	public Wagon this[int index]
	{
		get => Wagons[index];
		set => Wagons[index] = value;
	}

	public Wagon this[int anzSitze, string farbe]
	{
		get => Wagons.First(e => e.AnzSitze == anzSitze && e.Farbe == farbe);
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator +(Zug z, Zug z2)
	{
		z.Wagons.AddRange(z2.Wagons);
		z2.Wagons.Clear();
		return z;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;

		//foreach (PropertyInfo pi in a.GetType().GetProperties())
		//	if (pi.GetValue(a) != pi.GetValue(b))
		//		return false;
		//return true;

		//return a.GetType().GetProperties().Select(e => e.GetValue(a) == e.GetValue(b)).All(e => e);
	}

	public static bool operator != (Wagon a, Wagon b)
	{
		return !(a == b);
	}
}