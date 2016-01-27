using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

    public RaycastHit hit;
    public Ray ray;
    private Vector3 touchpos;

    public float radius = 5.0f;
    public float power = 10.0f;

    void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
//            if (Physics.Raycast(ray, out hit, 1000))
//            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 dir = gameObject.transform.position - touchpos;
                hit.transform.SendMessage("Touched", dir);
//            }
        }
    }
	
}

