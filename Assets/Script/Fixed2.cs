using UnityEngine;

public struct Fixed2 
{
    public Fixed X
    {
        get;
        private set;
    }

    public Fixed Y
    {
        get;
        private set;
    }

    public Fixed2(float x,float y)
    {
        this.X = new Fixed(x);
        this.Y = new Fixed(y);
    }

    public Fixed2(Fixed x,Fixed y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vector3 ToVector3(int y)
    {
        return new Vector3(this.X.ToFloat(), y, this.Y.ToFloat());
    }

    public static Fixed2 operator+(Fixed2 a,Fixed2 b)
    {
        return new Fixed2(a.X + b.X, a.Y + b.Y);
    }

    public static Fixed2 operator-(Fixed2 a,Fixed2 b)
    {
        return new Fixed2(a.X - b.X, a.Y - b.Y);
    }

    public static Fixed2 operator*(Fixed2 a,Fixed b)
    {
        return new Fixed2(a.X * b, a.Y * b);
    }

 
    public Fixed2 Normalized
    {
        get
        {
            if (X == 0 && Y == 0)
            {
                return new Fixed2();
            }
            Fixed n = ((X * X) + (Y * Y)).Sqrt();
            Fixed2 result = new Fixed2(X / n, Y / n);
            result.X = Fixed.Range(result.X, -1, 1);
            result.Y = Fixed.Range(result.Y, -1, 1);
            return result;
        }
    }

    public Fixed Magnitude
    {
        get
        {
            if (X == 0 & Y == 0)
            {
                return Fixed.Zero;
            }
            Fixed n = ((X * X) + (Y * Y)).Sqrt();
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
        return a.X * b.X + a.Y * b.Y;
    }

    public static Fixed2 operator-(Fixed2 a)
    {
        return new Fixed2(-a.X, -a.Y);
    }

    public static Fixed3 operator *(Fixed2 a, Fixed2 b)
    {
        return new Fixed3(new Fixed(), new Fixed(), a.X * b.Y - a.Y * b.X);
    }

    public static bool operator ==(Fixed2 a, Fixed2 b)
    {
        return a.X == b.X && a.Y == b.Y;
    }
    public static bool operator !=(Fixed2 a, Fixed2 b)
    {
        return a.X != b.X || a.Y != b.Y;
    }
    public override string ToString()
    {
        return "{" + X.ToString() + "," + Y.ToString() + "}";
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
