using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        linkedCamera = GetComponent<Camera>();
    }

    public void SetCameraLock(){
        offset = transform.position - new Vector3(0,0,0);
    }

    // Update is called once per frame
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
