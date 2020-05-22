using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collision : FixedComponent
{
    public bool isTrigger = false;
    public bool active = true;


    public Action<Collision> onCollisionEnter;
    public Action<Collision> onCollisionStay;
    public Action<Collision> onCollisionExit;

    protected List<Collision> currentCollision;
    protected List<Collision> lastCollision;

    public override void Init()
    {
        base.Init();
        gameObject.LaterUpdate += LaterUpdate;
    }

    public override void Update()
    {
        foreach (Collision col in currentCollision)
        {
            if (lastCollision.Contains(col))
            {
                onCollisionStay?.Invoke(col);
                lastCollision.Remove(col);
            }
            else
            {
                onCollisionEnter?.Invoke(col);
            }
        }

        foreach (Collision col in lastCollision)
        {
            onCollisionExit?.Invoke(col);
        }

        lastCollision.Clear();
        lastCollision.AddRange(currentCollision);
        currentCollision.Clear();
    }

    private void LaterUpdate()
    {
        OnCheck();
    }

    protected abstract void OnCheck();
}
