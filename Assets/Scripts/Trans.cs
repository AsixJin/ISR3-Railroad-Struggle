using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Trans : MonoBehaviour {

    public Image fader;
    public AudioSource mAudio;
    public int nextLevel;
    public float Timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;

        if(Timer >= 36)
        {
            mAudio.volume -= 0.01f;
            fader.color = new Color(0, 0, 0, fader.color.a + 0.01f);
        }

        if(fader.color.a >= 1 || Input.GetKeyDown(KeyCode.X))
        {
            Application.LoadLevel(nextLevel);
        }
	}
}
