using System;

namespace Daily
{

    enum RetType : byte
    {
        Login = 1,
        Register = 2,
        Match = 3,
        Ready = 4,
        Start = 5,
        Matched = 6,
        Error = 255
    }

    [Serializable]
    public struct StartRet
    {
        public string ip;
        public int port;
    }

    [Serializable]
    public struct ReadyRet
    {
        public string error;
    }

    [Serializable]
    public struct MatchedRet
    {
        public string msg;
    }

    [Serializable]
    public struct MatchRet
    {
        public int roomId;
        public int clientId;
    }

    [Serializable]
    public struct LoginRet
    {
        public string type;
        public string msg;
        public bool data;
    }

}