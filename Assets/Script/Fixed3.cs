using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Fixed3
{
    public Fixed x;
    public Fixed y;
    public Fixed z;

    public Fixed3(int x = 0, int y = 0, int z = 0)
    {
        this.x = new Fixed(x);
        this.y = new Fixed(y);
        this.z = new Fixed(z);

    }
    public Fixed3(float x, float y, float z)
    {

        this.x = new Fixed(x);
        this.y = new Fixed(y);
        this.z = new Fixed(z);

    }
    public Fixed3(Fixed x, Fixed y, Fixed z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x.ToFloat(), y.ToFloat(), z.ToFloat());
    }

    public Fixed2 toFixed2()
    {
        return new Fixed2(x, z);
    }

    public static Fixed3 operator +(Fixed3 a, Fixed3 b)
    {
        return new Fixed3(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Fixed3 operator -(Fixed3 a, Fixed3 b)
    {
        return new Fixed3(a.x - b.x, a.y - b.y, a.z - b.z);
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
        return a.x * b.x + b.y * a.y;
    }

    public static Fixed3 operator -(Fixed3 a)
    {
        return new Fixed3(-a.x, -a.y, -a.z);
    }

    public override string ToString()
    {
        return "{" + x.ToString() + "," + y.ToString() + "}";// + ":" + ToVector3().ToString();
    }
}
