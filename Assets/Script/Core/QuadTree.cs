using System.Collections.Generic;

public class QuadTree
{
    private Rectangle boundary;
    private readonly int capacity;

    private List<IShape> points;

    //细分的四个象限
    private QuadTree ne, nw, se, sw;

    private bool divide = false;

    public QuadTree(Rectangle boundary,int capacity)
    {
        this.boundary = boundary;
        this.capacity = capacity;

        points = new List<IShape>();
    }

    public bool Insert(IShape p)
    {
        if (!boundary.Contains(p))
            return false;

        if(points.Count < capacity)
        {
            points.Add(p);
            return true;
        }

        if(!divide)
        {
            Subdivide();
        }

        return (ne.Insert(p) || nw.Insert(p) || se.Insert(p) || sw.Insert(p));
    }

    private void Subdivide()
    {
        Fixed x = boundary.center.x;
        Fixed y = boundary.center.y;
        Fixed w = boundary.width;
        Fixed h = boundary.height;

        ne = new QuadTree(new Rectangle(x + w, y - h, w, h), capacity);
        nw = new QuadTree(new Rectangle(x - w, y - h, w, h), capacity);
        se = new QuadTree(new Rectangle(x + w, y + h, w, h), capacity);
        sw = new QuadTree(new Rectangle(x - w, y + h, w, h), capacity);

        divide = true;
    }


    public List<IShape> Query(IShape range,List<IShape> shapes = null)
    {
        if(shapes == null)
        {
            shapes = new List<IShape>();
        }

        if(!range.Intersects(boundary))
        {
            return shapes;
        }

        foreach(var point in points)
        {
            if (range.Contains(point))
                shapes.Add(point);
        }

        if(divide)
        {
            nw.Query(range, shapes);
            ne.Query(range, shapes);
            sw.Query(range, shapes);
            se.Query(range, shapes);
        }

        return shapes;
    }

}

