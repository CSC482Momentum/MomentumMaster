using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Assets;

public class WeaponsManager : MonoBehaviour {

    public int currentweapon = 0;
    public Weapon[] weapons;
    private int nrWeapons;

	// Use this for initialization
	void Start () {
        nrWeapons = weapons.Length;
        SwitchWeapon(currentweapon);
	}
	
	// Update is called once per frame
	void Update () {
//	    for (int i=1; i <= nrWeapons; i++)
//        {
//            if (Input.GetKeyDown("" + i))
//            {
//                currentweapon = i - 1;
//                SwitchWeapon(currentweapon);
//            }
            
//        }
        if(CrossPlatformInputManager.GetButtonDown("Weapon1"))
        {
            currentweapon = 0;
            SwitchWeapon(currentweapon);
        } else if (CrossPlatformInputManager.GetButtonDown("Weapon2"))
        {
            currentweapon = 1;
            SwitchWeapon(currentweapon);
        } else if (CrossPlatformInputManager.GetButtonDown("Switch"))
        {
            currentweapon++;
            if (currentweapon >= nrWeapons)
            {
                currentweapon = 0;
            }
            SwitchWeapon(currentweapon);

        }
    }

    void SwitchWeapon(int index)
    {
        for (int i=0; i < nrWeapons; i++)
        {
            if (i == index)
            {
                //weapons[i].gameObject.SetActive(true);
                weapons[i].model.gameObject.SetActive(true);
                weapons[i].enabled = true;
            } else
            {
                //weapons[i].gameObject.SetActive(false);
                weapons[i].model.gameObject.SetActive(false);
                weapons[i].enabled = false;

            }
        }
    }
}
