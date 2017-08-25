using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This code needs a target to lock on and then just keeps camera's relative position and rotation to the target.
/// </summary>
public class FixedCamera : MonoBehaviour
{

    [SerializeField] Transform target;

    float rotY;
    Vector3 offset;
    /// <summary>
    /// Finding offset from our camera to the target
    /// </summary>
    private void Start()
    {
        offset = target.position - transform.position;
    }

    /// <summary>
    /// Updating camera relative position and rotation 
    /// </summary>
    private void LateUpdate()
    {
        transform.position = target.position - (target.rotation * offset);

        transform.LookAt(target);
    }
}
