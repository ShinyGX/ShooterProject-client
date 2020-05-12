using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchView : MonoBehaviour
{
    public void Match()
    {
        DailyNetworkClient.Match(Gamedata.Instance.Username);
    }

    public void Ready()
    {
        DailyNetworkClient.Ready(Gamedata.Instance.RoomId, Gamedata.Instance.ClinetId);
    }
}
