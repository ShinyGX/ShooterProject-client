
using Daily;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DailyCommandHandler : CommandHandlerAdapter
{

    public MatchView matchView;

    public override void OnReceve(object msg)
    {
        if(msg is LoginRet)
        {
            if(((LoginRet)msg).type == "login")
            {
                if(((LoginRet)msg).data == true)
                {
                    Gamedata.Instance.Username = ((LoginRet)msg).msg;
                    SceneManager.LoadScene(Constants.SceneIndex.MAIN);
                }
            }

            if (((LoginRet)msg).type == "register")
            {
                if (((LoginRet)msg).data == true)
                {
                    Gamedata.Instance.Username = ((LoginRet)msg).msg;
                }
            }
        }

        if(msg is MatchRet)
        {
            Gamedata.Instance.RoomId = ((MatchRet)msg).roomId;
            Gamedata.Instance.ClinetId = ((MatchRet)msg).clientId;
        }

        if(msg is MatchedRet)
        {
            //TODO: 进入ready模式
            matchView.ResetMathcStatus(false);
        }

        if(msg is StartRet)
        {
            //TODO: 开始战斗
            Gamedata.Instance.Ip = ((StartRet)msg).ip;
            Gamedata.Instance.Port = ((StartRet)msg).port;

            SceneManager.LoadScene(Constants.SceneIndex.BATTLE);
        }
    }


}
