using UnityEngine;

public struct Fixed2 
{
    public Fixed x;
    public Fixed y;

    public Fixed2(float x,float y)
    {
        this.x = new Fixed(x);
        this.y = new Fixed(y);
    }

    public Fixed2(Fixed x,Fixed y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector3 ToVector3(int y = 0)
    {
        return new Vector3(this.x.ToFloat(), y, this.y.ToFloat());
    }

    public static Fixed2 operator+(Fixed2 a,Fixed2 b)
    {
        return new Fixed2(a.x + b.x, a.y + b.y);
    }

    public static Fixed2 operator-(Fixed2 a,Fixed2 b)
    {
        return new Fixed2(a.x - b.x, a.y - b.y);
    }

    public static Fixed2 operator*(Fixed2 a,Fixed b)
    {
        return new Fixed2(a.x * b, a.y * b);
    }

 
    public Fixed2 Normalized
    {
        get
        {
            if (x == 0 && y == 0)
            {
                return new Fixed2();
            }
            Fixed n = ((x * x) + (y * y)).Sqrt();
            Fixed2 result = new Fixed2(x / n, y / n);
            result.x = Fixed.Range(result.x, -1, 1);
            result.y = Fixed.Range(result.y, -1, 1);
            return result;
        }
    }

    public Fixed Magnitude
    {
        get
        {
            if (x == 0 & y == 0)
            {
                return Fixed.zero;
            }
            Fixed n = ((x * x) + (y * y)).Sqrt();
            return n;
        }
    }

    public static readonly Fixed2 left = new Fixed2(-1, 0);
    public static readonly Fixed2 right = new Fixed2(1, 0);
    public static readonly Fixed2 up = new Fixed2(0, 1);
    public static readonly Fixed2 down = new Fixed2(0, -1);
    public static readonly Fixed2 zero = new Fixed2(0, 0);


    public Fixed Dot(Fixed2 b)
    {
        return Dot(this, b);
    }

    public static Fixed Dot(Fixed2 a,Fixed2 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    public static Fixed2 operator-(Fixed2 a)
    {
        return new Fixed2(-a.x, -a.y);
    }

    public static Fixed3 operator *(Fixed2 a, Fixed2 b)
    {
        return new Fixed3(new Fixed(), new Fixed(), a.x * b.y - a.y * b.x);
    }

    public static bool operator ==(Fixed2 a, Fixed2 b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Fixed2 a, Fixed2 b)
    {
        return a.x != b.x || a.y != b.y;
    }
    public override string ToString()
    {
        return "{" + x.ToString() + "," + y.ToString() + "}";
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    
}
