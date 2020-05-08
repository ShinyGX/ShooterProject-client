using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Fixed3
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

    public Fixed Z
    {
        get;
        private set;
    }

    public Fixed3(int x = 0, int y = 0, int z = 0)
    {
        this.X = new Fixed(x);
        this.Y = new Fixed(y);
        this.Z = new Fixed(z);

    }
    public Fixed3(float x, float y, float z)
    {

        this.X = new Fixed(x);
        this.Y = new Fixed(y);
        this.Z = new Fixed(z);

    }
    public Fixed3(Fixed x, Fixed y, Fixed z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(X.ToFloat(), Y.ToFloat(), Z.ToFloat());
    }

    public static Fixed3 operator +(Fixed3 a, Fixed3 b)
    {
        return new Fixed3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    public static Fixed3 operator -(Fixed3 a, Fixed3 b)
    {
        return new Fixed3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }


    public static Fixed3 left = new Fixed3(-1, 0);
    public static Fixed3 right = new Fixed3(1, 0);
    public static Fixed3 up = new Fixed3(0, 1);
    public static Fixed3 down = new Fixed3(0, -1);
    public static Fixed3 zero = new Fixed3(0, 0);

    public Fixed Dot(Fixed3 b)
    {
        return Dot(this, b);
    }
    public static Fixed Dot(Fixed3 a, Fixed3 b)
    {
        return a.X * b.X + b.Y * a.Y;
    }

    public static Fixed3 operator -(Fixed3 a)
    {
        return new Fixed3(-a.X, -a.Y, -a.Z);
    }

    public override string ToString()
    {
        return "{" + X.ToString() + "," + Y.ToString() + "}";// + ":" + ToVector3().ToString();
    }
}
