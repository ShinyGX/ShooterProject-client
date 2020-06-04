using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetObjectDebug : MonoBehaviour
{
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityDebug.DrawCapsule(transform.position, new Vector3(0, 0, 0), transform.localScale,
    CapsuleDirection.YAxis, controller.radius, controller.height, Color.blue);
    }
}
