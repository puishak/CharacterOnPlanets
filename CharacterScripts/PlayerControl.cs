using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControl : MonoBehaviour
{
    private Movement mvmt;
    // private FPCamera MainCamera;


    [Range(1, 10)]
    [SerializeField] private float m_mouseSensitivity = 10;

    void Awake()
    {
        if (!mvmt) mvmt = gameObject.GetComponent(typeof(Movement)) as Movement;
        // if (!MainCamera) MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(FPCamera)) as FPCamera;
    }

    void FixedUpdate()
    {
        mvmt.vertical = Input.GetAxis("Vertical");
        mvmt.horizontal = Input.GetAxis("Horizontal");
        mvmt.mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity / 5;
        mvmt.mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity / 5;

        mvmt.jump = Input.GetKey(KeyCode.Space);
        mvmt.thrust = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


        //if (isThrusting) MainCamera.Angle -= m_mouseSensitivity * 2 * Time.deltaTime;

        // MainCamera.Angle += Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime * 20;

        //if (!wasGrounded && isGrounded) MainCamera.Angle = 0;
    }
}
