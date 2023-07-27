using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		//Plugin laden
		//Pfade sollten aus einer Config kommen (z.B. Json oder XML)
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_07_20\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll");

		Type calcType = loaded.GetTypes().FirstOrDefault(e => e.GetInterface(typeof(IPlugin).Name) != null);

		if (calcType != null) //Plugin gefunden
		{
			object o = Activator.CreateInstance(calcType);
			IPlugin plugin = (IPlugin) o;

            Console.WriteLine($"Name: {plugin.Name}");
            Console.WriteLine($"Beschreibung: {plugin.Description}");
            Console.WriteLine($"Version: {plugin.Version}");
            Console.WriteLine($"Autor: {plugin.Author}");

			//foreach (MethodInfo mi in calcType.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null))
			//{
			//	Console.WriteLine(mi.Name);
			//}
			List<MethodInfo> methods = calcType.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null).ToList();
			for (int i = 0; i < methods.Count; i++)
			{
				ReflectionVisible attr = methods[i].GetCustomAttribute<ReflectionVisible>();
                Console.WriteLine($"{i}: {attr.Name}({methods[i].GetParameters()
					.Aggregate(string.Empty, (agg, par) => $"{agg}{par.ParameterType} {par.Name}, ").TrimEnd(',', ' ')})");
			}

			Console.Write("Gib eine Zahl ein: ");
			int auswahl = int.Parse(Console.ReadLine());
            Console.WriteLine(methods[auswahl].Invoke(o, new object[] { 2, 3 }));
		}
	}
}