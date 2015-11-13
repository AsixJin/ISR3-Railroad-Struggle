using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoTrans : MonoBehaviour {

    //Logo parts
    public SpriteRenderer logo;
    public SpriteRenderer mask;
    public AudioSource noise;
    public Animator shock;
    public Image fader;
    //Members
    public int nextLevel;
    private float timer = 0;
    private Color maskclr;

    void Awake(){
        
    }

	// Use this for initialization
	void Start () {
        maskclr.b = 255;
        maskclr.g = 255;
        maskclr.r = 255;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(fader.color.a.ToString());
        timer += Time.deltaTime;
        mask.color = maskclr;

        if (timer >= 3)
        {
           maskclr.a += 0.005f;
        }
        if(timer >= 7)
        {
            noise.volume -= 0.01f;
            fader.color = new Color(0, 0, 0, fader.color.a + 0.01f);
        }

        if(fader.color.a >= 1)
        {
            Application.LoadLevel(nextLevel);
        }
	}

    public void PlayStatic()
    {
        noise.Play();
    }
}
