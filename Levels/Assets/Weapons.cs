using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

    public int currentweapon = 0;
    public GameObject[] weapons;
    private int nrWeapons;

	// Use this for initialization
	void Start () {
        nrWeapons = weapons.Length;
        SwitchWeapon(currentweapon);
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i=1; i <= nrWeapons; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                currentweapon = i - 1;
                SwitchWeapon(currentweapon);
            }
        }
	}

    void SwitchWeapon(int index)
    {
        for (int i=0; i < nrWeapons; i++)
        {
            if (i == index)
            {
                weapons[i].gameObject.SetActive(true);
            } else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
