using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float camSensitivity;

    public CinemachineBrain playerCam;

    public CinemachineFreeLook defaultFreeLookCam;
    public CinemachineCameraOffset cineCamOffset;

    public float cameraShiftSpeed = 5f;

    public float defaulOffsetAmount = 0f;
    public float cameraOffsetAmount = 0.6f;

    private PlayerInputHandler pInputs;
    void Start()
    {
        pInputs = GetComponent<PlayerInputHandler>();
        cineCamOffset = defaultFreeLookCam.gameObject.GetComponent<CinemachineCameraOffset>();
    }

   
    void Update()
    {

    }

    private void LateUpdate()
    {
        OffsetPlayerCamera();
    }

    public void OffsetPlayerCamera()
    {
        if (pInputs.rmbPressed)
        {
            cineCamOffset.m_Offset = Vector3.Lerp(cineCamOffset.m_Offset, new Vector3(cameraOffsetAmount, cineCamOffset.m_Offset.y, cineCamOffset.m_Offset.z), cameraShiftSpeed * Time.deltaTime);
        }
        else
        {
            cineCamOffset.m_Offset = Vector3.Lerp(cineCamOffset.m_Offset, new Vector3(defaulOffsetAmount, cineCamOffset.m_Offset.y, cineCamOffset.m_Offset.z), cameraShiftSpeed);
        }
    }
}
