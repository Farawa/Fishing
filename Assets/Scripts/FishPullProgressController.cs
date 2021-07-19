using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishPullProgressController : MonoBehaviour
{
    [SerializeField] private Fish fish;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image fillingImage;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        var progress = fish.catchProgress;
        var max = fish.health;
        var percent = progress / max;

        text.text = (int)(percent*100) + "/100";
        fillingImage.fillAmount = percent;

        transform.rotation = camera.transform.rotation;
    }
}
