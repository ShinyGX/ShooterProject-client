using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandHandler
{
    void OnReceve(object msg);
}

[RequireComponent(typeof(GameCommandReceiver))]
public class CommandHandlerAdapter : MonoBehaviour,ICommandHandler
{
    public GameCommandType type;

    public virtual void OnReceve(object msg){}

    private void Awake()
    {
        GetComponent<GameCommandReceiver>().Register(type, this);
    }


}