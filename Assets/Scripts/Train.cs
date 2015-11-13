using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {

    //Members
    private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
        rBody = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Application.loadedLevel == 1)
        {
            rBody.velocity = new Vector2(15, 0);

            if (transform.position.x >= 15)
            {
                Destroy(this.gameObject);
            }
        }       
	}
}
