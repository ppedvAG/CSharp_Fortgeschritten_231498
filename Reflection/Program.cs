using System.Reflection;
using System.Runtime.InteropServices;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		Program p = new Program();
		Type pt = p.GetType(); //Typ aus Objekt entnehmen mittels GetType()
		Type t = typeof(Program); //Typ holen durch typeof(<Typname>)

		object o = Activator.CreateInstance(pt); //Objekt erzeugen über einen Typen

		Convert.ChangeType(o, t); //Typ von einem Objekt ändern ohne Cast (indirekter Cast)

		//////////////////////////////////////

		pt.GetMethods(); //Alle Methoden des Typs finden + alle Informationen über alle Methoden
		pt.GetMethod("Test").Invoke(o, null); //Funktion indirekt aufrufen

		pt.GetProperty("Test2").SetValue(o, "Zwei Test");
        Console.WriteLine(pt.GetProperty("Test2").GetValue(o));

		//////////////////////////////////////
		
		Assembly a = Assembly.GetExecutingAssembly(); //Das derzeitige Assembly

		//Externe DLL laden
		Assembly de = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_07_20\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");

		Type compType = de.GetType("DelegatesEvents.Component");

		object comp = Activator.CreateInstance(compType);
		compType.GetEvent("Progress").AddEventHandler(comp, (int i) => Console.WriteLine($"Fortschritt: {i}"));
		compType.GetEvent("ProcessCompleted").AddEventHandler(comp, () => Console.WriteLine($"Prozess fertig"));
		compType.GetMethod("StartProcess").Invoke(comp, null);

		//compType.GetProperties().Aggregate(string.Empty, (agg, prop) => agg + prop.Name + "\n");
	}

	public string Test2 { get; set; }

	public void Test()
	{
        Console.WriteLine("Ein Test");
    }

	//[DllImport("win32.dll")]
	//static extern bool Test1(IntPtr x);
}