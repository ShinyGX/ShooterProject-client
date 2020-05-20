using System.Collections.Generic;

public interface IShape
{
    NetGameObject Data { get; set; }

    //是否包含点
    bool Contains(IShape point);

    //形状是否交接
    bool Intersects(IShape other);
}

public class ShapeBase
{
    private Fixed left, right, up, down;
    public Fixed width, height;
    private Fixed2[] points;


    public Fixed2 Center
    {
        get;
        set;
    }

    public Fixed2 this[int index]
    {
        get
        {
            return points[index];
        }
    }

    public void Reset()
    {
        left = points[0].x;
        right = points[0].x;
        up = points[0].y;
        down = points[0].y;

        for (int i = 0; i < points.Length; i++)
        {
            if (this[i].x < left)
                left = this[i].x;
            if (this[i].x > right)
                right = this[i].x;
            if (this[i].y < down)
                down = this[i].y;
            if (this[i].y > up)
                up = this[i].y;
        }

        width = FixedMath.Max(FixedMath.Abs(left), FixedMath.Abs(right)) * 2;
        height = FixedMath.Max(FixedMath.Abs(down), FixedMath.Abs(up)) * 2;
    }

    public Fixed2 Support(Fixed2 dir)
    {
        int index = 0;
        Fixed maxDot, t;
        Fixed2 p;

        p = this[index];

        maxDot = Fixed2.Dot(p, dir);
        for (; index < points.Length; index++)
        {
            t = Fixed2.Dot(this[index], dir);
            if (t > maxDot)
            {
                maxDot = t;
                p = this[index];
            }
        }

        return p + Center;
    }

    public static Fixed2 Support(ShapeBase a,ShapeBase b,Fixed2 dir)
    {
        Fixed2 p1 = a.Support(dir);
        Fixed2 p2 = b.Support(dir);
        return p1 - p2;
    }
}


public class Rectangle : IShape
{
    public NetGameObject Data { get; set; }
    public Fixed width, height;
    public Fixed2 center;

    public List<Fixed2> points;

    public Rectangle(Fixed x, Fixed y, Fixed width, Fixed height)
    {
        this.width = width;
        this.height = height;
        this.center = new Fixed2(x, y);

        points = new List<Fixed2>();

        points.Add(new Fixed2(center.x - width, center.y - height));
        points.Add(new Fixed2(center.x + width, center.y - height));
        points.Add(new Fixed2(center.x + width, center.y + height));
        points.Add(new Fixed2(center.x - width, center.y + height));

    }


    public bool Contains(IShape point)
    {
        if (point is Rectangle rect)
        {
            return (
                rect.center.x >= this.center.x - this.width &&
                rect.center.x <= this.center.x + this.width &&
                rect.center.y >= this.center.y - this.height &&
                rect.center.y <= this.center.y + this.height);
        }

        return false;
    }

    public bool Intersects(IShape other)
    {
        if(other is Rectangle rect)
        {
            return !(
                rect.center.x - rect.width > this.center.x + this.width ||
                rect.center.x + rect.width < this.center.x - this.width ||
                rect.center.y - rect.height > this.center.y + this.height ||
                rect.center.y + rect.height < this.center.y - this.height);
        }


        return false;
    }
}


public class Circle : IShape
{
    public NetGameObject Data { get; set; }
    public Fixed x, y, r, rSquared;

    public Circle(Fixed x, Fixed y, Fixed r)
    {
        this.x = x;
        this.y = y;
        this.r = r;
        this.rSquared = r * r;
    }

    public bool Contains(IShape point)
    {
        if (point is Circle c)
        {
            Fixed d = ((c.x - x) * (c.x - x)) + ((c.y - y) * (c.y - y));
            return d <= rSquared;
        }

        if(point is Rectangle rect)
        {
            return false;
        }

        return false;
    }

    public bool Intersects(IShape other)
    {
        if (other is Rectangle rect)
        {
            Fixed xDist = FixedMath.Abs(rect.center.x - this.x);
            Fixed yDist = FixedMath.Abs(rect.center.y - this.y);

            Fixed r = this.r;
            Fixed w = rect.width;
            Fixed h = rect.height;

            if (xDist > (r + w) || yDist > (r + h))
                return false;

            if (xDist <= w || yDist <= h)
                return true;

            Fixed edge = ((xDist - w) * (xDist - w)) + ((yDist - h) * (yDist - h));
            return edge <= this.rSquared;
        }


        return false;
    }
}