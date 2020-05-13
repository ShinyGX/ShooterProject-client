using System;
using System.Timers;

public class BattleGameLoop
{

    private Timer timer;

    //服务端帧
    private int serverStep = 0;
    //客户端帧
    private int clientStep = 0;

    private BattleNetworkHandler client;

    public Action update;
    public Action laterUpdate;

    private NetJoystick movement;

    public Fixed FixedTime
    {
        get;protected set;
    }

    public BattleGameLoop(BattleNetworkHandler client)
    {
        this.client = client;

        timer = new Timer(BattleNetworkClient.deltaTime.ToFloat() * 1000);
        timer.Elapsed += SendFrame;
        timer.Enabled = true;
    }

    private void SendFrame(object sender,ElapsedEventArgs args)
    {
        if (Gamedata.Instance.ClinetId < 0)
            return;

        INetworkProtocol protocol = new ByteProtocol();
        protocol.Push((byte)BattleNetworkHandler.MessageType.Frame);
        protocol.Push((byte)Gamedata.Instance.ClinetId);

        protocol.Push(movement.direction);

        client.SendMessage(protocol.OutputBytesStream());
    }

    public void SetMovement(Fixed2 dir)
    {
        movement.direction = dir;
    }

    public void ReceiveStep(INetworkProtocol protocol)
    {

        int length = protocol.GetByte();
        serverStep++;

        for(;clientStep < serverStep;clientStep++)
        {
            update?.Invoke();
            laterUpdate?.Invoke();

            FixedTime += BattleNetworkClient.deltaTime;
        }

    }


}
