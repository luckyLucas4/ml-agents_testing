using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBallMovement : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float angle = 50f;
    public float force = 70f;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        transform.Rotate(Vector3.back, angle);
        rb2D.AddForce(transform.up * force);
    }
    private void Update()
    {

    }
}
