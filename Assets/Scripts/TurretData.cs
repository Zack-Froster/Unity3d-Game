using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData 
{
    public GameObject turretPrefab;
    public int price;
    public GameObject turretUpgradePrefab;
    public int pirceUpgrade;
    public TurretType turretType;
}
public enum TurretType
{
    LaserTurret,
    MissleTurret,
    StandardTurret
}