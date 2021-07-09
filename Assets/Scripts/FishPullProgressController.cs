using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishPullProgressController : MonoBehaviour
{
    [SerializeField] private Fish fish;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image fillingImage;

    private void Update()
    {
        var progress = fish.catchProgress;
        var max = fish.health;
        var percent = progress / max;

        text.text = (int)(percent*100) + "/100";
        fillingImage.fillAmount = percent;

        transform.LookAt(Camera.main.transform);
        var rotateVector = new Vector3(0, -180, 0);
        transform.Rotate(rotateVector);
    }
}
