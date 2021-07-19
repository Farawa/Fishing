using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotsShop : MonoBehaviour
{
    public static BotsShop instance = null;

    private List<GameObject> delivers = new List<GameObject>();
    private List<GameObject> catchers = new List<GameObject>();

    private int maxDeliversCount;
    private float deliversCapacity;
    private int maxCatchersCount;
    private float catchersPower;

    [SerializeField] private Transform botsParent;
    [SerializeField] private GameObject deliverPrefab;
    [SerializeField] private GameObject catcherPrefab;
    [SerializeField] private GameObject openButtonParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
        openButtonParent.SetActive(false);
    }

    public void UpdateParameters(int countDelivers, float deliversCapacity, int catchersCount, float catchersPower)
    {
        maxDeliversCount = countDelivers;
        this.deliversCapacity = deliversCapacity;
        maxCatchersCount = catchersCount;
        this.catchersPower = catchersPower;

        if (delivers.Count < maxDeliversCount)
            SpawnBots(maxDeliversCount - delivers.Count, false);

        if (catchers.Count < maxCatchersCount)
            SpawnBots(maxCatchersCount - catchers.Count, true);

        for (int i = 0; i < delivers.Count; i++)
            delivers[i].GetComponent<DeliverBoat>().maxWeight = deliversCapacity;

        for (int i = 0; i < catchers.Count; i++)
            catchers[i].GetComponent<CatcherBot>().catchPower = catchersPower;
    }

    private void SpawnBots(int count, bool isCatcher)
    {
        for (int i = 0; i < count; i++)
        {
            if (isCatcher)
                catchers.Add(Instantiate(catcherPrefab, botsParent));
            else
                delivers.Add(Instantiate(deliverPrefab, botsParent));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            openButtonParent.gameObject.SetActive(true);
            openButtonParent.transform.rotation = Camera.main.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            openButtonParent.gameObject.SetActive(false);
        }
    }
}
