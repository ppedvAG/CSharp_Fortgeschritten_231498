using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//SystemJson();

		//NewtonsoftJson();

		//XML();

		//Binary();

		//CSV
		//TextFieldParser tfp = new(filePath);
	}

	static void SystemJson()
	{
		//string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		//string filePath = Path.Combine(desktop, "Test.txt");

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new Fahrzeug(9, 250, FahrzeugMarke.VW),
		//	new PKW(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		//JsonSerializerOptions options = new JsonSerializerOptions(); //Einstellungen beim (De-)Serialisieren
		//options.WriteIndented = true; //Json schön schreiben
		//options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Zirkelbezüge ignorieren

		////WICHTIG: Optionen übergeben
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson, options);

		//////////////////////////////////////////////////////////////////

		//JsonDocument doc = JsonDocument.Parse(readJson); //Generisches Dokument anlegen ohne separate Klassen
		//foreach (JsonElement je in doc.RootElement.EnumerateArray()) //Json Objekte iterieren
		//{
		//	Console.WriteLine(je.GetProperty("ID").GetInt32()); //Auf einzelne Felder mittels GetProperty zugreifen
		//	Console.WriteLine(je.GetProperty("MaxV").GetInt32()); //Am Ende das Objekt konvertieren mittels Get<Typ>()
		//	Console.WriteLine((FahrzeugMarke) je.GetProperty("Marke").GetInt32());
		//	Console.WriteLine();
		//}
	}

	static void NewtonsoftJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerSettings options = new JsonSerializerSettings(); //Einstellungen beim (De-)Serialisieren
		//options.Formatting = Formatting.Indented; //Json schön schreiben
		options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Zirkelbezüge ignorieren
		options.TypeNameHandling = TypeNameHandling.Objects; //Vererbung beachten

		//WICHTIG: Settings übergeben
		string json = JsonConvert.SerializeObject(fahrzeuge, options);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson, options);

		////////////////////////////////////////////////////////////////

		JToken doc = JToken.Parse(readJson); //Generisches Dokument anlegen ohne separate Klassen
		foreach (JToken je in doc) //Json Objekte iterieren
		{
			Console.WriteLine(je["ID"].Value<int>()); //Auf einzelne Felder mittels GetProperty zugreifen
			Console.WriteLine(je["MaxV"].Value<int>()); //Am Ende das Objekt konvertieren mittels Get<Typ>()
			Console.WriteLine((FahrzeugMarke) je["Marke"].Value<int>());
			Console.WriteLine();
		}
	}

	static void XML()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
			xml.Serialize(sw, fahrzeuge);

		using (StreamReader sr = new StreamReader(filePath))
		{
			List<Fahrzeug> readXml = xml.Deserialize(sr) as List<Fahrzeug>;
		}

		/////////////////////////////////////////////////////////

		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		foreach (XmlNode node in doc.DocumentElement.ChildNodes) //Header überspringen
		{
			Console.WriteLine(node["ID"].InnerText);
			Console.WriteLine(node["MaxV"].InnerText);
			Console.WriteLine(node["Marke"].InnerText);
			Console.WriteLine();
		}
	}

	static void Binary()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		BinaryFormatter xml = new BinaryFormatter();
		using (StreamWriter sw = new StreamWriter(filePath))
			xml.Serialize(sw.BaseStream, fahrzeuge);

		using (StreamReader sr = new StreamReader(filePath))
		{
			List<Fahrzeug> readXml = xml.Deserialize(sr.BaseStream) as List<Fahrzeug>;
		}
	}
}

//Vererbung bei System.Json
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]

[Serializable]
public class Fahrzeug
{
	//System.Json Attribute
	//[JsonIgnore]
	//[JsonPropertyName("Identifier")]
	public int ID { get; set; }

	//Newtonsoft.Json Attribute
	//[JsonIgnore]
	//[JsonProperty("Maximalgeschwindigkeit")]
	public int MaxV { get; set; }

	//[XmlIgnore]
	//[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

[Serializable]
public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }