using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Data/Weapon/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public float speed;
    public float travelDistance;
    public bool through;
    public GameObject model;
}