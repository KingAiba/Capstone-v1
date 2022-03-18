using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator playerAnim;
    private playerController playerController;

    public float playerMovementBlendSpeed = 0f;

    void Start()
    {

        playerAnim = GetComponent<Animator>();
        playerController = GetComponent<playerController>();

        playerController.OnRoll += PlayRollAnim;
        
    }

    
    void Update()
    {

    }

    private void LateUpdate()
    {
        PlayMovementAnim(playerController.pInputHandler.inputVector);
    }

    public void PlayMovementAnim(Vector3 inputVector)
    {
        //playerMovementBlendSpeed = curSpeed / maxSpeed;
        playerAnim.SetFloat("moveX", Mathf.Lerp(playerAnim.GetFloat("moveX"), inputVector.x, playerMovementBlendSpeed * Time.deltaTime));
        playerAnim.SetFloat("moveY", Mathf.Lerp(playerAnim.GetFloat("moveY"), inputVector.z, playerMovementBlendSpeed * Time.deltaTime));
    }

    public void PlayRollAnim()
    {
        playerAnim.SetTrigger("doRoll");
    }
}
