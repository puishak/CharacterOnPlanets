using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    public static float gConstant = 6.67f;
    private Rigidbody rb;
    [SerializeField]
    private bool debug = false;

    public Vector3 fog { get; private set; }


    private void Awake()
    {
        if (!rb) rb = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
    }

    private void FixedUpdate()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        fog = Vector3.zero;
        foreach (GameObject planet in planets) fog += CalculateFOG(planet);

        if (debug) Debug.DrawRay(transform.position, fog);

        rb.AddForce(fog * Time.deltaTime, ForceMode.Acceleration);
    }


    private Vector3 CalculateFOG(GameObject gb)
    {
        float m1 = gb.GetComponent<Rigidbody>().mass;
        float m2 = rb.mass;
        Vector3 direction = gb.transform.position - transform.position;
        return direction * (gConstant * m1 * m2 / (Mathf.Pow(direction.magnitude, 2) + 0.0001f));
    }
}
