using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum WeaponType { Beam = 0, Shotgun = 1, PulseBall = 2 }

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHardPoint;

    private Weapon equippedWeapon;
    public Weapon EquippedWeapon
    {
        get { return equippedWeapon; }

    }
    public Weapon[] weapons = new Weapon[(int)Weapon.WeaponType.WeaponCount];

    private Transform trans;

    public void SelectWeapon(Weapon.WeaponType type)
    {
        //Debug.Log("Select weapon " + type.ToString());
        //Debug.Log("weapon index : " + (int)type);
        equippedWeapon = weapons[(int)type - 1];
        //Debug.Log("selected " + equippedWeapon.name);
    }
    public void FireCurrentWeapon()
    {
        //Debug.Log("Fire current weapon");
        equippedWeapon.FireWeapon(1.0f);
    }

    public void ActivateBomb()
    {
        weapons[(int)Weapon.WeaponType.Bomb - 1].FireWeapon(1.0f);
    }
    public void Init(Weapon[] weaponPrefabs)
    {
        trans = transform;
           
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            //Debug.Log("Length: " + weaponPrefabs.Length);
            weapons[i] = Instantiate(weaponPrefabs[i]) as Weapon;
            weapons[i].transform.SetParent(weaponHardPoint, false);
            weapons[i].EnemyLayer = TagsAndLayers.EnemyShipLayer;
            weapons[i].ShotDirection = transform.right;

            if(weapons[i] is Beam)
            {
                ((Beam)weapons[i]).Initialize(50);
            }
        }

        equippedWeapon = weapons[(int)Weapon.WeaponType.Shotgun-1];
    }
}
