using UnityEngine;

public class IsometricCamera : MonoBehaviour
{

    public float panSpeed = 35f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public float mapSize;

    public float scrollSpeed = 20f;
    public float dragSpeed = 5f;
    public float minY = 20f;
    public float maxY = 120f;
    

    private Vector3 dragOrigin;
    private bool isDragging;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(2))
        {
            float speed = dragSpeed * Time.deltaTime;
            Vector3 dragPos = new Vector3(Input.GetAxis("Mouse X") * 50f * speed, 0, Input.GetAxis("Mouse Y") * 50f * speed);
            Camera.main.transform.position -= dragPos;
            
            return;
        }

        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, 0, mapSize);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, 0, mapSize - 100);

        transform.position = pos;

    }
}
