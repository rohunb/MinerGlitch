using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        playerMove = gameObject.GetSafeComponent<PlayerMove>();
        playerAttack = gameObject.GetSafeComponent<PlayerAttack>();
    }
    private void Start()
    {
        InputManager.Instance.OnKeyboardAxisEvent += KeyboardAxisEvent;
        InputManager.Instance.RegisterKeysHold(KeyboardHold, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Space, KeyCode.LeftControl, KeyCode.RightControl);
        InputManager.Instance.RegisterMouseButtonsDown(MouseDown, MouseButton.Left, MouseButton.Right);
    }

    void KeyboardAxisEvent(Vector2 direction)
    {
        playerMove.Move(direction);
    }
    void KeyboardHold(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Alpha1:
                playerAttack.SelectWeapon(Weapon.WeaponType.Beam);
                break;
            case KeyCode.Alpha2:
                playerAttack.SelectWeapon(Weapon.WeaponType.Shotgun);
                break;
            case KeyCode.Alpha3:
                playerAttack.SelectWeapon(Weapon.WeaponType.Orb);
                break;
            case KeyCode.Space:
                playerAttack.FireCurrentWeapon();
                break;
            //case KeyCode.LeftControl:
            //case KeyCode.RightControl:
            //    playerAttack.ActivateBomb();
            //    break;
            default:
                break;
        }
    }
    void MouseDown(MouseButton button)
    {
        if (button == MouseButton.Left)
        {
            playerAttack.FireCurrentWeapon();
        }
        if (button == MouseButton.Right)
        {
            playerAttack.ActivateBomb();
        }
    }
}
