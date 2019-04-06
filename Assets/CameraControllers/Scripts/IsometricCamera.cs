using UnityEngine;

public class IsometricCamera : MonoBehaviour
{

    public float panSpeed = 35f;
    public float panBorderThickness = 10f;
    public float mapSize;
    public float scrollSpeed = 20f;
    public float dragSpeed = 5f;
    public float fovMin = 20f;
    public float fovMax = 60f;
    public Camera linkedCamera;

    private Vector3 dragOrigin;
    private bool isDragging;

    private void Start()
    {
        linkedCamera = GetComponent<Camera>();
    }

    

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetMouseButton(2))
        {
            float speed = dragSpeed * Time.deltaTime;
            Vector3 dragPos = new Vector3(-Input.GetAxis("Mouse Y") * 50f * speed, 0, Input.GetAxis("Mouse X") * 50f * speed);
            Camera.main.transform.position -= dragPos;
            return;
        } else
        {
            Vector3 pos = transform.position;

            if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
            {
                pos.z += panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            linkedCamera.fieldOfView -= scroll * scrollSpeed;
            linkedCamera.fieldOfView = Mathf.Clamp(linkedCamera.fieldOfView, fovMin, fovMax);

            pos.x = Mathf.Clamp(pos.x, 0, mapSize);
            pos.z = Mathf.Clamp(pos.z, 0, mapSize - 100);

            transform.position = pos;
        }

        

    }
}
