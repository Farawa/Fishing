using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody boatRigidbody;
    [SerializeField] private float speed = 5;
    [SerializeField] private float axeleration = 2;
    private float currentSpeed = 0;

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
        var vector = new Vector3(horizontal, 0, vertical) * speed;
        boatRigidbody.transform.LookAt(boatRigidbody.position + vector.normalized);
    }

    private void Move()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var vector = new Vector3(horizontal, 0, vertical).normalized;

        boatRigidbody.velocity = vector * speed;
    }
}