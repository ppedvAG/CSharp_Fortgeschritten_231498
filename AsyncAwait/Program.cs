using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Geschirr();
		//Kaffee();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //Synchron, 7s

		//sw.Restart();
		//Task t1 = Task.Run(Toast);
		////Task t2 = Task.Run(Geschirr); //ContinueWith benutzen
		//Task t2 = Task.Run(Geschirr).ContinueWith(t => Kaffee()); //Verkettung ermöglichen
		//Task.WaitAll(t1, t2);
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //Parallel aber WaitAll, 4s

		//async Methoden
		//Wenn eine async Methode gestartet wird, wird diese als Task gestartet
		//Innerhalb von async Methoden kann await verwendet werden
		//async void: Die Methode kann selbst await benutzen, kann aber nicht awaited werden
		//async Task: Die Methode kann selbst await benutzen, kann aber awaited werden. Braucht keinen Rückgabewert
		//async Task<T>: Kann selbst await benutzen und awaited werden. Hat einen Rückgabewert

		//await Keyword
		//await: Warte bis der gegebene Task fertig ist
		//await kann auch ein Ergebnis zurückgeben, wenn der Task ein Ergebnis hat

		//sw.Restart();
		//ToastAsync();
		//GeschirrAsync();
		//KaffeeAsync();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //35ms, Tasks wurden abgebrochen

		//sw.Restart();
		//Task t1 = ToastAsync(); //Task wird automatisch gestartet
		//Task t2 = GeschirrAsync();
		//await t2; //Warte auf t2
		//Task t3 = KaffeeAsync();
		//await t3; //Warte auf den Rest
		//await t1; //Warte auf den Rest
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s, Parallel

		sw.Restart();
		Task<Toast> t1 = ToastObjectAsync(); //Starte den Toast Task
		Task<Tasse> t2 = GeschirrObjectAsync(); //Starte den Geschirr Task
		Tasse tasse = await t2; //Das Ergebnis des Tasks wird durch await abgerufen (-> t2.Result)
		Task<Kaffee> t3 = KaffeeObjectAsync(tasse); //Starte den Kaffee Task
		Kaffee kaffee = await t3; //Hier sollten die awaits nach der Länge der Tasks sortiert werden (längster Task am Ende)
		Toast toast = await t1; //Wenn die Länge nicht deterministisch ist, sollte geschätzt werden
		Console.WriteLine(sw.ElapsedMilliseconds); //4s, Parallel

		//Task<Toast> t1 = ToastObjectAsync();
		//Tasse tasse = await GeschirrObjectAsync();
		//Kaffee kaffee = await KaffeeObjectAsync(tasse);
		////Kaffee kaffee = await KaffeeObjectAsync(await GeschirrObjectAsync());
		//Toast toast = await t1;

		//WhenAll, WhenAny: Warte auf mehrere Tasks (mit await)
		await Task.WhenAll(t1, t2, t3); //-> mehrere await Statements konsoldieren
		await Task.WhenAny(t1, t2, t3); //-> Warte auf den ersten Task der fertig ist
	}

	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
	}

	static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task ToastAsync()
	{
		await Task.Delay(4000); //await: Warte bis der gegebene Task fertig ist
		Console.WriteLine("Toast fertig");
	}

	static async Task GeschirrAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //await: Warte bis der gegebene Task fertig ist
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> GeschirrObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
}

public class Toast { }

public class Tasse { }

public class Kaffee { }