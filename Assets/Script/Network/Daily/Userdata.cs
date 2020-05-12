using System;


[Serializable]
public struct Userdata{

    public string type;
    public string username;
    public string pwd;

}

[Serializable]
public struct Matchdata
{
    public string type;
    public string name;
}

[Serializable]
public struct Readydate
{
    public string type;
    public int roomId;
    public int clientId;
}