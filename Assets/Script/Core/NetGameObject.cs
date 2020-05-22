using System;
using System.Collections.Generic;
using UnityEngine;

public class NetGameObject
{
    public string tag;
    public string name;

    public string prefab;

    public int clientId = -1;

    public bool active = true;
    private bool start = false;

    public FixedTransform transform = new FixedTransform();
    public List<FixedComponent> fixedComponents = new List<FixedComponent>();
    public Action LaterUpdate;


    public BattleNetworkHandler handler;

    public Fixed DeltaTime
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

        handler.gameLoop.update += NetUpdate;
        handler.gameLoop.laterUpdate += NetLaterUpdate;
    }

    public virtual void Update() { }
    public virtual void OnStart() { }

    protected void NetUpdate()
    {
        if (!active)
            return;

        if(!start)
        {
            OnStart();
            start = true;
        }

        Update();

        foreach(var component in fixedComponents)
        {
            component.Update();
        }

    }


    private void NetLaterUpdate()
    {
        LaterUpdate?.Invoke();
    }


    public T AddComponent<T>(T com = null) where T: FixedComponent,new()
    {
        if (com == null)
        {
            com = new T();
        }

        com.gameObject = this;
        com.Init();

        fixedComponents.Add(com);
        return com;
    }

    public T GetComponent<T>() where T: FixedComponent
    {
        for(int i = 0;i < fixedComponents.Count;i++)
        {
            if (fixedComponents[i] is T)
                return fixedComponents[i] as T;
        }

        return null;
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
    public GameObject InstantiateGameObject()
    {
        GameObject obj = GameObject.Instantiate(GetPrefab());
        var view = obj.GetComponent<NetGameObjectView>();
        if (view == null)
            view = obj.AddComponent<NetGameObjectView>();
        view.NetGameObject = this;

        return obj;
    }

    public GameObject GetPrefab()
    {
        var prefab = Resources.Load(PrefabPath()) as GameObject;
        if (prefab == null)
        {
            Debug.LogError("{" + PrefabPath() + "}is Null");
        }
        return prefab;
    }

    private string PrefabPath()
    {
        return "Perfabs/" + prefab;
    }
}
