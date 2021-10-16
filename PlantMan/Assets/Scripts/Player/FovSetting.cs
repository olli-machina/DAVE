using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class FovSetting : MonoBehaviour
{
    public CinemachineVirtualCamera aimCam;
    public CinemachineFreeLook cam;
    public Slider fov;
    public float startCamFOV = 60, startAimFOV = 39;

    // Start is called before the first frame update
    void Start()
    {
        startAimFOV = aimCam.m_Lens.FieldOfView;
        startCamFOV = cam.m_Lens.FieldOfView;
        fov.onValueChanged.AddListener(delegate { UpdateFOV(); });
    }
    
    public void UpdateFOV()
    {
        Debug.Log("Updating FOV");
        cam.m_Lens.FieldOfView = startCamFOV * fov.value;
        aimCam.m_Lens.FieldOfView = startAimFOV * fov.value;
    }
}
