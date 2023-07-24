using System.Collections.Concurrent;
using System.Net.Mime;

namespace Multithreading;

internal class _08_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<string> list = new();
		list.Add("one");
		list.Add("two");

		ConcurrentDictionary<string, int> dict = new();
		dict.TryAdd("one", 1);
		dict.TryAdd("two", 2);
	}
}
