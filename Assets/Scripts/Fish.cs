using UnityEngine;

public class Fish : MonoBehaviour
{
    public float health = 100f;
    public float price = 10f;
    public float weight = 1f;
    public float speed = 3f;

    public float catchProgress;
    public bool isCatched = false;

    [SerializeField] private GameObject progressObject;
    private int pullRodsCount = 0;
    private Rigidbody rigidbody;
    private Vector3 areal = Vector3.one;
    private Vector3 targetPosition = Vector3.zero;

    public int PullRodsCount
    {
        get => pullRodsCount;
        set
        {
            pullRodsCount = value;
            if (pullRodsCount == 0)
                catchProgress = 0;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        isCatched = false;
        PullRodsCount = 0;
        catchProgress = 0;
    }

    public void SetAreal(Vector3 vector)
    {
        areal = vector;
    }

    private void Update()
    {
        if (!IsOnView())
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }
        CheckDestination();
        Move();
        if (PullRodsCount != 0)
            progressObject.SetActive(true);
        else
            progressObject.SetActive(false);
    }

    public void Setup(float fishHealth, float fishPrice, float fishWeight, float fishSpeed, Vector3 fishAreal)
    {
        health = fishHealth;
        price = fishPrice;
        weight = fishWeight;
        speed = fishSpeed;
        areal = fishAreal;
        targetPosition = GetNewTarget();
    }

    private void Move()
    {
        var vector = targetPosition - transform.localPosition;
        vector = vector.normalized*speed;
        rigidbody.velocity = vector;
        transform.LookAt(vector+transform.position);
    }

    private void CheckDestination()
    {
        var difference = transform.localPosition - targetPosition;
        if (Mathf.Abs(difference.x) < 0.1f && Mathf.Abs(difference.z) < 0.1f)
            targetPosition = GetNewTarget();
    }

    private Vector3 GetNewTarget()
    {
        var x = UnityEngine.Random.Range(-areal.x, areal.x);
        var z = UnityEngine.Random.Range(-areal.z, areal.z);
        var y = 0;
        return new Vector3(x, y, z);
    }

    private bool IsOnView()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position);
        var min = 0.05f;
        var max = 1.05f;
        if (point.y < -min || point.x < -min || point.y > max || point.x > max)
            return false;
        return true;
    }
}