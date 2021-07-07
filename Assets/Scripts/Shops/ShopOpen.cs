using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private ShopController shop;
    [SerializeField] private ParameterType[] types;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private void Click()
    {
        shop.gameObject.SetActive(true);
        shop.SetupShop(types);
    }
}
