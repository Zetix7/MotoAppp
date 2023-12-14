namespace MotoApp;

public class App : IApp
{
    private readonly IUserCommunication _userComunication;

    public App(IUserCommunication userComunication)
    {
        _userComunication = userComunication;
    }

    public void Run()
    {
        _userComunication.Run();
    }
}
