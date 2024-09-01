using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input Reader")]
public class InputReader : ScriptableObject
{
    public event Action OnJumpInput = delegate { };

    public float RetrieveHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool RetrieveJumpInput(bool thisFrame = false)
    {
        if(thisFrame) return Input.GetKeyDown(KeyCode.Space);
        else return Input.GetKey(KeyCode.Space);
    }

    public bool RetrieveDashInput(bool thisFrame = false)
    {
        if(thisFrame) return Input.GetKeyDown(KeyCode.LeftShift);
        else return Input.GetKey(KeyCode.LeftShift);
    }

    public bool RetrieveDuckInput()
    {
        return Input.GetAxis("Vertical") < 0;
    }
}
