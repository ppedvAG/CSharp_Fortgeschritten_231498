internal class Program
{
	private static void Main(string[] args)
	{
		List<int> intList = new(); //Generics: T wird nach unten übernommen (hier T = int)
		intList.Add(1); //T wird durch int ersetzt

		List<string> strings = new List<string>();
		strings.Add("a"); //T wird durch string ersetzt

		Dictionary<string, int> dict = new(); //Klasse mit 2 Generics: TKey -> string, TValue -> int
		dict.Add("1", 1);
	}
}

public class DataStore<T> : IProgress<T>, IEquatable<int> //T bei Vererbung, kann auch fixe Typen beinhalten
{
	private T[] _data; //T bei einem Feld

	public List<T> Data => _data.ToList(); //Generic an Generic weitergeben

	public void Add(T item, int index) //T als Parameter
	{
		_data[index] = item;
	}

	public T Get(int index)
	{
		if (index < 0 || index > _data.Length)
			return default; //default: Standardwert von T (int: 0, string: null, bool: false, ...)
		return _data[index];
	}

	public void Report(T value)
	{
		throw new NotImplementedException();
	}

	public bool Equals(int other)
	{
		throw new NotImplementedException();
	}

	public void PrintType<MyType>()
	{
        Console.WriteLine(default(MyType)); //default: Standardwert von T (int: 0, string: null, bool: false, ...)
		Console.WriteLine(typeof(MyType));
        Console.WriteLine(nameof(MyType)); //String aus einem Typen generieren ("int", "string", ...)

		//Typvergleiche mit Generics
		//if (MyType is int) { } //Nicht möglich

		if (typeof(MyType) == typeof(int))
		{
			//Möglich
		}

		object o = null;
		Convert.ChangeType(o, typeof(MyType)); //Cast mittels Funktion (bei Generics notwendig)
    }
}