using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Progress.Value++;
		} //UI Updates finden am Main Thread statt, Main Thread wird blockiert
	}

	private void Button_Click1(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			Dispatcher.Invoke(() => Progress.Value = 0); //UI Updates sind nicht erlaubt von Side Threads/Tasks
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => Progress.Value++); //Dispatcher.Invoke(...): Führe den gegebenen Code auf dem UI Thread aus
			}
		});
	}

	private async void Button_Click2(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Progress.Value++;
		}
	}

	private async void Button_Click3(object sender, RoutedEventArgs e)
	{
		using HttpClient client = new HttpClient();
		Task<HttpResponseMessage> responseTask = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt");
		Button1.IsEnabled = false;
		TB.Text = "Text wird geladen...";
		HttpResponseMessage resp = await responseTask;
		if (resp.IsSuccessStatusCode)
		{
			Task<string> textTask = resp.Content.ReadAsStringAsync();
			TB.Text = "Text wird ausgelesen...";
			await Task.Delay(250);
			string text = await textTask;
			TB.Text = text;
			Button1.IsEnabled = true;
		}
	}

	private async void Button_Click4(object sender, RoutedEventArgs e)
	{
		ConcurrentDictionary<int, string> texte = new();
		for (int i = 0; i < 100; i++)
		{
			StringBuilder sb = new();
			for (int j = 0; j < 1000000; j++)
				sb.Append(j);
			texte[i] = sb.ToString();
		}

		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		Stopwatch sw = Stopwatch.StartNew();
		//Normale foreach
		//foreach (KeyValuePair<int, string> kv in texte)
		//{
		//	await File.WriteAllTextAsync(Path.Combine(folderPath, $"Test{kv.Key}.txt"), kv.Value);
		//	TB.Text += $"File geschrieben: {kv.Key}\n";
		//}
		//TB.Text += $"Normal: {sw.ElapsedMilliseconds}\n";

		//Parallel ForEach
		sw.Restart();
		await Parallel.ForEachAsync(texte, (kv, ct) =>
		{
			File.WriteAllText(Path.Combine(folderPath, $"Test{kv.Key}.txt"), kv.Value); //Hier kein await notwendig, da die Tasks die hier erstellt werden automatisch asynchron sind
			Dispatcher.Invoke(() => TB.Text += $"File geschrieben: {kv.Key}\n"); //Dispatcher hier wieder notwendig, da wir hier in Side Tasks sind
			return ValueTask.CompletedTask;
		});
		TB.Text += $"Parallel: {sw.ElapsedMilliseconds}\n";

		//Liste von Tasks statt ForEachAsync
		sw.Restart();
		List<Task> taskList = new();
		foreach (KeyValuePair<int, string> kv in texte)
		{
			taskList.Add(Task.Run(() =>
			{
				File.WriteAllText(Path.Combine(folderPath, $"Test{kv.Key}.txt"), kv.Value); //Hier kein await notwendig, da die Tasks die hier erstellt werden automatisch asynchron sind
				Dispatcher.Invoke(() => TB.Text += $"File geschrieben: {kv.Key}\n"); //Dispatcher hier wieder notwendig, da wir hier in Side Tasks sind
			}));
		}
		await Task.WhenAll(taskList);
		TB.Text += $"Task List: {sw.ElapsedMilliseconds}\n";
	}
}