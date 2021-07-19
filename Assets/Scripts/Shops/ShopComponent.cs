using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oldValue;
    [SerializeField] private TextMeshProUGUI newValue;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Button buyButton;
    private ParameterType type;


    private void Start()
    {
        buyButton.onClick.AddListener(Click);
    }

    private void Click()
    {
        if (PlayerUpgrades.instance.TryImproveParameter(type))
        {
            Setup(PlayerUpgrades.instance.GetShopValues(type));
        }
    }

    public void Setup(ShopValues values)
    {
        nameText.text = values.nameText.ToString();
        oldValue.text = values.currentValue.ToString();
        type = values.type;
        costText.text = values.cost.ToString();
        var isLastLevel = values.currentValue == values.nextValue ? true : false;
        if (isLastLevel)
        {
            arrow.SetActive(false);
            newValue.gameObject.SetActive(false);
            buyButton.enabled = false;
            costText.gameObject.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
            newValue.gameObject.SetActive(true);
            buyButton.enabled = true;
            costText.gameObject.SetActive(true);
            newValue.text = values.nextValue.ToString();
        }
    }
}
