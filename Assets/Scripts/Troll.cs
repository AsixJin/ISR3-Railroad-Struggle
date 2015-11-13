using UnityEngine;
using System.Collections;

public class Troll : Actor
{

    #region Members
    private Hero mHero;
    public AudioSource mGroan;
    public float heIsDead; 
    #endregion

    public override void Start(){
        base.Start();
        if (Application.loadedLevel == 1) { mHero = GameObject.Find("Hero").GetComponent<Hero>(); speed = Random.Range(1, 2.01f); }
        
        sRender.color = new Color(Random.Range(0.10f, 1.1f), Random.Range(0.10f, 1.1f), Random.Range(0.10f, 1.1f));
    }

    // Update is called once per frame
	public override void Update ()
    {
        if(Health <= 0)
        {
            Death();
        }

        #region Code for level
        if (Application.loadedLevel == 1)
        {
            #region Animations
            if (true)
            {
                mAnim.Play("TrollIdle" + direction.ToString());
            }
            #endregion

            #region Movement
            Move(direction);
            #endregion
        }
        else
        {
            direction = 3;
            mAnim.Play("TrollIdle1");
            Move(direction);
        }
        #endregion

    }

    public override void Death(){
        base.Death();

    }

    IEnumerator DecideDir()
    {
       while(true)
       {
           if(!hasEntered)
           {

           }
           else
           {
               int dir = Random.Range(0, 2);
               if (dir == 0)
               {
                   if (mHero.transform.position.y > transform.position.y) { direction = 0; }
                   else if (mHero.transform.position.y < transform.position.y) { direction = 2; }
               }
               else if (dir == 1)
               {
                   if (mHero.transform.position.x < transform.position.x) { direction = 3; }
                   else if (mHero.transform.position.x > transform.position.x) { direction = 1; }
               }
           }
           yield return new WaitForSeconds(3f);
       }
    }

    #region Collsion Methods
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "pBullet")
        {
            Health -= 1;
            if (Health <= 0) { if (mHero.killCount <= 10) { mHero.killCount += 1; } }
        }
        if (col.transform.tag == "wall")
        {
            if(Application.loadedLevel == 1)
            {
                if (hasEntered)
                {
                    Debug.Log("Has entered");
                    StopCoroutine(DecideDir());
                    ReverseDir();
                }
                else
                {
                    hasEntered = true;
                }
            }
            else
            {
                this.gameObject.transform.position = new Vector3 (9.50f, this.gameObject.transform.position.y, 0);
            }
        }
        if (col.transform.tag == "train")
        {
            Health -= 100;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        if (col.transform.tag == "wall")
        {
            Debug.Log("Has exit");
            StartCoroutine(DecideDir());
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.transform.tag == "spawn")
        {
            direction = col.GetComponent<SpawnPoint>().GoToward;
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.transform.tag == "troll")
        {
            ReverseDir();
        }
    }
    #endregion

}
