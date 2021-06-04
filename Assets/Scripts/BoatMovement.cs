using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody boatRigidbody;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 lastPosition;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (vertical == 0 && horizontal == 0) return;
        var direction = (lastPosition - boatRigidbody.transform.position);
        boatRigidbody.transform.LookAt(direction + boatRigidbody.transform.position);
        var t = boatRigidbody.position;
        t.y = 0;
        boatRigidbody.position = t;
        lastPosition = boatRigidbody.transform.position;
    }

    private void Move()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        var vector = new Vector3(horizontal, 0, vertical);
        boatRigidbody.velocity = vector.normalized * speed;
    }
}