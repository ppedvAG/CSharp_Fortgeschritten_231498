namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: Tasks verketten, wenn der erste Task fertig ist, können Folgetasks gestartet werden
		//Auf den vorherigen Task zugreifen, mittels Parameter in ContinueWith
		Task t = new Task(() => { });
		t.ContinueWith(vorherigerTask => { });
		t.ContinueWith(vorherigerTask => { }); //Mehrere Tasks anhängen, werden gleichzeitig gestartet wenn der erste Task fertig ist
		t.Start();

		Task<double> berechne = new Task<double>(() =>
		{
			Thread.Sleep(1000);
			return Math.Pow(4, 23);
		});
		berechne.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Dieser Task wird immer ausgeführt (auch bei Fehlern)
		berechne.ContinueWith(vorherigerTask => Console.WriteLine("Fehler"), TaskContinuationOptions.OnlyOnFaulted); //Führe diesen Task nur bei Fehlern aus
		berechne.ContinueWith(vorherigerTask => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion); //Führe diesen Task nur bei keinem Fehler aus
		berechne.Start();

		Console.ReadKey();
	}
}
