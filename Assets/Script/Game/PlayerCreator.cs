public class PlayerCreator : MonoSignleton<PlayerCreator>, INetGameManager
{
    public int player = 1;
    public int Layer { get { return 99; } }

    public void OnInit(BattleNetworkHandler handler)
    {

        var playerPos = new Fixed2(handler.random.Range(0, 20), handler.random.Range(0, 20));
        for (int i = 0; i < player; i++)
        {
            var netObj = new CharacterNetController();
            netObj.Init(handler, i);
            netObj.prefab = "Player1";

            var col = netObj.AddComponent<BoxCollision>();
            col.width = 1.ToFixed();
            col.height = 1.ToFixed();

            var localPos = playerPos + Fixed2.left * i.ToFixed() * 10.ToFixed();
            netObj.transform.position =
                new Fixed3(localPos.x, Fixed.zero, localPos.y);

            netObj.InstantiateGameObject();
        }
    }
}
