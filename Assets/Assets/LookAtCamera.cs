using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera Camera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.transform.rotation * Vector3.forward,
                       Camera.transform.rotation * Vector3.up);
        
    }
}
