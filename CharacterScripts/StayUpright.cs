using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravity))]
public class StayUpright : MonoBehaviour
{
    private Rigidbody rb;
    private Gravity gravity;

    public bool isGrounded = true;
    private bool wasGrounded = true;

    public bool dontGround = false;

    private List<Collider> collisions = new List<Collider>();

    private Vector3 velocity;

    [Header("Grounding")]
    [SerializeField] public static float minProximityToPlanet = 10;
    [SerializeField] public static float groundingCoolDown = 2.0f;//time to wait before switching to jet controls

    private float groundingTimeStamp;

    private void Awake()
    {
        if (!rb) rb = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        if (!gravity) gravity = gameObject.GetComponent(typeof(Gravity)) as Gravity;
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
        if (isGrounded)
        {

            Vector3 gravityDirection = gravity.fog.normalized;

            if (Vector3.Dot(transform.up, -gravityDirection) < 1)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravity.fog) * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 40 * Time.deltaTime);
            }
        }
        else
        {
            //Debug.DrawRay(transform.position, Velocity.normalized * C.minProximityToPlanet, Color.red);
            Vector3 _origin = transform.position + gravity.fog.normalized * 5;
            RaycastHit hit;
            if (Physics.Raycast(_origin, gravity.fog, out hit, minProximityToPlanet) && !dontGround)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravity.fog) * transform.rotation;
                float t = Time.deltaTime * velocity.magnitude * 10 / minProximityToPlanet;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            }
        }

        wasGrounded = isGrounded;
    }


    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, -gravity.fog.normalized) > 0.5f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            Vector3 _surfaceNormal = contactPoints[i].normal;
            if (Vector3.Dot(_surfaceNormal, -gravity.fog.normalized) > 0.5f)
            {
                isGrounded = true;
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0)
        {
            isGrounded = false;
        }
    }
}
