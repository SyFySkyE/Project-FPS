using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float lightRestoreAmount = 5f;
    [SerializeField] float restoreAngleAmount = 40f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(restoreAngleAmount);
            other.GetComponentInChildren<Flashlight>().RestoreLightIntensity(lightRestoreAmount);
            Destroy(gameObject);
        }
    }
}
