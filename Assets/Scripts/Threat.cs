using System;
using UnityEngine;

[CreateAssetMenu()]
public class Threat : ScriptableObject
{
    public int TimeToSpawn;
    public ThreatPosition[] Positions;
    public GameObject SignalObject;
    public GameObject ThreatObject;
}

[Serializable]
public class ThreatPosition
{
    public Vector2 Position;
    public float Angle;
    public float AngleDiff;
}