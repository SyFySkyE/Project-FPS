using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomInSensitivity = 1;
    [SerializeField] float zoomOutSensitivity = 2;
    [SerializeField] Animator myAnimator;    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomOut()
    {
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
        myAnimator.SetBool("zoom", false);
    }

    private void ZoomIn()
    {
        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
        myAnimator.SetBool("zoom", true);
    }

    private void OnDisable()
    {
        ZoomOut();
    }
}
