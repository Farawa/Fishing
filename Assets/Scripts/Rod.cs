using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [HideInInspector] public bool isFree = true;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Transform lineSource;
    [SerializeField] private float lineWidth = 0.03f;
    private Transform lineTarget;
    private LineRenderer lineRenderer;
    private Transform zeroPosition;

    private void Start()
    {
        zeroPosition = BoatFishing.instance.transform;
    }

    public void PullFish(Fish target, float maxDistance, float catchPower)
    {
        StartCoroutine(PullProcess(target, maxDistance, catchPower));
    }

    private IEnumerator PullProcess(Fish fish, float maxDistance, float catchPower)
    {
        lineTarget = fish.transform;
        CreateLine();
        var progress = 0f;
        var health = fish.health;
        //fish.isPulling = true;
        while (true)
        {
            progress += catchPower;
            print("progress = " + progress + " / " + health);
            if (progress>=health)
            {
                BoatFishing.instance.CatchFish();
                DestroyLine();
                yield break;
            }
            var distance = Vector3.Distance(zeroPosition.position, fish.transform.position);
            distance -= 0.5f;//this is fix magic number :/
            print("distance betwen boat and fish = " + distance);
            if (distance > maxDistance)
            {
                //fish.isPulling = false;
                DestroyLine();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void CreateLine()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;
        lineRenderer.numCapVertices = 5;
        StartCoroutine(UpdateLine());
        isFree = false;
    }

    private IEnumerator UpdateLine()
    {
        while (true)
        {
            lineRenderer.SetPosition(0, lineSource.position);
            lineRenderer.SetPosition(1, lineTarget.position);
            yield return null;
        }
    }

    private void DestroyLine()
    {
        StopAllCoroutines();
        Destroy(lineRenderer);
        isFree = true;
    }
}