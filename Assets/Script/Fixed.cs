using System;
using UnityEngine;

[Serializable]
public struct Fixed
{
    public static readonly int bitFracbits = 16;
    public static readonly Fixed Zero;

    public Int64 Value
    {
        get
        {
            return bit;
        }
    }

    private Int64 bit;

    public Fixed(int x)
    {
        bit = (x << bitFracbits);
    }

    public Fixed(float x)
    {
        bit = (Int64)((x) * (1 << bitFracbits));
    }

    public Fixed(Int64 x)
    {
        bit = ((x) * (1 << bitFracbits));
    }

    public Fixed SetValue(Int64 v)
    {
        bit = v;
        return this;
    }

    public static Fixed Lerp(Fixed a, Fixed b, float t)
    {
        return a + (b - a) * t;
    }
    public static Fixed Lerp(Fixed a, Fixed b, Fixed t)
    {
        return a + (b - a) * t;
    }

    public static Fixed RotationLerp(Fixed a, Fixed b, Fixed t)
    {
        while (a < 0)
        {
            a += 360;
        }
        while (b < 0)
        {
            b += 360;
        }
        var offset1 = b - a;
        var offset2 = b - (a + 360);
        return a + t * (offset1.Abs() < offset2.Abs() ? offset1 : offset2);
    }
    public Fixed Abs()
    {

        return Fixed.Abs(this);
    }
    public Fixed Sqrt()
    {
        return Fixed.Sqrt(this);
    }

    public static Fixed Range(Fixed n, int min, int max)
    {
        if (n < min)
            n = new Fixed(min);
        if (n > max)
            n = new Fixed(max);
        return n;
    }


    //******************* +  **************************
    public static Fixed operator +(Fixed p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit + p2.bit;
        return tmp;
    }
    public static Fixed operator +(Fixed p1, int p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit + (Int64)(p2 << bitFracbits);
        return tmp;
    }
    public static Fixed operator +(int p1, Fixed p2)
    {
        return p2 + p1;
    }
    public static Fixed operator +(Fixed p1, Int64 p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit + p2 << bitFracbits;
        return tmp;
    }
    public static Fixed operator +(Int64 p1, Fixed p2)
    {
        return p2 + p1;
    }

    public static Fixed operator +(Fixed p1, float p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit + (Int64)(p2 * (1 << bitFracbits));
        return tmp;
    }
    public static Fixed operator +(float p1, Fixed p2)
    {
        Fixed tmp = p2 + p1;
        return tmp;
    }
    //*******************  -  **************************
    public static Fixed operator -(Fixed p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit - p2.bit;
        return tmp;
    }

    public static Fixed operator -(Fixed p1, int p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit - (Int64)(p2 << bitFracbits);
        return tmp;
    }

    public static Fixed operator -(int p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = (p1 << bitFracbits) - p2.bit;
        return tmp;
    }
    public static Fixed operator -(Fixed p1, Int64 p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit - (p2 << bitFracbits);
        return tmp;
    }
    public static Fixed operator -(Int64 p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = (p1 << bitFracbits) - p2.bit;
        return tmp;
    }

    public static Fixed operator -(float p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = (Int64)(p1 * (1 << bitFracbits)) - p2.bit;
        return tmp;
    }
    public static Fixed operator -(Fixed p1, float p2)
    {
        Fixed tmp;
        tmp.bit = p1.bit - (Int64)(p2 * (1 << bitFracbits));
        return tmp;
    }

    //******************* * **************************
    public static Fixed operator *(Fixed p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = ((p1.bit) * (p2.bit)) >> (bitFracbits);
        return tmp;
    }

    public static Fixed operator *(int p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = p1 * p2.bit;
        return tmp;
    }
    public static Fixed operator *(Fixed p1, int p2)
    {
        return p2 * p1;
    }
    public static Fixed operator *(Fixed p1, float p2)
    {
        Fixed tmp;
        tmp.bit = (Int64)(p1.bit * p2);
        return tmp;
    }
    public static Fixed operator *(float p1, Fixed p2)
    {
        Fixed tmp;
        tmp.bit = (Int64)(p1 * p2.bit);
        return tmp;
    }
    //******************* / **************************
    public static Fixed operator /(Fixed p1, Fixed p2)
    {
        Fixed tmp;
        if (p2 == Fixed.Zero)
        {
            Debug.LogWarning("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            tmp.bit = (p1.bit) * (1 << bitFracbits) / (p2.bit);
        }
        return tmp;
    }
    public static Fixed operator /(Fixed p1, int p2)
    {
        Fixed tmp;
        if (p2 == 0)
        {
            Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            tmp.bit = p1.bit / (p2);
        }
        return tmp;
    }
    public static Fixed operator %(Fixed p1, int p2)
    {
        Fixed tmp;
        if (p2 == 0)
        {
            Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            tmp.bit = (p1.bit % (p2 << bitFracbits));
        }
        return tmp;
    }
    public static Fixed operator /(int p1, Fixed p2)
    {
        Fixed tmp;
        if (p2 == Zero)
        {
            Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            Int64 tmp2 = ((Int64)p1 << bitFracbits << bitFracbits);
            tmp.bit = tmp2 / (p2.bit);
        }
        return tmp;
    }
    public static Fixed operator /(Fixed p1, Int64 p2)
    {
        Fixed tmp;
        if (p2 == 0)
        {
            Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            tmp.bit = p1.bit / (p2);
        }
        return tmp;
    }
    public static Fixed operator /(Int64 p1, Fixed p2)
    {
        Fixed tmp;
        if (p2 == Zero)
        {
            UnityEngine.Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            if (p1 > Int32.MaxValue || p1 < Int32.MinValue)
            {
                tmp.bit = 0;
                return tmp;
            }
            tmp.bit = (p1 << bitFracbits) / (p2.bit);
        }
        return tmp;
    }
    public static Fixed operator /(float p1, Fixed p2)
    {
        Fixed tmp;
        if (p2 == Zero)
        {
            Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            Int64 tmp1 = (Int64)p1 * ((Int64)1 << bitFracbits << bitFracbits);
            tmp.bit = (tmp1) / (p2.bit);
        }
        return tmp;
    }
    public static Fixed operator /(Fixed p1, float p2)
    {
        Fixed tmp;
        if (p2 > -0.000001f && p2 < 0.000001f)
        {
            Debug.LogError("/0");
            tmp.bit = Zero.bit;
        }
        else
        {
            tmp.bit = (p1.bit << bitFracbits) / ((Int64)(p2 * (1 << bitFracbits)));
        }
        return tmp;
    }
    public static Fixed Sqrt(Fixed p1)
    {
        Fixed tmp;
        Int64 ltmp = p1.bit * (1 << bitFracbits);
        tmp.bit = (Int64)Math.Sqrt(ltmp);
        return tmp;
    }
    public static bool operator >(Fixed p1, Fixed p2)
    {
        return (p1.bit > p2.bit) ? true : false;
    }
    public static bool operator <(Fixed p1, Fixed p2)
    {
        return (p1.bit < p2.bit) ? true : false;
    }
    public static bool operator <=(Fixed p1, Fixed p2)
    {
        return (p1.bit <= p2.bit) ? true : false;
    }
    public static bool operator >=(Fixed p1, Fixed p2)
    {
        return (p1.bit >= p2.bit) ? true : false;
    }
    public static bool operator !=(Fixed p1, Fixed p2)
    {
        return (p1.bit != p2.bit) ? true : false;
    }
    public static bool operator ==(Fixed p1, Fixed p2)
    {
        return (p1.bit == p2.bit) ? true : false;
    }

    public static bool Equals(Fixed p1, Fixed p2)
    {
        return (p1.bit == p2.bit) ? true : false;
    }

    public bool Equals(Fixed right)
    {
        if (bit == right.bit)
        {
            return true;
        }
        return false;
    }

    public static bool operator >(Fixed p1, float p2)
    {
        return (p1.bit > (p2 * (1 << bitFracbits))) ? true : false;
    }
    public static bool operator <(Fixed p1, float p2)
    {
        return (p1.bit < (p2 * (1 << bitFracbits))) ? true : false;
    }
    public static bool operator <=(Fixed p1, float p2)
    {
        return (p1.bit <= p2 * (1 << bitFracbits)) ? true : false;
    }
    public static bool operator >=(Fixed p1, float p2)
    {
        return (p1.bit >= p2 * (1 << bitFracbits)) ? true : false;
    }
    public static bool operator !=(Fixed p1, float p2)
    {
        return (p1.bit != p2 * (1 << bitFracbits)) ? true : false;
    }
    public static bool operator ==(Fixed p1, float p2)
    {
        return (p1.bit == p2 * (1 << bitFracbits)) ? true : false;
    }


    public static Fixed Max()
    {
        Fixed tmp;
        tmp.bit = Int64.MaxValue;
        return tmp;
    }

    public static Fixed Max(Fixed p1, Fixed p2)
    {
        return p1.bit > p2.bit ? p1 : p2;
    }
    public static Fixed Min(Fixed p1, Fixed p2)
    {
        return p1.bit < p2.bit ? p1 : p2;
    }

    public static Fixed Precision()
    {
        Fixed tmp;
        tmp.bit = 1;
        return tmp;
    }

    public static Fixed MaxValue()
    {
        Fixed tmp;
        tmp.bit = Int64.MaxValue;
        return tmp;
    }
    public static Fixed Abs(Fixed P1)
    {
        Fixed tmp;
        tmp.bit = Math.Abs(P1.bit);
        return tmp;
    }
    public static Fixed operator -(Fixed p1)
    {
        Fixed tmp;
        tmp.bit = -p1.bit;
        return tmp;
    }

    public float ToFloat()
    {
        return bit / (float)(1 << bitFracbits);
    }
    public Quaternion ToUnityRotation()
    {
        return Quaternion.Euler(0, -this.ToFloat(), 0);
    }
    public int ToInt()
    {
        return (int)(bit >> (bitFracbits));
    }
    public override string ToString()
    {
        double tmp = (double)bit / (double)(1 << bitFracbits);
        return tmp.ToString();
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
