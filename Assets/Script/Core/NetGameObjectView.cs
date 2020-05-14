using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetGameObjectView : MonoBehaviour
{
    private NetGameObject netGameObject;
    public NetGameObject GameObject
    {
        get
        {
            return netGameObject;
        }
        set
        {
            netGameObject = value;
            InitGameObject();
        }
    }


    private void InitGameObject()
    {
        transform.position = netGameObject.transform.position.ToVector3();
        netGameObject.onStart += OnStart;
    }

    protected virtual void OnStart(){ }

    private void Lerp(float t)
    {
        if (netGameObject == null)
            return;

        transform.position = Vector3.Lerp(transform.position, netGameObject.transform.position.ToVector3(), t);
        transform.rotation = Quaternion.Euler(netGameObject.transform.rotation.ToVector3());
    }

    private void Update()
    {
        Lerp(Time.deltaTime * 10);
    }


}
