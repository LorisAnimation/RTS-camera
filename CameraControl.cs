using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera cam;

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    public float scrollSpeed = 20f;
    public float scrollTime = 0.25f;
    public Vector3 scrollDestination = Vector3.zero;
    public float minFOV = 10;
    public float maxFOV = 40;

    public float rotSpeed = 5f;
    private float X;
    private float Y;


    private void Start()
    {
        panLimit.x = 60;
        panLimit.y = 60;
    }
    void Update()
    {

        MoovingCamera();   

        CameraRotation();

        ScrollZoom();

        
    }

    private void MoovingCamera()
    {

        if (Input.GetKey("z") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.position += panSpeed * Time.deltaTime * transform.forward;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.position += panSpeed * Time.deltaTime * -transform.forward;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.position += panSpeed * Time.deltaTime * transform.right;
        }
        if (Input.GetKey("q") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.position += panSpeed * Time.deltaTime * -transform.right;
        }


        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }

    private void CameraRotation()
    {
        if (Input.GetMouseButton(2))
        {
            transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X") * rotSpeed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
    }

    private void ScrollZoom()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            scrollDestination = cam.transform.position - new Vector3(0, scroll * scrollSpeed, 0);
        }

        if (scrollDestination != Vector3.zero && scrollDestination.y < maxFOV && scrollDestination.y > minFOV)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, scrollDestination, scrollTime);

            if (cam.transform.position == scrollDestination)
            {
                scrollDestination = Vector3.zero;
            }
        }

        if (cam.transform.position.y > maxFOV)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, maxFOV, cam.transform.position.z);
        }

        if (cam.transform.position.y < minFOV)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, minFOV, cam.transform.position.z);
        }
    }
}
