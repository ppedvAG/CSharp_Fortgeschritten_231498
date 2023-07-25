using System.Diagnostics;

namespace Multitasking;

internal class _06_ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
		for (int i = 0; i < durchgänge.Length; i++)
		{
			int d = durchgänge[i];

			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Durchgänge: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Durchgänge: {d}, {sw2.ElapsedMilliseconds}ms");
		}

		/*
			For Durchgänge: 1000, 4ms
			ParallelFor Durchgänge: 1000, 41ms
			For Durchgänge: 10000, 2ms
			ParallelFor Durchgänge: 10000, 1ms
			For Durchgänge: 50000, 20ms
			ParallelFor Durchgänge: 50000, 3ms
			For Durchgänge: 100000, 28ms
			ParallelFor Durchgänge: 100000, 20ms
			For Durchgänge: 250000, 68ms
			ParallelFor Durchgänge: 250000, 55ms
			For Durchgänge: 500000, 104ms
			ParallelFor Durchgänge: 500000, 37ms
			For Durchgänge: 1000000, 190ms
			ParallelFor Durchgänge: 1000000, 100ms
			For Durchgänge: 5000000, 1927ms
			ParallelFor Durchgänge: 5000000, 510ms
			For Durchgänge: 10000000, 2175ms
			ParallelFor Durchgänge: 10000000, 562ms
			For Durchgänge: 100000000, 18826ms
			ParallelFor Durchgänge: 100000000, 8606ms
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i => erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}
