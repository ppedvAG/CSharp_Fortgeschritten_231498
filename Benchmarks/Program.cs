using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System.Text;

namespace Benchmarks;

internal class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<Benchmarks>();
		//Benchmarks debuggen
		//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
	}
}

[MemoryDiagnoser(false)]
[RankColumn]
//[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Benchmarks
{
	public List<Fahrzeug> Fahrzeuge;

	[Params(10000, 50000, 100000)]
	public int Anzahl;

	[GlobalSetup]
	public void Setup()
	{
		Fahrzeuge = new();
		Random rnd = new Random();
		for (int i = 0; i < Anzahl; i++)
		{
			Fahrzeug f = new(rnd.Next(100, 500), (FahrzeugMarke) rnd.Next(0, 3));
			Fahrzeuge.Add(f);
		}
	}

	[GlobalCleanup]
	public void Cleanup()
	{
		Fahrzeuge.Clear();
		Fahrzeuge = null;
	}

	//[Benchmark]
	[IterationCount(30)]
	public void StringPlus()
	{
		string s = string.Empty;
		for (int i = 0; i < 10000; i++)
			s += i;
	}

	//[Benchmark]
	[IterationCount(30)]
	public void StringB()
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < 10000; i++)
		{
			sb.Append(i);
		}
		string s = sb.ToString();
	}

	[Benchmark]
	[IterationCount(30)]
	public void ForEach()
	{
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in Fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);
	}

	[Benchmark]
	[IterationCount(30)]
	public void StandardLinq()
	{
		List<Fahrzeug> bmwsAlt = (from f in Fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();
	}

	[Benchmark]
	[IterationCount(30)]
	public void Linq()
	{
		List<Fahrzeug> bmwsNeu = Fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
	}
}

public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke { Audi, BMW, VW }