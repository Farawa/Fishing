using System.Collections;
using UnityEngine;

public class RodBot : MonoBehaviour
{
    [SerializeField] private Transform lineSource;
    [SerializeField] private CatcherBot catcher;
    private Transform catcherTransform;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        catcherTransform = catcher.transform;
        lineRenderer.enabled = false;
    }
    public void PullFish(Fish target, float maxDistance, float catchPower)
    {
        StartCoroutine(PullProcess(target, maxDistance, catchPower));
    }

    private IEnumerator PullProcess(Fish fish, float maxDistance, float catchPower)
    {
        lineRenderer.enabled = true;
        while (true)
        {
            var distance = Vector3.Distance(catcherTransform.position, fish.transform.position)-1;
            if (!fish.gameObject.activeSelf || distance > maxDistance) 
                break;
            fish.catchProgress += catchPower * Time.deltaTime;

            if (fish.catchProgress >= fish.health)
            {
                catcher.GotFish(fish);
                break;
            }
            
            lineRenderer.SetPosition(0, lineSource.position);
            lineRenderer.SetPosition(1, fish.transform.position);
            yield return null;
        }
        print("stop pull");
        catcher.isCatching = false;
        lineRenderer.enabled = false;
    }
}