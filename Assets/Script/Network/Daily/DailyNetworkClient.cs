using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DailyNetworkClient : MonoBehaviour
{

    private DailyNetworkHandler client;

    // Start is called before the first frame update
    void Start()
    {
        client = new DailyNetworkHandler();
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

    public void Login()
    {
        Userdata data = new Userdata
        {
            type = "login",
            username = "yyyyy",
            pwd = "xxxxx"
        };

        string json = JsonUtility.ToJson(data);
        client.SendMessage(Encoding.UTF8.GetBytes(json));
        Debug.Log(json);
    }
}
