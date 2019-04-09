using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Interface: 	CameraSelector- A class to change the currently selected camera on user input.
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	TBD
///
/// DATE: 		March 24th, 2019
///
/// REVISIONS:
///
/// DESIGNER: 	Jeffrey Choy
///
/// PROGRAMMER: Jeffrey Choy
///
/// NOTES:
/// ----------------------------------------------
public class CameraSelector : MonoBehaviour
{

    public Camera playerLockCamera;
    public Camera isometricCamera;

    //
    /// ----------------------------------------------
	/// CONSTRUCTOR: Start
	///
	/// DATE: 		March 24th, 2019
	///
	/// REVISIONS:
	///
	/// DESIGNER:	Jeffrey Choy
	///
	/// PROGRAMMER:	Jeffrey Choy
	///
	/// INTERFACE: 	Update()
	///
	/// NOTES: Start is called before the first frame update - initializes values
	/// ----------------------------------------------
    void Start()
    {
        playerLockCamera.enabled = true;
        isometricCamera.enabled = false;
        isometricCamera.transform.position = playerLockCamera.transform.position;
    }

    /// ----------------------------------------------
    /// CONSTRUCTOR: Update
    ///
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	Update()
    ///
    /// NOTES: Called once per tick. Checks for user input and toggles between the two cameras.
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// CONSTRUCTOR: SetIsometricToPlayer
    ///
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	SetIsometricToPlayer()
    ///
    /// NOTES: Moves the isometric camera position to the current player lock camera position
    /// ----------------------------------------------
    public void SetIsometricToPlayer(){
        isometricCamera.transform.position = playerLockCamera.transform.position;
        isometricCamera.fieldOfView = playerLockCamera.fieldOfView;
    }

}
