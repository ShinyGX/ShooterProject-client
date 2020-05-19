public class NetJoystick
{
    private Fixed2 direction;
    public Fixed2 Direction
    {
        get
        {
            return SquareToCircle(direction);
        }
        set
        {
            direction = value;
        }
    }


    private Fixed two = new Fixed(2.0f);
    private Fixed2 SquareToCircle(Fixed2 v)
    {
        Fixed x = v.x;
        Fixed y = v.y;

        Fixed sqrtX = v.x * v.x;
        Fixed sqrtY = v.y * v.y;

        x = x * Fixed.Sqrt(Fixed.one - sqrtX / two);
        y = y * Fixed.Sqrt(Fixed.one - sqrtY / two);


        return new Fixed2(x, y);
    }

}
