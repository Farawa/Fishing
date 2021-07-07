using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterData", menuName = "ScriptableObjects/ParameterData", order = 1)]
public class ParameterData : ScriptableObject
{
    [SerializeField] private ParameterType parameterType;
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private string labelText;
    [SerializeField] private ValueAndPrice[] array;
    public ParameterType type { get { return parameterType; } }

    public float GetValue(int index)
    {
        return array[index].value;
    }

    public float GetPrice(int index)
    {
        return array[index].price;
    }

    public ShopValues GetStruct()
    {
        var str = new ShopValues();
        str.nameText = labelText;
        str.currentValue = array[currentLevel].value;
        str.type = parameterType;
        str.cost = 0;
        if (currentLevel == array.Length - 1)
        {
            str.nextValue = str.currentValue;
        }
        else
        {
            str.nextValue = array[currentLevel + 1].value;
            str.cost = GetPrice(currentLevel + 1);
        }
        return str;
    }

    public int GetMaxLevel()
    {
        return array.Length;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int value)
    {
        currentLevel = value;
    }
}
