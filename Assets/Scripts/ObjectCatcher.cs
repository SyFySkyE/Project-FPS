using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCatcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Object " + other.name + " has fallen through the object catcher. Destroying object.");
        Destroy(other.gameObject);
    }
}
