using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var boatM = other.GetComponent<BoatManager>();
            boatM.SellAll();
        }
    }
}