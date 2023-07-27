namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		(string, string) tupel;

		double d = 2_359_87_2.5_423_729_347;
            Console.WriteLine(d);

		//Referenztyp
		//class
		//Klassen werden bei Zuweisung auf Variablen referenziert
		//Bei Klassen werden die Speicheradressen verglichen
		//if (p == p2) -> if (p.GetHashCode() == p.GetHashCode())
		Program p = new Program();
		Program p2 = p;
        Console.WriteLine(p.GetHashCode());
        Console.WriteLine(p2.GetHashCode());

		//Wertetyp
		//struct
		//Structs werden bei Zuweisung auf Variablen kopiert
		//Bei Structs werden die Werte verglichen
		//if (x == y) -> if (x == y)
		int x = 5;
		int y = x;
		x = 10;

		Test(p);

		string name;
		string parameterName = "Test";
		if (parameterName != null)
			name = parameterName;
		else
			throw new Exception();

		name = parameterName ?? throw new Exception();

		PrintPerson(vorname: "Test", nachname: "Test");
		PrintPerson(alter: 30, adresse: "");

		string inputName = "LUkas";
		string fixName = char.ToUpper(inputName[0]) + inputName[1..].ToLower();

		//Verbatim String (@): Escape Sequenzen werden nicht interpretiert
		string verbatim = @"\n";
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_07_17";

		//Interpolated String ($): Ermöglicht Code in einen String einzubauen
		string kombi = "Der Name ist " + name + ", x ist " + x + ", y ist " + y;
		string interpolated = $"Der Name ist {name}, x ist {x}, y ist {y}";
		string i2 = $"X ist größer als 5: {x > 5}";
		string i3 = $"Der Name fängt mit A an: {(name[0] == 'A' ? "Ja" : "Nein")}";

		Person person = new Person(1, "");
		//person.Test1(); //Test2 hier nicht sichtbar
		//ITest itest = person;
		//itest.Test2();

		Dictionary<string, int> dict = new();

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
                    Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				Console.WriteLine("Wochenende");
				break;
		}

	}

	//Strg + . Schnelloptionen aufrufen
	public static void Test(Program p) => Console.WriteLine();

	//Klasse konfigurieren über optionale Parameter, es müssen nur die Werte eingegeben werden die benötigt sind
	public static void PrintPerson(string vorname = "", string nachname = "", int alter = 0, string adresse = "")
	{
	}
}

//public class Person : ITest
//{
//	public int Id { get; set; }

//	public string Name { get; set; }

//	public Person(int id, string name)
//	{
//		Id = id;
//		Name = name;
//	}

//	public void Test1()
//	{
//		throw new NotImplementedException();
//	}
//}

public record Person([field: Obsolete] int ID, string Name)
{
	/// <summary>
	/// Leerer Konstruktor im Record muss mit this und Standardwerten erstellt werden
	/// </summary>
    public Person() : this(-1, null)
    {
            
    }

    public void Test1()
	{

	}
}

public interface ITest
{
	static readonly int Zahl = 5;

	void Test1();

	void Test2()
	{
		//Bad Practice
	}
}