using System;
using System.Timers;

public class BattleGameLoop
{

    private Timer timer;

    //服务端帧
    private int serverStep = 0;
    //客户端帧
    private int clientStep = 0;

    private BattleNetworkHandler handler;

    public Action update;
    public Action laterUpdate;

    private NetJoystick movement;

    private NetInputUnit[] inputUnit;
    public NetInputUnit this[int index]
    {
        get
        {
            if (index < 0)
                return inputUnit[this.inputUnit.Length - 1];
            else
                return inputUnit[index];
        }
    }

    public Fixed FixedTime
    {
        get;protected set;
    }

    public bool IsLoaclPlayer(int id)
    {
        return id == Gamedata.Instance.ClinetId;
    }

    public BattleGameLoop(BattleNetworkHandler handler,int max)
    {
        this.handler = handler;

        timer = new Timer(BattleNetworkClient.deltaTime.ToFloat() * 1000);
        timer.Elapsed += SendFrame;
        timer.Enabled = true;

        movement = new NetJoystick();

        inputUnit = new NetInputUnit[max + 1];
        for (int i = 0; i < max + 1; i++)
        {
            inputUnit[i] = new NetInputUnit();
        }
    }

    private void SendFrame(object sender,ElapsedEventArgs args)
    {
        if (Gamedata.Instance.ClinetId < 0)
            return;

        INetworkProtocol protocol = new ByteProtocol();
        protocol.Push((byte)BattleNetworkHandler.MessageType.Frame);
        protocol.Push((byte)Gamedata.Instance.ClinetId);

        protocol.Push(movement.Direction);

        handler.SendMessage(protocol.OutputBytesStream());
    }

    public void SetMovement(Fixed2 dir)
    {
        movement.Direction = dir;
    }

    public void ReceiveStep(INetworkProtocol protocol)
    {

        int length = protocol.GetByte();
        serverStep++;

        for (int i = 0; i < length; i++)
        {
            if (protocol.GetBool())
            {
                inputUnit[i].ReceviceStep(protocol);
            }
        }

        for(;clientStep < serverStep;clientStep++)
        {
            update?.Invoke();
            laterUpdate?.Invoke();

            PhysicManager.Instance.RebuildQuadTree();



            FixedTime += BattleNetworkClient.deltaTime;
        }

    }


}
