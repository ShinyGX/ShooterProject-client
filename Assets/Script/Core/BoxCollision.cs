using System;
using System.Collections.Generic;

public class BoxCollision : Collision
{
    public Fixed width, height;
    private Rectangle rect = new Rectangle();


    public override void Update()
    {
        base.Update();

        rect.center = this.gameObject.transform.position.toFixed2();
        rect.width = width;
        rect.height = height;
        rect.Data = this;

        if(active)
            PhysicManager.Instance.Insert(rect);
    }

    protected override void OnCheck()
    {
        List<IShape> intersects = PhysicManager.Instance.Query(rect);
        foreach(IShape shape in intersects)
        {
            currentCollision.Add(shape.Data);
        }
    }


}
