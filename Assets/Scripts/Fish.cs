using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float health = 100f;
    public float price = 10f;
    public float weight = 1f;
    public float speed = 3f;

    private int pullRodsCount = 0;
    private bool isCatched = false;
    private float progress = 0;
    [SerializeField] private GameObject progressObject;

    private Vector3 areal = Vector3.one;
    private Vector3 targetPosition = Vector3.zero;
    private Rigidbody rigidbody;

    public int pullingRodsCount
    {
        get
        {
            return pullRodsCount;
        }
        set
        {
            pullRodsCount = value;
            if (pullRodsCount == 0)
                catchProgress = 0;
        }
    }
    public float catchProgress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = value;
            if (progress >= health && !isCatched)
            {
                isCatched = true;
                BoatFishing.instance.CatchFish(this);
            }
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        isCatched = false;
        pullingRodsCount = 0;
        catchProgress = 0;
    }

    public void SetAreal(Vector3 vector)
    {
        areal = vector;
    }

    private void Update()
    {
        if (!IsOnView())
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }
        CheckDestination();
        Move();
        if (pullRodsCount != 0)
            progressObject.SetActive(true);
        else
            progressObject.SetActive(false);
    }

    public void Setup(float fishHealth, float fishPrice, float fishWeight, float fishSpeed, Vector3 fishAreal)
    {
        health = fishHealth;
        price = fishPrice;
        weight = fishWeight;
        speed = fishSpeed;
        areal = fishAreal;
    }

    private void Move()
    {
        var vector = targetPosition - transform.localPosition;
        vector = vector.normalized*speed;
        rigidbody.velocity = vector;
        transform.LookAt(vector+transform.position);
    }

    private void CheckDestination()
    {
        var difference = transform.localPosition - targetPosition;
        if (Mathf.Abs(difference.x) < 0.1f && Mathf.Abs(difference.z) < 0.1f)
            targetPosition = GetNewTarget();
    }

    private Vector3 GetNewTarget()
    {
        var x = UnityEngine.Random.Range(-areal.x, areal.x);
        var z = UnityEngine.Random.Range(-areal.z, areal.z);
        var y = 0;
        return new Vector3(x, y, z);
    }

    private bool IsOnView()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position);
        var min = 0.05f;
        var max = 1.05f;
        if (point.y < -min || point.x < -min || point.y > max || point.x > max)
            return false;
        return true;
    }
}