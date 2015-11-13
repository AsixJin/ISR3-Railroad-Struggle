using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    #region Constants
    protected readonly int STOP = -1;
    protected readonly int NORTH = 0;
    protected readonly int EAST = 1;
    protected  readonly int SOUTH = 2;
    protected readonly int WEST = 3;
    #endregion

    #region Members
    //Public
    public int Health;
    public float speed;
    public GameObject bullet;
    public bool hasEntered = false;

    public Transform sPos0;
    public Transform sPos1;
    public Transform sPos2;
    public Transform sPos3;
    //Protected
    public int direction;
    public Bullet curBullet;
    protected Rigidbody2D rBody;
    protected Animator mAnim;
    protected SpriteRenderer sRender;
    #endregion

    // Use this for initialization
	public virtual void Start () {
        rBody = this.gameObject.GetComponent<Rigidbody2D>();
        mAnim = this.gameObject.GetComponent<Animator>();
        sRender = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if(Health <= 0)
        {
            Death();
        }
	}

    //By taking an int value you can move player
    protected void Move(int dir){
        if(dir == NORTH)
        {
            rBody.velocity = new Vector2(0, speed);
        }
        if(dir == EAST)
        {
            rBody.velocity = new Vector2(speed, 0);
        }
        if(dir == SOUTH)
        {
            rBody.velocity = new Vector2(0, -speed);
        }
        if(dir == WEST)
        {
            rBody.velocity = new Vector2(-speed, 0);
        }
        if(dir == STOP)
        {
            rBody.velocity = new Vector2(0, 0);
        }
    }

    //Method for shooting bullets
    protected void Shoot(){
        GameObject obj = this.gameObject;
        if (direction == NORTH) { obj = (GameObject)Instantiate(bullet, sPos0.position, Quaternion.identity); }
        else if (direction == EAST) { obj = (GameObject)Instantiate(bullet, sPos1.position, Quaternion.identity); }
        else if (direction == SOUTH) { obj = (GameObject)Instantiate(bullet, sPos2.position, Quaternion.identity); }
        else if (direction == WEST) { obj = (GameObject)Instantiate(bullet, sPos3.position, Quaternion.identity); }
        
        curBullet = obj.GetComponent<Bullet>();

        curBullet.dir = direction;
    }

    //Method for reversing direction
    protected void ReverseDir()
    {
        if (direction == NORTH) { direction = SOUTH; }
        else if (direction == SOUTH) { direction = NORTH; }
        else if (direction == EAST) { direction = WEST; }
        else if (direction == WEST) { direction = EAST; }
    }

    //Method for when actor dies
    public virtual void Death()
    {
        Destroy(this.gameObject);
    }
}
