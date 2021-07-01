using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float health = 100f;
    public float price = 10f;
    public float weight = 1f;

    private int pullRodsCount = 0;

    private bool isCatched = false;

    public int pullingRodsCount
    {
        get
        {
            return pullRodsCount;
        }
        set
        {
            pullRodsCount = value;
            if (pullRodsCount == 0)
                catchProgress = 0;
        }
    }

    private float progress = 0;
    public float catchProgress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = value;
            if (progress >= health && !isCatched)
            {
                isCatched = true;
                BoatFishing.instance.CatchFish(this);
            }
        }
    }
    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}