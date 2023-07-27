using PluginBase;

namespace PluginCalculator;

//[ReflectionVisible("")]
public class Calculator : IPlugin
{
	//[ReflectionVisible("")]
	public string Name => "Calculator Plugin";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Addition")]
	public double Addiere(int x, int y) => x + y;

	[ReflectionVisible("Subtraktion")]
	public double Subtrahiere(int x, int y) => x - y;
}