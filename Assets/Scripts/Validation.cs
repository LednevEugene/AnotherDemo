using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// This class can detect ImageTarget's tracking state
/// </summary>
public class Validation : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    [SerializeField] Text validationLabel;
    [SerializeField] GameObject Ethan;
    void Start () {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

	 public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(validationLabel != null)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                //Turning off validation label and turning on Ethan
                validationLabel.gameObject.SetActive(false);
                Ethan.SetActive(true);
            }
            else
            {
                //Turning on validation label and turning off Ethan
                //Becaues if don't do that game goes apeshit and crashes spamming inactive controller move warning 
                validationLabel.gameObject.SetActive(true);
                Ethan.SetActive(false);
            }
        }
       
    }
}
