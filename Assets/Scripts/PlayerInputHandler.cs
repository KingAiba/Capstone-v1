using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector3 inputVector = new Vector3(0, 0, 0);

    public bool spacePressed = false;
    public bool shiftPressed = false;

    public bool rmbPressed = false;
    public bool lmbPressed = false;


    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
    }

    public void GetInput()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        spacePressed = Input.GetKeyDown(KeyCode.Space);
        rmbPressed = Input.GetKey(KeyCode.Mouse1);
        lmbPressed = Input.GetKey(KeyCode.Mouse0);
    }


}
