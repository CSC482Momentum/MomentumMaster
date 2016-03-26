using UnityEngine;
using System.Collections;

public abstract class powerup : MonoBehaviour
{
    public float activeTime;
    private bool triggered;
    public float timeStamp;
    // Use this for initialization
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// This is caused when this object collides with another object. 
    /// Need to make sure that that object is a player, so that the player can pick it up. This needs to add... maybe this object to the player? So that the player can benfit from the powerup. 
    /// Then, this object needs to destory the overall powerup game object (or, disable it?... need to figure out how to respawn the powerups). 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if(successfullyAddToPowerupList(other))// we have successfully added this to the list of powerups that the object should have.... which, actually covers us for the case that the object doesn't even have a list (or isn't a player).
        {


        }
    }

    private bool successfullyAddToPowerupList(Collider other)
    {
        var temp = other.GetComponent<powerupList>();
        if (temp != null && temp.add(this)) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public bool runEffect(GameObject temp)
    {
        if (!triggered)
        {
            triggered = true;
            timeStamp = Time.time + activeTime;
        }
        if (timeStamp >= Time.time)
        {
            effect(temp); //we want to use the effect of the powerup on our gameobject
        }
        else if (triggered == true)
        { // this means that the powerup has been triggered and we have reached the end of it's duration
            reset(temp); // we want to reset the effects of the powerup on the gameobject
            return true; // return true because the effect has finished.
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public abstract void effect(GameObject temp);
    public abstract void reset(GameObject temp);

}
