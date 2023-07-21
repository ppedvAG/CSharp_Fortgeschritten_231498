namespace DelegatesEvents;

internal class Component
{
	//Events müssen nicht EventHandler haben -> Delegate mit void wird benötigt
	public event Action<int> Progress; //int alt Parameter

	public event Action ProcessCompleted;

	public void StartProcess()
	{
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Progress?.Invoke(i); //? hier essentiell, da der Entwickler auf der anderen Seite hier kein Event anhängen muss
		}
		ProcessCompleted?.Invoke(); //? hier essentiell, da der Entwickler auf der anderen Seite hier kein Event anhängen muss
	}
}
