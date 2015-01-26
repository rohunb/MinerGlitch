using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour {

    public enum WeaponType {Invalid, Beam, Orb, Shotgun, Bomb, WeaponCount };
   
    public enum WeaponBitFlags
    {
        Invalid = 0,
        Orb = 1,
        Beam = 1 << 1,
        Shotgun = 1 << 2,
        Bomb = 1 << 3,
        WeaponCount = 15
    };

    [SerializeField]
    protected int WeaponLevel = 1;
    [SerializeField]
    protected int MaxWeaponLevel = 3;
    [SerializeField]
    protected float projectileSpeed = 1.0f;
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    [SerializeField]
    public Vector2 ShotDirection;

    [SerializeField]
    protected float reloadTimer = 1.0f;
    [SerializeField]
    protected int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField]
    protected GameObject ProjectilePrefab;

    protected float currentTimer = 0.0f;
    [SerializeField]
    private int enemyLayer;
    public int EnemyLayer
    {
        get { return enemyLayer; }
        set { enemyLayer = value; }
    }
    //public int EnemyLayer {get; set;}


    /// <summary>
    /// Modifies the scale of the fired projectile
    /// </summary>
    /// <param name="increase">True to increase scale, false to decrease</param>
    /// <param name="multiplier">The amount to apply</param>
    public virtual void ModifyScale(bool increase, float multiplier)
    {
        Vector3 currScale = transform.localScale;

        if(increase)
            transform.localScale.Set(currScale.x * multiplier, 
                currScale.y * multiplier, currScale.z * multiplier);
        else
            transform.localScale.Set(currScale.x / multiplier,
                            currScale.y / multiplier, currScale.z / multiplier);
    }

    public virtual void LevelUp()
    {
        if (WeaponLevel < MaxWeaponLevel)
            WeaponLevel++;
    }

    public virtual void LevelDown()
    {
        if (WeaponLevel > 0)
            WeaponLevel--;
    }

    public virtual void FireWeapon(float accuracy)
    {

    }

    public virtual void OnCollision(GameObject gameObject, GameObject projectile)
    {

    }
}
