using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
    }

    void OnGUI()
    {
        if (GUILayout.Button("Switch Camera"))
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondaryCamera.enabled = !secondaryCamera.enabled;
        }
    }

    public Transform[] markers;
    private int currentMarker = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMarker = (currentMarker + 1) % markers.Length;
            secondaryCamera.transform.position = markers[currentMarker].position;
            secondaryCamera.transform.rotation = markers[currentMarker].rotation;
        }
    }

    public void SwitchCamera()
    {
        mainCamera.enabled = !mainCamera.enabled;
        secondaryCamera.enabled = !secondaryCamera.enabled;
    }



}