using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicManager : Singleton<PhysicManager>
{
    private List<NetGameObject> shaps;
    private QuadTree tree;

    Rectangle rect = new Rectangle(0.ToFixed(), 0.ToFixed(), 500.ToFixed(), 500.ToFixed());
    public void OnInit()
    {

        shaps = new List<NetGameObject>();
        tree = new QuadTree(rect, 8);
    }


    public void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.DrawLine(rect.points[i].ToVector3(), rect.points[i + 1].ToVector3());
        }
        Debug.DrawLine(rect.points[3].ToVector3(), rect.points[0].ToVector3());

    }

}
