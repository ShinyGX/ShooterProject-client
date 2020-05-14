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
        transform.position = netGameObject.transform.position.ToVector3();
        transform.rotation = Quaternion.Euler(netGameObject.transform.rotation.ToVector3());
        transform.localScale = netGameObject.transform.scale.ToVector3();
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




    private void OnDestroy()
    {
        if (NetGameObject != null)
            NetGameObject.Destory();
    }
}
