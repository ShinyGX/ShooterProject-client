using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(GameCommandDispatch))]
public class DailyNetworkClient : MonoBehaviour
{

    private static DailyNetworkHandler client;
    private GameCommandDispatch sender;

    // Start is called before the first frame update
    void Start()
    {
        sender = GetComponent<GameCommandDispatch>();

        client = new DailyNetworkHandler(sender);
        client.Connect("127.0.0.1", 4396);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while(client.MessageQue.Count > 0)
        {
            client.ParseMessage(client.MessageQue.Dequeue());
        }
    }

    public static void Ready(int roomId,int clientId)
    {
        Readydate data = new Readydate
        {
            type = "ready",
            roomId = roomId,
            clientId = clientId
        };

        string json = JsonUtility.ToJson(data);
        client.SendMessage(Encoding.UTF8.GetBytes(json));
    }

    public static void Match(string name)
    {
        Matchdata data = new Matchdata
        {
            type = "match",
            name = name
        };

        string json = JsonUtility.ToJson(data);
        client.SendMessage(Encoding.UTF8.GetBytes(json));
    }

    public static void Login(string username,string pwd)
    {
        if (client == null)
            return;

        Userdata data = new Userdata
        {
            type = "login",
            username = username,
            pwd = pwd
        };


        string json = JsonUtility.ToJson(data);
        client.SendMessage(Encoding.UTF8.GetBytes(json));
        
    }

    public static void Register(string username,string pwd)
    {
        if (client == null)
            return;

        Userdata data = new Userdata
        {
            type = "register",
            username = username,
            pwd = pwd
        };
       
        string json = JsonUtility.ToJson(data);
        client.SendMessage(Encoding.UTF8.GetBytes(json));
    }

}
