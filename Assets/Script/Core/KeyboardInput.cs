using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : VirtualInput
{
    [Header("Movement")]
    public KeyCode forward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode back;

    private Fixed forwardValue;
    private Fixed rightValue;

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        forwardValue = (Input.GetKey(forward) ? Fixed.one : Fixed.zero) - (Input.GetKey(back) ? Fixed.one : Fixed.zero);
        rightValue = (Input.GetKey(right) ? Fixed.one : Fixed.zero) - (Input.GetKey(left) ? Fixed.one : Fixed.zero);

        Direction = new Fixed2(forwardValue, rightValue);
    }



}
