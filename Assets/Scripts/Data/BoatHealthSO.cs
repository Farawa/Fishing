using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "BoatUpdates/HealthData", order = 7)]
public class BoatHealthSO : ScriptableObject
{
    public float[] health;
}
