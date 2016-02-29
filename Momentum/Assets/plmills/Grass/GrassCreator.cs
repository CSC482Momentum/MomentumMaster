using UnityEngine;
using System.Collections;

public class GrassCreator : MonoBehaviour {

	public GameObject grassSprite;

	public float density = 1;

	public int width;

	public int height;

	void Start () {

		//grassSprite.GetComponent<SpriteRenderer>().color = Color.black;

		//BoxCollider boxCollider = (BoxCollider)GetComponent(typeof(BoxCollider));

		Vector3 positional = transform.position;

		//positional.y += .2f;

		for(int i = 0; i < width; i++){

			positional.z = transform.position.z + density * i;

			positional.x = transform.position.x;

			for(int j = 0; j < height; j++){
				
				positional.x += density;

				Instantiate(grassSprite, positional, transform.rotation);
			}
		}

		Object.Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
