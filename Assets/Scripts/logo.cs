using UnityEngine;
using System.Collections;

public class logo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x >= -0.2f)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime*2f, 3.1f, -1);
        }   
	}
}
