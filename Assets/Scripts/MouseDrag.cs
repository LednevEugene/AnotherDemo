using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// This class react to players attempt to drag object with mouse
/// </summary>
public class MouseDrag : MonoBehaviour {

    Vector3 offset;
    Vector3 screenPoint;
    Vector3 curScreenPoint;

    /// <summary>
    /// We need an offset in order to move object relativey to player's point of view
    /// </summary>
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }
    /// <summary>
    /// Applying dragged mouse position to our object x and z axis. 
    /// </summary>
    void OnMouseDrag()
    {
        curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, transform.position.y, curPosition.z);

    }
}
