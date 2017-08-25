using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// So this class is implementing rotation with two fingers
/// (DoubleTouchRotation) DTRotation
/// </summary>
public class DTRotation : MonoBehaviour {

    public float rotationSpeed  = 1f;

    /// <summary>
    /// I'm tracking changes in position of the second touch and just adding them to eulerAngles.
    /// </summary>
    void Update () {
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (Input.touchCount >= 2)
        {

            //Touch touchZero = Input.GetTouch(0); 
            Touch touchOne = Input.GetTouch(1);
         

            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x /*+ (touchOne.deltaPosition.y * Time.deltaTime * rotationSpeed)*/,
                                                transform.eulerAngles.y + (touchOne.deltaPosition.x * Time.deltaTime * rotationSpeed),
                                                transform.eulerAngles.z + (touchOne.deltaPosition.y * Time.deltaTime * rotationSpeed));


            // I didn't like this way                     
            
            //var diff = VectOne - VectZero;
            //float angle = Mathf.Atan2(diff.y, diff.x);

            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Mathf.Rad2Deg * (angle) * rotationSpeed * Time.deltaTime, transform.eulerAngles.z);
        }
    }

}
