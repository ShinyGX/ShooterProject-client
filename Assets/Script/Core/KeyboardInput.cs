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

    [Header("Base Input")]
    public string atk;
    public string crounch;
    public string skillQ;
    public string jump;

    [Header("Mouse Input")]
    public string verticalAxis;
    public string horizontalAxis;


    private bool atkState;
    private bool crounchState;
    private bool skillQState;
    private bool jumpState;

    private Fixed forwardValue;
    private Fixed rightValue;

    private Fixed vertical;
    private Fixed horizontal;

    public bool invertX;
    public bool invertY;

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        forwardValue = (Input.GetKey(forward) ? Fixed.one : Fixed.zero) - (Input.GetKey(back) ? Fixed.one : Fixed.zero);
        rightValue = (Input.GetKey(right) ? Fixed.one : Fixed.zero) - (Input.GetKey(left) ? Fixed.one : Fixed.zero);

        vertical = Input.GetAxis(verticalAxis).ToFixed();
        horizontal = Input.GetAxis(horizontalAxis).ToFixed();

        atkState = Input.GetButton(atk);
        crounchState = Input.GetButton(crounch);
        skillQState = Input.GetButton(skillQ);
        jumpState = Input.GetButton(jump);

        keyMap[GameKeyCode.ATK] = atkState;
        keyMap[GameKeyCode.CROUNCH] = crounchState;
        keyMap[GameKeyCode.SKILLQ] = skillQState;
        keyMap[GameKeyCode.JUMP] = jumpState;

        if (invertY)
            vertical = -vertical;
        if (invertX)
            horizontal = -horizontal;

        Direction = new Fixed2(forwardValue, rightValue);
        MouseDirection = new Fixed2(horizontal, vertical);
    }



}
