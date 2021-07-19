using UnityEngine;

public class Catcher : MonoBehaviour
{
    [SerializeField] private Rod[] rods;
    [SerializeField] private SphereCollider CatcherCollider;

    public float catchPower;
    public float maxCatchDistance;
    public int maxRodsPerFish;

    public void SetCatchDistance(float value)
    {
        CatcherCollider.radius = value;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            var fish = collision.gameObject.GetComponent<Fish>();
            if (fish.PullRodsCount < maxRodsPerFish && IsCanCatch(fish)&&!fish.isCatched)
            {
                var rod = GetFreeRod();
                if (rod != null)
                {
                    rod.PullFish(fish, maxCatchDistance, catchPower);
                }
            }
            CheckFullStorage();
        }
    }

    public void SetParameters(float catchPower, float maxCatchDistance, int maxRodsPerFish, int rodsCount)
    {
        this.catchPower = catchPower;
        this.maxCatchDistance = maxCatchDistance;
        this.maxRodsPerFish = maxRodsPerFish;
        SetRodsCount(rodsCount);
    }

    public bool IsCanCatch(Fish fish)
    {
        return BoatStorage.instance.currentWeight + fish.weight <= BoatManager.instance.MaxFishCapacity ? true : false;
    }

    public void SetRodsCount(int value)
    {
        for (int i = 0; i < value; i++)
        {
            rods[i].gameObject.SetActive(true);
        }
        for (int i = value; i < rods.Length; i++)
        {
            rods[i].gameObject.SetActive(false);
        }
    }

    public void CheckFullStorage()
    {
        var isHaveOnePullRod = false;
        foreach (var rod in rods)
        {
            if (rod.gameObject.activeSelf && !rod.isFree)
            {
                isHaveOnePullRod = true;
            }
        }
        if (!isHaveOnePullRod)
        {
            //TODO
            print("Хранилище полное");
        }
    }

    private Rod GetFreeRod()
    {
        foreach (Rod rod in rods)
        {
            if (rod.isFree && rod.gameObject.activeSelf)
                return rod;
        }
        return null;
    }

    public void GotFish(Fish fish)
    {
        BoatManager.instance.AddFish(fish);
        fish.gameObject.SetActive(false);
    }
}
