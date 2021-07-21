using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatcherBot : MonoBehaviour
{
    [SerializeField] private RodBot rod;
    [SerializeField] private float maxCatchDistance = 4;
    private SphereCollider sphere;
    public float catchPower;

    private Fish targetFish;
    private NavMeshAgent agent;

    public bool isCatching = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sphere = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateTargetsCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateTargetsCoroutine()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            if (targetFish == null || !targetFish.gameObject.activeSelf)
            {
                targetFish = SpawnersHolder.instance.GetFish();
                targetFish.isCatched = true;
            }
            agent.SetDestination(targetFish.transform.position);

            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fish")
        {
            if (other.gameObject != targetFish.gameObject||isCatching) return;
            isCatching = true;
            rod.PullFish(targetFish, sphere.radius, catchPower);
            print("start pool");
        }
    }

    public void GotFish(Fish fish)
    {
        BoatManager.instance.playerMoney += fish.price;
        fish.gameObject.SetActive(false);
        //TODO выпрыгивание монетки
    }
}