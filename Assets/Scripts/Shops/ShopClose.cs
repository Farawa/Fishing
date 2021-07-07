using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class ShopClose : MonoBehaviour
{
    [SerializeField] private GameObject shop;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private void Click()
    {
        shop.gameObject.SetActive(false);
    }
}
