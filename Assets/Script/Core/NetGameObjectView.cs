using UnityEngine;

public class NetGameObjectView : MonoBehaviour
{

    private NetGameObject netGameObject;
    public NetGameObject NetGameObject
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
        transform.position = NetGameObject.logicObject.transform.position;
        transform.rotation = NetGameObject.logicObject.transform.rotation;
        transform.localScale = NetGameObject.logicObject.transform.localScale;
    }

    protected virtual void OnStart(){ }

    private void Lerp(float t)
    {
        if (netGameObject == null)
            return;

        transform.position = Vector3.Lerp(transform.position, netGameObject.logicObject.transform.position, t);
        transform.rotation = Quaternion.Lerp(transform.rotation, NetGameObject.logicObject.transform.rotation, t);
        transform.localScale = Vector3.Lerp(transform.localScale, NetGameObject.logicObject.transform.localScale, t);
    }

    private void Update()
    {
        Lerp(Time.deltaTime * 10);
    }





    private void OnDestroy()
    {
        if (NetGameObject != null)
            NetGameObject.Destory();
    }
}
