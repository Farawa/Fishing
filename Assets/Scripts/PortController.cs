using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{
    public static PortController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var boatM = other.GetComponent<BoatManager>();
            boatM.SellAll();
        }
    }
}