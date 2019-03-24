using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{

    public Camera playerLockCamera;
    public Camera isometricCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerLockCamera.enabled = false;
        isometricCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerLockCamera.enabled = !playerLockCamera.enabled;
            isometricCamera.enabled = !isometricCamera.enabled;
        }
        
    }
}
