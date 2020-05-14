﻿using System;
using System.Collections.Generic;

public class NetGameObject
{
    public string tag;
    public string name;

    public int clientId = -1;

    public bool active = true;
    private bool start = false;

    public Action onStart;
    public Action netUpdate;

    public FixedTransform transform;
    public List<FixedComponent> fixedComponents;


    public BattleNetworkHandler handler;

    public Fixed deltaTime
    {
        get
        {
            return BattleNetworkClient.deltaTime;
        }
    }


    public bool IsLocalPlayer
    {
        get
        {
            return handler.gameLoop.IsLoaclPlayer(clientId);
        }
    }

    public virtual void Init(BattleNetworkHandler handler,int clientId = -1)
    {
        this.clientId = clientId;
        this.handler = handler;
    }

    protected void NetUpdate()
    {
        if (!active)
            return;

        if(!start)
        {
            onStart();
            start = true;
        }

        netUpdate();

        foreach(var component in fixedComponents)
        {
            component.Update();
        }
    }

    public T AddComponent<T>(T com = null) where T:FixedComponent,new()
    {
        if (com == null)
        {
            com = new T();
        }

        com.gameObject = this;
        fixedComponents.Add(com);
        return com;
    }

    public void Reset(Fixed3 position,Fixed3 rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Destory()
    {
        active = false;

    }

}
