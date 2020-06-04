using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualInput : MonoBehaviour
{
    public Fixed2 Direction
    {
        get;
        protected set;
    }

    public Fixed2 MouseDirection
    {
        get;
        protected set;
    }

    public Dictionary<GameKeyCode, bool> keyMap = new Dictionary<GameKeyCode, bool>();
}

public enum GameKeyCode : byte
{
    ATK = 1,
    CROUNCH = 2,
    SKILLQ = 4,
    JUMP = 8
}
