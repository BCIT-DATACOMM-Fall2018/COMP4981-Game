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
        isometricCamera.transform.position = playerLockCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerLockCamera.enabled)
            {
                isometricCamera.transform.position = playerLockCamera.transform.position;
                isometricCamera.fieldOfView = playerLockCamera.fieldOfView;
            } 
            else
            {
                playerLockCamera.fieldOfView = isometricCamera.fieldOfView;
            }
            playerLockCamera.enabled = !playerLockCamera.enabled;
            isometricCamera.enabled = !isometricCamera.enabled;
        }
        
    }
}
