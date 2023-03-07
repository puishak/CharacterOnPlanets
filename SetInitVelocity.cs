using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetInitVelocity : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private Vector3 initial_velocity;


    [SerializeField]
    private bool set_random_initial_velocity = false;
    [SerializeField]
    private float max_random_initial_velocity = 100;

    private void Awake()
    {
        if (!rb) rb = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        if (set_random_initial_velocity)
        {
            float xv = Random.Range(-max_random_initial_velocity, max_random_initial_velocity);
            float yv = Random.Range(-max_random_initial_velocity, max_random_initial_velocity);
            float zv = Random.Range(-max_random_initial_velocity, max_random_initial_velocity);
            initial_velocity = new Vector3(xv, yv, zv);

        }

    }

    void Start()
    {
        rb.velocity = initial_velocity;
    }
}
