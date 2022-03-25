using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : EntityManager
{
    public PlayerInputHandler pInputHandler;
    private Rigidbody playerRB;

    public Camera playerCam;

    public float maxSprintSpeed;
    public float maxWalkSpeed;

    public Vector3 moveDirection;

    public float rotationSpeed = 1f;
    public float targetRotation = 0f;

    public float accelerationAmount;
    public float accelerationDecayAmount;
    public float moveSpeed;

    public bool isRolling = false;
    public float rollTimer = 0.4f;

    public float rollForce = 5f;

    public bool isSprinting = false;

    public delegate void OnRollDelegate();
    public OnRollDelegate OnRoll;


    protected override void Start()
    {
        base.Start();

        pInputHandler = GetComponent<PlayerInputHandler>();
        pInputHandler.ChangeCharacterInputEnabled(true);

        playerRB = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
        //RotatePlayerToCamera();
    }

    public void RotatePlayerToCamera()
    {
        moveDirection = Quaternion.Euler(0, playerCam.transform.rotation.eulerAngles.y, 0) * moveDirection;

        Quaternion rotationVector = Quaternion.Euler(0, playerCam.transform.rotation.eulerAngles.y, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationVector, rotationSpeed * Time.fixedDeltaTime);
        
    }

    public void AccelerateAndDecelerateProcedure()
    {
        moveSpeed += ((pInputHandler.inputVector.magnitude > 0 ? accelerationAmount : accelerationDecayAmount) * Time.fixedDeltaTime);
        moveSpeed = Mathf.Clamp(moveSpeed, 0, isSprinting ? maxSprintSpeed : maxWalkSpeed);
        moveSpeed = pInputHandler.inputVector.magnitude > 0 ? moveSpeed : 0;
    }

    public void MovePlayer()
    {
        moveDirection = pInputHandler.inputVector.normalized;

        RotatePlayerToCamera();
        AccelerateAndDecelerateProcedure();

        playerRB.velocity = isRolling ? playerRB.velocity : (new Vector3(moveDirection.x * moveSpeed, playerRB.velocity.y, moveDirection.z * moveSpeed));

        if(pInputHandler.spacePressed)
        {
            TryRoll();
        }
    }

    public void TryRoll()
    {
        if(!isRolling)
        {
            OnRoll?.Invoke();
            playerRB.AddRelativeForce(pInputHandler.inputVector.magnitude > 0 ? pInputHandler.inputVector * rollForce : Vector3.forward * rollForce, ForceMode.Impulse);
            StartCoroutine(DoRoll());
        }
    }

    public IEnumerator DoRoll()
    {
        isRolling = true;
        yield return new WaitForSeconds(rollTimer);
        isRolling = false;
    }

    public override void TakeDamage(float val, Vector3 dir)
    {
        if(!isRolling)
        {
            base.TakeDamage(val, dir);
        }
        
    }

    public void DisableMovementOnDeath()
    {
        pInputHandler.ChangeCharacterInputEnabled(false);
    }

    public override void Die()
    {
        base.Die();
        DisableMovementOnDeath();
        moveDirection = new Vector3(0, 0, 0);
    }
}
