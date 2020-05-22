using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : Collision
{

    public Fixed r;

    private Circle circle = new Circle();

    public override void Update()
    {
        base.Update();

        circle.x = this.gameObject.transform.position.x;
        circle.y = this.gameObject.transform.position.z;

        circle.r = r;
        circle.Data = this;

        if (active)
            PhysicManager.Instance.Insert(circle);
    }

    protected override void OnCheck()
    {
        List<IShape> intersets = PhysicManager.Instance.Query(circle);
        foreach(IShape shape in intersets)
        {
            currentCollision.Add(shape.Data);
        }
    }
}
