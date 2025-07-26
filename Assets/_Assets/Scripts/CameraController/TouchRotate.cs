using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    private Vector2 lastInputPosition;
    private bool isDragging = false;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastInputPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastInputPosition;
            RotateObject(delta);
            lastInputPosition = Input.mousePosition;
        }
#else
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastInputPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = touch.position - lastInputPosition;
                RotateObject(delta);
                lastInputPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
#endif
    }

    void RotateObject(Vector2 delta)
    {
        Camera cam = Camera.main;

        Vector3 horizontalAxis = cam.transform.up;       
        Vector3 verticalAxis = cam.transform.right;      

        float rotX = -delta.y * rotationSpeed;
        float rotY = delta.x * rotationSpeed;

        transform.Rotate(horizontalAxis, rotY, Space.World);
        transform.Rotate(verticalAxis, rotX, Space.World);
    }
}
