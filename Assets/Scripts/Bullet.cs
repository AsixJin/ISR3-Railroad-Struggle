using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    #region Members
    public float Timer;
    public int dir = -1;
    private Hero mHero;
    private Rigidbody2D rBody;
    #endregion

    // Use this for initialization
	void Start () {
        rBody = this.gameObject.GetComponent<Rigidbody2D>();
        mHero = GameObject.Find("Hero").GetComponent<Hero>();
        Move(dir);
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if(Timer >= 0.7f)
        {
            Destroy(this.gameObject);
        }
	}

    public void Move(int dir)
    {
        Debug.Log("Is Moving");
        if (dir == 0)
        {
            rBody.velocity = new Vector2(0, 15);
        }
        else if (dir == 1)
        {
            rBody.velocity = new Vector2(15, 0);
        }
        else if (dir == 2)
        {
            rBody.velocity = new Vector2(0, -15);
        }
        else if (dir == 3)
        {
            rBody.velocity = new Vector2(-15, 0);
        }
        else
        {
            Move(Random.Range(0, 4));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "wall" || col.transform.tag == "troll")
        {
            Destroy(this.gameObject);
        }
    }
}
