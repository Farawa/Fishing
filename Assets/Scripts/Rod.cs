using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    public bool isFree { get; private set; } = true;
    [SerializeField] private Transform lineSource;
    [SerializeField] private Catcher catcher;
    [HideInInspector] public Fish currentFish;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
        lineRenderer.enabled = true;
        fish.PullRodsCount++;
        while (true)
        {
            fish.catchProgress += catchPower * Time.deltaTime;
            if (!fish.gameObject.activeSelf || !catcher.IsCanCatch(fish))
            {
                break;
            }
            if (fish.catchProgress >= fish.health&&!fish.isCatched)
            {
                fish.isCatched = true;
                catcher.GotFish(fish);
                if(fish==null)
                {
                    yield return new WaitForSeconds(1);
                    continue;
                }
                break;
            }
            var distance = Vector3.Distance(lineSource.position, fish.transform.position);
            if (distance > maxDistance)
            {
                fish.PullRodsCount--;
                break;
            }
            lineRenderer.SetPosition(0, lineSource.position);
            lineRenderer.SetPosition(1, fish.transform.position);
            yield return null;
        }
        lineRenderer.enabled = false;
        isFree = true;
        currentFish = null;
    }

    public void StopPull()
    {
        currentFish.PullRodsCount--;
        lineRenderer.enabled = false;
        isFree = true;
        currentFish = null;
        StopAllCoroutines();
    }
}