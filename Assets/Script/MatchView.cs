using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchView : MonoBehaviour
{

    public Button btn_match;
    public Button btn_ready;

    public void ResetMathcStatus(bool status)
    {
        btn_match.interactable = status;
        btn_ready.interactable = !status;
    }

    public void Match()
    {
        DailyNetworkClient.Match(Gamedata.Instance.Username);
    }

    public void Ready()
    {
        DailyNetworkClient.Ready(Gamedata.Instance.RoomId, Gamedata.Instance.ClinetId);
    }
}
