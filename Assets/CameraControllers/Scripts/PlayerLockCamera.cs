using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockCamera : MonoBehaviour
{

    public GameObject player;
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
        offset = transform.position - player.transform.position;
        linkedCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        linkedCamera.fieldOfView -= scroll * scrollSpeed;
        linkedCamera.fieldOfView = Mathf.Clamp(linkedCamera.fieldOfView, fovMin, fovMax);


    }
}
