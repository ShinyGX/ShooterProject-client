public class Gamedata : Singleton<Gamedata>
{
    public string Username
    {
        get;set;
    }

    public int ClinetId
    {
        get;set;
    }

    public int RoomId
    {
        get;set;
    }

    public string Ip
    {
        get;set;
    }

    public int Port
    {
        get;set;
    }
}
