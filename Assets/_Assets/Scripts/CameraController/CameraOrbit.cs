using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;        
    public float distance = 5.0f;   
    public float xSpeed = 120.0f;   
    public float ySpeed = 120.0f;   

    public float yMinLimit = -20f;  
    public float yMaxLimit = 80f;   

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
#else
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            x += touch.deltaPosition.x * xSpeed * 0.02f * Time.deltaTime;
            y -= touch.deltaPosition.y * ySpeed * 0.02f * Time.deltaTime;
        }
#endif

        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
