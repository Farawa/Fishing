using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHelper : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;

    public void SetColliderSize(float size)
    {
        sphereCollider.radius = size;
    }
}
