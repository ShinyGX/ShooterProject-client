using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    public InputField if_username;
    public InputField if_pwd;

    public void Login()
    {
        string username = if_username.text;
        string pwd = if_pwd.text;

        DailyNetworkClient.Login(username, pwd);
    }

    public void Register()
    {
        string username = if_username.text;
        string pwd = if_pwd.text;

        DailyNetworkClient.Register(username, pwd);
    }
}
