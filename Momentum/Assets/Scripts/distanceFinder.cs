using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Assets;
using UnityEngine.UI;

public class distanceFinder: MonoBehaviour {
    public RigidbodyFirstPersonController fpsc;
    public WeaponsManager weapons;
    private RawImage image;
    // Use this for initialization
    void Start () {
        image = gameObject.GetComponent<RawImage>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 fwd = fpsc.cam.transform.TransformDirection(Vector3.forward);
        var currentWeapon = weapons.weapons[weapons.currentweapon].GetComponent<Weapon>();
        var withinPrimary = Physics.Raycast(transform.position, fwd, out hit, currentWeapon.getPrimaryRange());
        var withinSecondary = Physics.Raycast(transform.position, fwd, out hit, currentWeapon.getSecondaryRange());
        if (withinPrimary && withinSecondary)
        {
            if (hit.collider.tag == "Hook")
            {
                image.color = Color.magenta;
            } else
            {
                image.color = Color.cyan;
            }
        } else if(withinPrimary)
        {
            image.color = Color.blue;
        } else if (withinSecondary && hit.collider.tag == "Hook")
        {
            image.color = Color.green;
        } else
        {
            image.color = Color.red;
        }
    }
}
