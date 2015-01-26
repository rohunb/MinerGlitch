using UnityEngine;
using System.Collections;

public class BitMaskAttribute : PropertyAttribute
{
    public System.Type propType;
    public BitMaskAttribute(System.Type aType)
    {
        propType = aType;
    }
}

public class EnemyConfig : MonoBehaviour
{

    [BitMask(typeof(AIMove.MoveFlags))]
    public int PossibleMoves;

    [BitMask(typeof(Weapon.WeaponBitFlags))]
    public int PossibleWeapons;

    public float minSinAmplitude = 0.0f;
    public float maxSinAmplitude = 0.0f;

    public float minSinFrequency = 0.0f;
    public float maxSinFrequency = 0.0f;

    public bool applySinHorizontal = false;
    public bool applySinVertical = false;

    public float minRotationAngle = 0.0f;
    public float maxRotationAngle = 0.0f;

    public Vector2 moveDirection = new Vector2(0.0f, 0.0f);

    public bool ValidMoveOption(AIMove.MoveTypes option)
    {
        int compareVal = (int)AIMove.MoveFlags.All;
        //Debug.Log("Options: " + (AIMove.MoveTypes)option);
        switch (option)
        {
            case AIMove.MoveTypes.Stationary:
                if (PossibleMoves == 0)
                    return true;
                else
                    return false;
                break;
            case AIMove.MoveTypes.Linear:
                compareVal = (int)AIMove.MoveFlags.Linear;
                break;
            case AIMove.MoveTypes.Sin:
                compareVal = (int)AIMove.MoveFlags.Sin;
                break;
            case AIMove.MoveTypes.Rotation:
                compareVal = (int)AIMove.MoveFlags.Rotation;
                break;
            case AIMove.MoveTypes.Tracking:
                compareVal = (int)AIMove.MoveFlags.Tracking;
                break;
            case AIMove.MoveTypes.Count:
                compareVal = (int)AIMove.MoveFlags.All;
                break;
            default:
                break;
        }
        //Debug.Log("CompVal: " + compareVal.ToString());
        return ((int)PossibleMoves & (int)compareVal) == (int)compareVal;
    }

    public bool ValidWeaponOption(Weapon.WeaponType option)
    {
        int compareVal = (int)Weapon.WeaponBitFlags.WeaponCount;

        switch (option)
        {
            case Weapon.WeaponType.Orb:
                compareVal = (int)Weapon.WeaponBitFlags.Orb;
                break;
            case Weapon.WeaponType.Beam:
                compareVal = (int)Weapon.WeaponBitFlags.Beam;
                break;
            case Weapon.WeaponType.Shotgun:
                compareVal = (int)Weapon.WeaponBitFlags.Shotgun;
                break;
            case Weapon.WeaponType.Bomb:
                compareVal = (int)Weapon.WeaponBitFlags.Bomb;
                break;
            case Weapon.WeaponType.WeaponCount:
                compareVal = (int)Weapon.WeaponBitFlags.WeaponCount;
                break;
            default:
                break;
        }

        return ((int)PossibleWeapons & compareVal) == compareVal;
    }

    public bool UseLinearMove()
    {
        return ((int)PossibleMoves & (int)AIMove.MoveFlags.Linear) == (int)AIMove.MoveFlags.Linear;
    }

    public bool UseRotationalMove()
    {
        return ((int)PossibleMoves & (int)AIMove.MoveFlags.Rotation) == (int)AIMove.MoveFlags.Rotation;
    }

    public bool UseSinMove()
    {
        return ((int)PossibleMoves & (int)AIMove.MoveFlags.Sin) == (int)AIMove.MoveFlags.Sin;
    }

    public bool UseStationaryMove()
    {
        return PossibleMoves == (int)AIMove.MoveFlags.Stationary;
    }

    public bool UseTrackingMove()
    {
        return ((int)PossibleMoves & (int)AIMove.MoveFlags.Tracking) == (int)AIMove.MoveFlags.Tracking;
    }

    public bool UseOrb()
    {
        return ((int)PossibleWeapons & (int)Weapon.WeaponBitFlags.Orb) == (int)Weapon.WeaponBitFlags.Orb;
    }

    public bool UseBeam()
    {
        return ((int)PossibleWeapons & (int)Weapon.WeaponBitFlags.Beam) == (int)Weapon.WeaponBitFlags.Beam;
    }

    public bool UseShotgun()
    {
        return ((int)PossibleWeapons & (int)Weapon.WeaponBitFlags.Shotgun) == (int)Weapon.WeaponBitFlags.Shotgun;
    }

    public bool UseBomb()
    {
        return ((int)PossibleWeapons & (int)Weapon.WeaponBitFlags.Bomb) == (int)Weapon.WeaponBitFlags.Bomb;
    }

    public bool UseNoWeapons()
    {
        return PossibleWeapons == (int)Weapon.WeaponBitFlags.Invalid;
    }
}
