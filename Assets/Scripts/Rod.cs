using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    public bool isFree { get; private set; } = true;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Transform lineSource;
    [SerializeField] private float lineWidth = 0.03f;
    public Fish currentFish;
    private Transform lineTarget;
    private LineRenderer lineRenderer;
    private Transform zeroPosition;

    private void Start()
    {
        zeroPosition = BoatFishing.instance.transform;
        CreateLine();
        lineRenderer.enabled = false;
    }

    public void PullFish(Fish target, float maxDistance, float catchPower)
    {
        StartCoroutine(PullProcess(target, maxDistance, catchPower));
    }

    private IEnumerator PullProcess(Fish fish, float maxDistance, float catchPower)
    {
        isFree = false;
        currentFish = fish;
        lineTarget = fish.transform;
        lineRenderer.enabled = true;
        fish.pullingRodsCount++;
        while (true)
        {
            fish.catchProgress += catchPower * Time.deltaTime;
            if (fish == null)
            {
                lineRenderer.enabled = false;
                break;
            }
            var distance = Vector3.Distance(zeroPosition.position, fish.transform.position);
            distance -= 0.5f;//this is fix magic number :/
            if (distance > maxDistance)
            {
                fish.pullingRodsCount--;
                lineRenderer.enabled = false;
                break;
            }
            lineRenderer.SetPosition(0, lineSource.position);
            lineRenderer.SetPosition(1, lineTarget.position);
            yield return null;
        }
        isFree = true;
        currentFish = null;
    }

    public void StopPull()
    {
        currentFish.pullingRodsCount--;
        lineRenderer.enabled = false;
        isFree = true;
        currentFish = null;
        StopAllCoroutines();
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
    }
}