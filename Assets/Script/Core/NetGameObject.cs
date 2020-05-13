public class NetGameObject
{
    public string tag;
    public string name;

    public int clientId = -1;

    public bool active = true;

    public FixedTransform transform;
    public BattleNetworkHandler handler;

    public bool IsLocalPlayer
    {
        get
        {
            return handler.gameLoop.IsLoaclPlayer(clientId);
        }
    }

    


}
