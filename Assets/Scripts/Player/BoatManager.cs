using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public static BoatManager instance = null;

    [SerializeField] private Catcher catcher;

    public float playerMoney { get { return Preffs.GetFloat("PlayerMoney"); } set { Preffs.SetFloat("PlayerMoney", value); } }
    public float currentHealth { get; private set; } = 100;
    public float CatchPower { get => _catchPower; private set => _catchPower = value; }
    private float _catchPower = 10;
    public float MaxFishCapacity { get => _maxFishCapacity; private set => _maxFishCapacity = value; }
    private float _maxFishCapacity = 100;
    public int RodsCount { get => _rodsCount; private set => _rodsCount = value; }
    private int _rodsCount = 6;
    public int MaxRodPerFish { get => _maxRodsPerFish; private set => _maxRodsPerFish = value; }
    private int _maxRodsPerFish = 2;
    public float BoatSpeed { get => _boatSpeed; private set => _boatSpeed = value; }
    private float _boatSpeed = 5;
    public float MaxCatchDistance { get => _maxCatchDistance; private set => _maxCatchDistance = value; }
    private float _maxCatchDistance = 5;
    public float MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
    private float _maxHealth = 100;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

    private void Start()
    {
        PlayerUpgrades.instance.UpdateParameters();
    }

    public void UpdateImprovements(float power, float capacity, int rods, int rodsPerFish, float speed, float catchDistance, float health)
    {
        _catchPower = power;
        _maxFishCapacity = capacity;
        _rodsCount = rods;
        _maxRodsPerFish = rodsPerFish;
        _boatSpeed = speed;
        _maxCatchDistance = catchDistance;
        _maxHealth = health;
        catcher.SetParameters(power, catchDistance, rodsPerFish, rods);
    }

    public void AddFish(Fish fish)
    {
        BoatStorage.instance.AddFish(fish);
        catcher.CheckFullStorage();
    }

    public void SellAll()
    {
        playerMoney += BoatStorage.instance.moneyCost;
        BoatStorage.instance.SetEmptyStorage();
        currentHealth = _maxHealth;
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
    }
}
