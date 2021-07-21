using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverBoat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Transform port;
    private float money = 0;
    [HideInInspector] public float maxWeight;

    private bool isFull = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = BoatManager.instance.transform;
        port = PortController.instance.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isFull && BoatStorage.instance.currentWeight != 0)
        {
            money = BoatStorage.instance.GetMoneyFromFish(maxWeight);
            isFull = true;
        }
        if (other.tag == "Port"&&isFull)
        {
            SellAll();
            print("sale");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void SellAll()
    {
        BoatManager.instance.playerMoney += money;
        money = 0;
        isFull = false;
    }

    private void Update()
    {
        if (!isFull && BoatStorage.instance.currentWeight != 0)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(port.position);
        }
    }
}