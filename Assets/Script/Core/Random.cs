using System;

public class Random
{
    UInt64 seed;
    public Random(UInt64 seed)
    {
        this.seed = seed;
    }

    public int Next()
    {
        seed = seed * 439677L;
        return (int)((seed) >> 16 & 0x7fff);
    }

    public int Range(int max)
    {
        return Next() % max;
    }

    public int Range(int min,int max)
    {
        return min + Range(max - min);
    }

    public Fixed RangeFixed()
    {
        return Range(0, 10000).ToFixed() / 10000;
    }

    public Fixed RangeFixed(Fixed max)
    {
        return RangeFixed() * max;
    }

    public Fixed RangeFixed(Fixed min,Fixed max)
    {
        return min + RangeFixed(max - min);
    }
}
