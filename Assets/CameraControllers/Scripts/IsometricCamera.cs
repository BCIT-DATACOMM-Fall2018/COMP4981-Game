using UnityEngine;

/// ----------------------------------------------
/// Interface: 	IsometricCamera- A camera positioned isometrically that allows for edgepan.
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
    private void Start()
    {
        linkedCamera = GetComponent<Camera>();
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
    /// NOTES: Called once per tick after all update calls. Checks for user input/mouse position and moves the camera accordingly
    /// ----------------------------------------------
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
