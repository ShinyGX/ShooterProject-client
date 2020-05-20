using System;

public class FixedMath
{


    public static Fixed Abs(Fixed a)
    {
        if(a < Fixed.zero)
        {
            return -a;
        }

        return a;
    }

    public static Fixed Lerp(Fixed a, Fixed b, float t)
    {
        return a + (b - a) * t;
    }
    public static Fixed Lerp(Fixed a, Fixed b, Fixed t)
    {
        return a + (b - a) * t;
    }

    public static Fixed3 Lerp(Fixed3 a, Fixed3 b, float t)
    {
        return new Fixed3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
    }

    public static Fixed3 Lerp(Fixed3 a,Fixed3 b,Fixed t)
    {
        return new Fixed3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
    }


    public static Fixed SmoothDamp(Fixed current, Fixed target, ref Fixed currentVelocity, Fixed smoothTime, Fixed maxSpeed, Fixed deltaTime)
    {
        smoothTime = Max(new Fixed(0.0001f), smoothTime);
        Fixed num = 2f / smoothTime;
        Fixed num2 = num * deltaTime;
        Fixed num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
        Fixed num4 = current - target;
        Fixed num5 = target;
        Fixed num6 = maxSpeed * smoothTime;
        num4 = Clamp(num4, -num6, num6);
        target = current - num4;
        Fixed num7 = (currentVelocity + num * num4) * deltaTime;
        currentVelocity = (currentVelocity - num * num7) * num3;
        Fixed num8 = target + (num4 + num7) * num3;
        if (num5 - current > 0f == num8 > num5)
        {
            num8 = num5;
            currentVelocity = (num8 - num5) / deltaTime;
        }
        return num8;
    }

    public static Fixed Max(Fixed a, Fixed b)
    {
        return a > b ? a : b;
    }

    public static Fixed Clamp(Fixed value, Fixed max, Fixed min)
    {
        if (value < min)
            return min;

        if (value > max)
            return max;

        return value;
    }
}
