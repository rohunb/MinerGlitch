using UnityEngine;
using System.Collections;

public class AIAttack : MonoBehaviour {

    private float InitialWaitTime = 1.0f;
    public float weaponCooldown = 1.0f;
    [SerializeField]
    private AIShip owningShip;

	// Use this for initialization
	private IEnumerator Start () {

        yield return new WaitForSeconds(InitialWaitTime);
        StartCoroutine(FireWeapon());
	}
	
    private IEnumerator FireWeapon()
    {
        while (true)
        {
            ///Do Fire Weapon
            //Debug.Log("fire");
            owningShip.EquippedWeapon.FireWeapon(owningShip.Accuracy);

            yield return new WaitForSeconds(weaponCooldown);
        }
    }

    public void SetOwningShip(AIShip newOwner)
    {
        owningShip = newOwner;
    }

	
}
