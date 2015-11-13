using UnityEngine;
using System.Collections;

public class Hero : Actor {

    public bool GoBack = false;
    private float reloadTime = 0;
    public int killCount = 0;
    public GameObject trainBuddy;
    public AudioSource buddyReady;
    public AudioSource firing;

	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
        if(killCount == 10)
        {
            killCount += 1;
            buddyReady.Play();
        }

        #region Animations
        if(rBody.velocity == new Vector2(0, 0))
        {
            mAnim.Play("HeroIdle" + direction.ToString());
        }
        else 
        {
            //mAnim.Play("HeroWalk0");
            mAnim.Play("HeroWalk" + direction.ToString());
        }
        #endregion 

        #region Movement Code
        if(!GoBack)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Move(NORTH);
                direction = NORTH;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(EAST);
                direction = EAST;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Move(SOUTH);
                direction = SOUTH;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(WEST);
                direction = WEST;
            }
            else
            {
                Move(STOP);
                //direction = STOP;
            }
        }
        
        #endregion

        #region Shooting code
        if(reloadTime >= 0)
        {
            reloadTime -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && rBody.velocity == new Vector2(0,0))
        {
            if(!firing.isPlaying)
            {
                firing.Play();
            }  
            if(reloadTime <= 0)
            {
                Shoot();
                reloadTime = 0.1f;
            }           
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            if (!firing.isPlaying)
            {
                firing.Play();
            }  
            if (reloadTime <= 0)
            {
                Shoot();
                reloadTime = 0.3f;
            }  
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            firing.Stop();
        }

        //For Summoning Train Buddy
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {   
            if(killCount >= 11)
            {
                SummonTrain();
                killCount -= 11;
            }
        }
        #endregion
    }

    void SummonTrain()
    {
        int track = Random.Range(0, 2);
        if (track == 0) { Instantiate(trainBuddy, new Vector3(-12.5f, 3.7f, 0), Quaternion.identity); }
        else if (track == 1) { Instantiate(trainBuddy, new Vector3(-12.5f, -3.45f, 0), Quaternion.identity); }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "train")
        {
            Health -= 100;
        }
        if (col.transform.tag == "wall")
        {
            GoBack = true;
            ReverseDir();
            Move(direction);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "wall")
        {
            GoBack = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "troll")
        {
            Health -= 1;
        }
        
    }

}
