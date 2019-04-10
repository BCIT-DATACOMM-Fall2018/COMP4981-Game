using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Interface: 	PlayerLockCamera- A camera with position locked to player
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
public class PlayerLockCamera : MonoBehaviour
{

    public GameObject player;
    private Vector3 lastPosition;
    public Camera linkedCamera;
    public float yZoom;
    public float zZoom;
    private Vector3 offset;
    public float scrollSpeed = 20f;
    public float fovMin = 20f;
    public float fovMax = 60f;


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
        linkedCamera = GetComponent<Camera>();
    }

    /// ----------------------------------------------
    /// CONSTRUCTOR: SetCameraLock
    ///
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	LateUpdate()
    ///
    /// NOTES: Sets offset from player model.
    /// ----------------------------------------------
    public void SetCameraLock(){
        offset = transform.position - new Vector3(0,0,0);
    }

    /// ----------------------------------------------
    /// CONSTRUCTOR: LateUpdate
    ///
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	LateUpdate()
    ///
    /// NOTES: Called once per tick after all update calls. Updates camera position based on player model.
    /// ----------------------------------------------
    void LateUpdate()
    {
        if(player.transform.position.x == -10){
            transform.position = lastPosition + offset;

        } else {
            transform.position = player.transform.position + offset;
            lastPosition = player.transform.position;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        linkedCamera.fieldOfView -= scroll * scrollSpeed;
        linkedCamera.fieldOfView = Mathf.Clamp(linkedCamera.fieldOfView, fovMin, fovMax);
    }
}
