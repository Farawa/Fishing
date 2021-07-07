using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentMoneyText;
    [SerializeField] private TextMeshProUGUI capacityText;
    [SerializeField] private TextMeshProUGUI fishCostText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image fill;

    private void Update()
    {
        var currentCapacity = BoatStorage.instance.currentWeight;
        var fishCost = BoatStorage.instance.moneyCost;
        var maxCapacity = BoatManager.instance.maxFishCapacity;
        var currentMoney = BoatManager.instance.playerMoney;
        var maxHealth = BoatManager.instance.maxHealth;
        var currentHealth = BoatManager.instance.currentHealth;

        capacityText.text = currentCapacity + "/" + maxCapacity;
        fishCostText.text = fishCost.ToString();
        currentMoneyText.text = currentMoney.ToString();
        healthText.text = currentHealth + "/" + maxHealth;
        fill.fillAmount = currentCapacity / maxCapacity;
    }
}
