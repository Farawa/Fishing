using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoatSpeedhData", menuName = "BoatUpdates/BoatSpeedData", order = 5)]
public class BoatSpeedSO : ScriptableObject
{
    public float[] boatSpeed;
}
