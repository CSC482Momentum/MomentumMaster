using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class powerupList : MonoBehaviour {
    public List<powerup> powerups;
    public int maxNumber = 2; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool add(powerup pickup)
    {
        if(powerups.Count < maxNumber)
        {

            return true;
        } else
        {
            return false;
        }


    }
}
