using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject componentPrefab;

    public void SetupShop(ParameterType[] types)
    {
        ClearContent();

        foreach (var type in types)
        {
            var t = Instantiate(componentPrefab, content);
            var str = PlayerUpgrades.instance.GetShopValues(type);
            t.GetComponent<ShopComponent>().Setup(str);
        }
        UpdateContentSize();
    }

    private void ClearContent()
    {
        if (content.childCount == 0) return;
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private void UpdateContentSize()
    {
        var grid = content.GetComponent<GridLayoutGroup>();
        var size = content.sizeDelta;
        size.y = grid.padding.top + grid.padding.bottom + grid.cellSize.y * content.childCount + grid.spacing.y * (content.childCount - 1);
        content.sizeDelta = size;
    }
}
