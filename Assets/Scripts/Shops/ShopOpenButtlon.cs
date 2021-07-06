using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOpenButtlon : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private void Click()
    {
        shop.SetActive(true);
    }
}
