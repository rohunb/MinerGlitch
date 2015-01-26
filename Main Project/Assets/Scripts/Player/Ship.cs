using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private Material glitchMat;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    protected GameObject ShipShield;
    [SerializeField]
    protected float Health = 1.0f;
    [SerializeField]
    protected float ShieldPower = 1.0f;
    [SerializeField]
    protected bool ShieldOn = false;
    [SerializeField]
    protected float accuracy = 0.0f;
    public float Accuracy
    {
        get { return accuracy; }
    }

    [SerializeField]
    protected Vector2 moveSpeed = new Vector2(1.0f, 1.0f);
    public Vector2 MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    [SerializeField]
    protected float rotationSpeed = 0.0f;
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }
    [SerializeField]
    private float glitchEffectDuration = 0.5f;
    [SerializeField]
    private Vector2 allowedScaleRange = new Vector2(1.0f / 3.0f, 3.0f);
    [SerializeField]
    protected Vector2 allowedSpeedRange = new Vector2(1.0f / 2.0f, 2.0f);
    [SerializeField]
    private Vector2 allowedAccuracyRange = new Vector2(-30.0f, 30.0f);
    [SerializeField]
    private Vector2 allowedProjectileScaleRange = new Vector2(1.0f / 3.0f, 3.0f);

    public Weapon[] Weapons = new Weapon[(int)Weapon.WeaponType.WeaponCount];

    [SerializeField]
    protected Weapon equippedWeapon;
    public Weapon EquippedWeapon
    {
        get { return equippedWeapon; }

    }
    public float ProjectileSize { get; private set; }
    private Material originalMat;
    protected List<Effect> activeEffects;
    protected Transform trans;
    private Vector3 defaultScale;

    public virtual void ApplyDamage(float damage)
    {
        if (ShieldOn)
        {
            damage -= ShieldPower;

            if (damage < 0.0f)
                damage = 0.0f;
        }

        Health -= damage;

        if (Health <= 0.0f)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void SetEquippedWeapon(Weapon.WeaponType weaponToEquip)
    {
        if (weaponToEquip == Weapon.WeaponType.WeaponCount)
            return;

        equippedWeapon = Weapons[(int)weaponToEquip];
    }

    protected void UpdateWeaponDirection(Vector2 newDir)
    {
        equippedWeapon.ShotDirection = newDir;
    }

    public virtual void ModifySpeed(float multiplier)
    {

    }
    public virtual void SetDefaultSpeed()
    {

    }
    public virtual void ModifyScale(float multiplier)
    {
        Vector3 newScale = trans.localScale * multiplier;
        if (newScale.x < defaultScale.x)
        {
            if (defaultScale.x / newScale.x > 3.0f)
            {
                trans.localScale = defaultScale / 3.0f;
            }
            else
            {
                trans.localScale = newScale;
            }
        }
        else
        {
            if (newScale.x / defaultScale.x > 3.0f)
            {
                trans.localScale = defaultScale * 3.0f;
            }
            else
            {
                trans.localScale = newScale;
            }
        }
    }
    
    public virtual void SetDefaultScale()
    {
        trans.localScale = defaultScale;
    }
    public virtual void ModifyAccuracy(float modifier)
    {
        float newAcc = accuracy + modifier;
        if (newAcc < allowedAccuracyRange.x)
        {
            newAcc = allowedAccuracyRange.x;
        }
        else if (newAcc > allowedAccuracyRange.y)
        {
            newAcc = allowedAccuracyRange.y;
        }
        accuracy = newAcc;
    }
    public virtual void SetDefaultAccuracy()
    {
        accuracy = 0.0f;
    }
    public virtual void ModifyProjScale(float multiplier)
    {
        float newProjScale = ProjectileSize * multiplier;
        if(newProjScale<allowedProjectileScaleRange.x)
        {
            newProjScale = allowedProjectileScaleRange.x;
        }
        else if(newProjScale > allowedProjectileScaleRange.y)
        {
            newProjScale = allowedProjectileScaleRange.y;
        }
        ProjectileSize = newProjScale;
    }
    public virtual void SetDefaultProjScale()
    {
        ProjectileSize = 1.0f;
    }
    public void AddEffect(Effect effect)
    {
        activeEffects.Add(effect);
        StartCoroutine(SetGlitchEffect());
        if (effect == null)
        {
            Debug.LogError("effect null");
        }
        effect.Execute(this);
    }
    
    public IEnumerator SetGlitchEffect()
    {
        if (spriteRenderer)
        {
            spriteRenderer.material = glitchMat;
        }
        else
        {
            yield return null;
        }
        yield return new WaitForSeconds(glitchEffectDuration);
        if (spriteRenderer)
        {
            spriteRenderer.material = originalMat;
        }
        else
        {
            yield return null;
        }
    }
    protected virtual void Awake()
    {
        activeEffects = new List<Effect>();
        originalMat = spriteRenderer.material;
        trans = transform;
        defaultScale = trans.localScale;
        ProjectileSize = 1.0f;

    }
    protected virtual void Start()
    {

    }

}
