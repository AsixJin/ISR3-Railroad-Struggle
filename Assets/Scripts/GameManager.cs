using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    #region Constants
    public readonly int DEBUG = -2;
    public readonly int ENDLESS = -1;
    public readonly int ONEMINGAME = 0;
    public readonly int THREEMINGAME = 1;
    public readonly int FIVEMINGAME = 2;
    #endregion

    #region Actors
    public Hero mHero;
    public Light theSun;
    public GameObject Troll;

    public Slider mTimer;
    public Slider mHBar;
    public Text mMin;
    public Text mSec;
    public Image mPanel;
    public Text Info;

    public Button but1;
    public Button but2;
    private Text tBut2;
    public Button but3;
    private Text tBut3;
    public Button but4;
    private Text tBut4;
    #endregion

    #region Members
    public bool GameOver = false;
    public int maxBeaten;

    public float playTimer = 0;
    public float totalMinutes = 0;
    public float restartTimer = 0;

    public int maxTime;

    public int gameType = -2;
    public float spawnRate;
    public int numToSpawn;
    #endregion


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

       
    }

    void Start()
    {
        SetButtons();
    }

    //Called when a level is loaded
	void OnLevelWasLoaded() {
        restartTimer = 0;
        totalMinutes = 0;
        playTimer = 0;
        GameOver = false;
        if (Application.loadedLevel == 1)
        {
            theSun = GameObject.Find("spotlight").GetComponent<Light>();
            mHero = GameObject.Find("Hero").GetComponent<Hero>();
            mTimer = GameObject.Find("Timer").GetComponent<Slider>();
            mHBar = GameObject.Find("hpBar").GetComponent<Slider>();
            mMin = GameObject.Find("minCounter").GetComponent<Text>();
            mSec = GameObject.Find("secCounter").GetComponent<Text>();
            Info = GameObject.Find("Info").GetComponent<Text>();
            mPanel = GameObject.Find("Panel").GetComponent<Image>();
            mPanel.gameObject.SetActive(false);
            Info.gameObject.SetActive(false);
            if(gameType == ENDLESS || gameType == DEBUG)
            {
                mTimer.gameObject.SetActive(false);
            }
            SpawnVariableControl();
            StartCoroutine(MasterSpawner(Troll));
        }
        else
        {
            SetButtons();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        #region Level Code
        if (Application.loadedLevel == 1)
        {
            if(gameType != ENDLESS || gameType != DEBUG)
            {
                theSun.intensity = ((float)totalMinutes + playTimer/60)/(float)maxTime;
                mTimer.value = ((float)totalMinutes + playTimer / 60) / (float)maxTime;
            }
            mSec.text = playTimer.ToString("00");
            mMin.text = totalMinutes.ToString();
            mHBar.value = mHero.Health;
            

            #region Debug Code
#if UNITY_EDITOR
            if (gameType == DEBUG)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) { Spawn0(Troll, 10); }
                if (Input.GetKeyDown(KeyCode.Alpha2)) { Spawn1(Troll, 10); }
                if (Input.GetKeyDown(KeyCode.Alpha3)) { Spawn2(Troll, 10); }
                if (Input.GetKeyDown(KeyCode.Alpha4)) { Spawn3(Troll, 10); }

                if (Input.GetKeyDown(KeyCode.Alpha6)) { StartCoroutine(MasterSpawner(Troll)); }
                if (Input.GetKeyDown(KeyCode.Alpha7)) { StopAllCoroutines(); }
            }
            
#endif
            #endregion

            #region Timer Code
            if (!GameOver) { playTimer += Time.deltaTime; }
            
            if (playTimer >= 60)
            {
                totalMinutes += 1;
                playTimer -= 60;
            }

            //Spawn Variable Controls
            SpawnVariableControl();

            if (gameType != ENDLESS || gameType != DEBUG)
            {
                if (totalMinutes >= maxTime)
                {
                    EndGame();
                }
            }
            #endregion

            #region Hero's Death
            if (mHero == null)
            {
                restartTimer += Time.deltaTime;
            }

            if (restartTimer >= 3)
            {
                EndGame();
            }
            #endregion

            if(Input.GetKeyDown(KeyCode.H))
            {
                if(GameOver)
                {
                    Application.LoadLevel(2);
                }
            }
        }
        #endregion

        if (Application.loadedLevel == 2)
        {
            if(Input.GetKeyDown(KeyCode.Home))
            {
                EnableButtons();
            }
        }

    }

    void EnableButtons()
    {
        but2.interactable = true;
        but3.interactable = true;
        but4.interactable = true;

        tBut2.text = "Sunrise in\n3 minute";
        tBut3.text = "Sunrise in\n5 minute";
        tBut4.text = "Sunrise in\n??? minute";

            but2.onClick.AddListener(() => { Button2(); });
            but3.onClick.AddListener(() => { Button3(); });
            but4.onClick.AddListener(() => { Button4(); });
        
    }

    void SetButtons()
    {
        but1 = GameObject.Find("Button").GetComponent<Button>();
        but2 = GameObject.Find("Button 1").GetComponent<Button>();
        tBut2 = GameObject.Find("TB2").GetComponent<Text>();
        but3 = GameObject.Find("Button 2").GetComponent<Button>();
        tBut3 = GameObject.Find("TB3").GetComponent<Text>();
        but4 = GameObject.Find("Button 3").GetComponent<Button>();
        tBut4 = GameObject.Find("TB4").GetComponent<Text>();
        but1.onClick.AddListener(() => { Button1(); });
        if (maxBeaten >= 1)
        {
            Debug.Log("Unlock Level 2");
            but2.interactable = true;
            tBut2.text = "Sunrise in\n3 minute";
            but2.onClick.AddListener(() => { Button2(); });
        }
        if (maxBeaten >= 2)
        {
            Debug.Log("Unlock Level 3");
            but3.interactable = true;
            tBut3.text = "Sunrise in\n5 minute";
            but3.onClick.AddListener(() => { Button3(); });
        }
        if (maxBeaten >= 3)
        {
            Debug.Log("Unlock Level Endless");
            but4.interactable = true;
            tBut4.text = "Sunrise in\n??? minute";
            but4.onClick.AddListener(() => { Button4(); });
        }
        
    }

    void SpawnVariableControl()
    {
        if(gameType == ONEMINGAME)
            {
                spawnRate = 2;
                numToSpawn = 3;
            }
            else
            {
                if(totalMinutes < 1)
                {
                    spawnRate = 3;
                    numToSpawn = 3;
                }
                else if(totalMinutes < 2)
                {
                    spawnRate = 3;
                    numToSpawn = 5;
                }
                else if(totalMinutes < 3)
                {
                    spawnRate = 10;
                    numToSpawn = 10;
                }
                else if(totalMinutes < 4)
                {
                    spawnRate = 20;
                    numToSpawn = 15;
                }
                else if(totalMinutes < 5)
                {
                    spawnRate = 20;
                    numToSpawn = 20;
                }
                else if(totalMinutes >= 5)
                {
                    spawnRate = 20;
                    numToSpawn = 25;
                }
            }
    }

    void SetGame()
    {
      if(gameType == DEBUG)
      {

      }
      else if(gameType == ONEMINGAME)
      {
          maxTime = 1;
      }
      else if(gameType == THREEMINGAME)
      {
          maxTime = 3;
      }
      else if(gameType == FIVEMINGAME)
      {
          maxTime = 5;
      }
      else if(gameType == ENDLESS)
      {
          maxTime = 2173;
      }
      else
      {
          maxTime = 1;
      }
      Application.LoadLevel(1);
    }

    //For When the player wins
    void EndGame() {
        string toDisplay;
        mHero.Health += 1000;
        StopAllCoroutines();
        if(mHero == null)
        {
            if(gameType != ENDLESS)
            {
                toDisplay = "Look's like you can't run those errands...\nPress the 'H' key to continue";
            }
            else
            {
                toDisplay = "You've survived " + totalMinutes.ToString() + " minutes & " + playTimer.ToString("00") + " seconds\nPress the 'H' key to try again";
            }
            
        }
        else
        {
            maxBeaten = gameType + 1;
            toDisplay = "You've survived long enough to do those errands...\nPress the 'H' key to survive longer.";

        }
        GameOver = true;
        DisplayStats(toDisplay);
    }

    void DisplayStats(string text)
    {
        mPanel.gameObject.SetActive(true);
        Info.gameObject.SetActive(true);
        Info.text = text;
    }

    #region Buttons and UI
    public void Button1()
    {
        gameType = ONEMINGAME;
        SetGame();
    }
    public void Button2()
    {
        gameType = THREEMINGAME;
        SetGame();
    }
    public void Button3()
    {
        gameType = FIVEMINGAME;
        SetGame();
    }
    public void Button4()
    {
        gameType = ENDLESS;
        SetGame();
    }
    #endregion

    #region Spawners
    IEnumerator MasterSpawner(GameObject actor)
    {
        while(true){
            int spawner = Random.Range(0, 4);
            if(spawner == 0) { Spawn0(actor, numToSpawn); }
            else if(spawner == 1) { Spawn1(actor, numToSpawn); }
            else if(spawner == 2) { Spawn2(actor, numToSpawn); }
            else if(spawner == 3) { Spawn3(actor, numToSpawn); }
            Debug.Log("Spawned " + numToSpawn.ToString() + " Trolls at " + spawner.ToString() + "! SPAWN RATE: " + spawnRate.ToString());
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void Spawn0(GameObject actor, int numOfActor)
    {
        for(int i = 0; i < numOfActor; i++)
        {
            Instantiate(actor, new Vector3(Random.Range(-6, 7), 11, 0), Quaternion.identity);
        }
    }
    void Spawn1(GameObject actor, int numOfActor)
    {
        for (int i = 0; i < numOfActor; i++)
        {
            Instantiate(actor, new Vector3(18, Random.Range(-3, 3), 0), Quaternion.identity);
        }
    }
    void Spawn2(GameObject actor, int numOfActor)
    {
        for (int i = 0; i < numOfActor; i++)
        {
            Instantiate(actor, new Vector3(Random.Range(-6, 7), -12, 0), Quaternion.identity);
        }
    }
    void Spawn3(GameObject actor, int numOfActor)
    {
        for (int i = 0; i < numOfActor; i++)
        {
            Instantiate(actor, new Vector3(-18, Random.Range(-3, 3), 0), Quaternion.identity);
        }
    }
    #endregion

}
