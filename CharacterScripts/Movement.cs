using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StayUpright))]
public class Movement : MonoBehaviour
{
    private StayUpright su;
    private Rigidbody rb;

    //Character Control Inputs 
    [HideInInspector]
    public float vertical { get; set; }
    [HideInInspector]
    public float horizontal { get; set; }
    [HideInInspector]
    public float mouseX { get; set; }
    [HideInInspector]
    public float mouseY { get; set; }
    [HideInInspector]
    public bool jump { get; set; }
    [HideInInspector]
    public bool thrust { get; set; }
    //

    //Serialized inputs
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5;
    [SerializeField] public float turnSpeed = 150;
    [SerializeField] public float jumpForce = 5;
    [SerializeField] public float thrustForce = 10;
    [SerializeField] public bool jet = false;


    private float currentV = 0;
    private float currentH = 0;
    private float currentU = 0;
    private float currentF = 0;
    private float currentR = 0;

    private readonly float interpolation = 10;
    private readonly float backwardRunScale = 0.66f;
    private readonly float sidewaysRunScale = 0.75f;


    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.25f; //make serializable

    private bool groundControls;


    private void Awake()
    {
        if (!su) su = gameObject.GetComponent(typeof(StayUpright)) as StayUpright;
        if (!rb) rb = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
    }

    void FixedUpdate()
    {
        if (su.isGrounded) groundControls = true;

        if (groundControls)
        {
            if (vertical < 0)
            {
                vertical *= backwardRunScale;
            }

            currentV = Mathf.Lerp(currentV, vertical, Time.deltaTime);
            currentH = Mathf.Lerp(currentH, horizontal, Time.deltaTime * sidewaysRunScale);
            transform.position += transform.forward * currentV * moveSpeed * Time.deltaTime;
            transform.position += transform.right * currentH * moveSpeed * Time.deltaTime;


            JumpingAndLanding();
        }
        else
        {
            currentF = Mathf.Lerp(currentF, -horizontal, Time.deltaTime * interpolation);
            transform.rotation = Quaternion.AngleAxis(currentF * turnSpeed * Time.deltaTime, transform.forward) * transform.rotation;

            currentR = Mathf.Lerp(currentR, vertical, Time.deltaTime * interpolation);
            transform.rotation = Quaternion.AngleAxis(currentR * turnSpeed * Time.deltaTime, transform.right) * transform.rotation;
        }

        Thrust();
        currentU = Mathf.Lerp(currentU, mouseX, Time.deltaTime * interpolation);
        transform.rotation = Quaternion.AngleAxis(currentU * turnSpeed * Time.deltaTime, transform.up) * transform.rotation;
    }



    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;

        if (jumpCooldownOver && su.isGrounded && jump)
        {
            jumpTimeStamp = Time.time;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void Thrust()
    {
        bool t = thrust && !su.isGrounded;

        if (t)
        {
            rb.AddForce(transform.up * thrustForce, ForceMode.Force);
            groundControls = false;
        }
        // isThrusting = t;
        su.dontGround = t;
    }


}
