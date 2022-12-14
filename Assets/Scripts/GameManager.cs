
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Spawns players, handles Game State, disables and enables objects

public sealed class GameManager : MonoBehaviour
{
    public Color player1Color = Color.red;
    public Color player2Color = Color.blue;

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public bool spawnPlayer1;
    public bool spawnPlayer2;

    public bool isPlayersInvincible;
    private bool tutorialIsPlaying;

    public int waitTimeBeforeStarting;
    public float waitTimeAfterEnd;

    private static GameManager instance; //Singleton

    public float timeToRespawn;
    public bool playUnlimited;
    public float timeToNewGame;
    //UI
    public CountDownUI startingUI;
    public GameOver_UI gameOver_UI;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Gamemanger is null");
            }
            return instance;
        }
    }


    public Transform spawn1;
    public Transform spawn2;


    public List<ShipManager> players = new List<ShipManager>();


    public int m_NumRoundsToWin;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public Text m_MessageText;
    public GameObject m_TankPrefab;

    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private ShipManager m_RoundWinner;
    private ShipManager m_GameWinner;


    public List<GameObject> whirlpools = new List<GameObject>();
    public List<GameObject> volcanos = new List<GameObject>();
    public List<MerchantShip> merchantShips = new List<MerchantShip>();
    public List<IslandHealth> islands = new List<IslandHealth>();




    // Start is called before the first frame update
    private void Awake()
    {

        //Stop all audio
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.Stop();
        }

        instance = this;

        if (spawnPlayer1)
        {
            var p1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
            players.Add(p1.gameObject.GetComponent<ShipManager>());

            p1.GetComponent<ShipManager>().m_SpawnPoint = spawn1;

            p1.transform.position = spawn1.transform.position;
            p1.transform.rotation = spawn1.transform.rotation;
            TeleportManager.Instance.AddTeleportable(p1.gameObject);
        }

        if (spawnPlayer2)
        {
            var p2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
            //Set the two players
            players.Add(p2.gameObject.GetComponent<ShipManager>());

            p2.GetComponent<ShipManager>().m_SpawnPoint = spawn2;


            //Set position to spawnpoints
            p2.transform.position = spawn2.transform.position;
            p2.transform.rotation = spawn2.transform.rotation;

            //Make them teleportable
            TeleportManager.Instance.AddTeleportable(p2.gameObject);
        }

        //Sets the countdown before beginning

        if (startingUI != null)
            startingUI.countDownLength = waitTimeBeforeStarting;

       






    }
    private void NormalTime()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 0.5f);
    }

    public void SlowTime()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0.2f,0.5f);
        Invoke("NormalTime", 0.05f);
    }

    private void OnDisable()
    {
        this.enabled = true;
    }


    private void Start()
    {
        StartCoroutine(GameLoop());

        m_GameWinner = null;
    }

    private void Update()
    {
        if (isPlayersInvincible)
        {
            foreach (var player in players)
            {
                player.GetComponent<PlayerHealth>().CanTakeDamage = false;
            }
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStarting());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameEnding());




        if (playUnlimited)
        {
            gameOver_UI.gameObject.SetActive(false);
            m_GameWinner = null;
            ResetAllShips();
            StartCoroutine(GameLoop());
        }
        else
        {
            SceneManager.LoadScene("MainMenu_Start");

            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator GameStarting()
    {
        DisableShipControl();
        DisableWhirlpools();
        DisableMerchanshipMovement();
        yield return new WaitForSeconds(waitTimeBeforeStarting + 1);
    }


    private IEnumerator GamePlaying()
    {
        EnableShipControl();
        EnableWhirlpools();
        EnableVolcanos();
        EnableIslands();
        EnableMerchanshipMovement();

        Scene scene = SceneManager.GetActiveScene();
        if (scene == SceneManager.GetSceneByName("Tutorial"))
        {
            tutorialIsPlaying = true;
        }
        else
        {
            tutorialIsPlaying = false;
        }

        if (!tutorialIsPlaying)
        SoundManager.Instance.PlayMusic("BattleTheme");

        //If there is no game winner, revive dead player and add win for the player alive.
        while (m_GameWinner == null)
        {   
            //Run when there are more than 1 ship left
            while (!OneShipLeft())
            {   
                yield return null;
            }

            //TODO Dosen't work when both players die at the same time
            m_RoundWinner = GetRoundWinner();
            m_RoundWinner.roundWins++;
            m_GameWinner = GetGameWinner();

            //Revive dead player;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].isDead)
                    StartCoroutine(players[i].Revive());
                while(players[i].isDead) {
                    yield return null;
                }
            }
        }
        yield return null;
    }


    private IEnumerator GameEnding()
    {
        gameOver_UI.winningPlayer = players.IndexOf(m_GameWinner);
        SoundManager.Instance.PlayEffects("Victory");
        gameOver_UI.gameObject.SetActive(true);

        DisableShipControl();
        DisableWhirlpools();
        DisableVolcanos();
        DisableIslands();
        DisableMerchanshipMovement();

        yield return new WaitForSeconds(timeToNewGame);
        yield return null;
    }


    private bool OneShipLeft()
    {
        int numShipsLeft = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].isDead)
                numShipsLeft++;
        }

        return numShipsLeft <= 1;
    }


    private ShipManager GetRoundWinner()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].isDead)
                return players[i];
        }
        return null;
    }


    private ShipManager GetGameWinner()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].roundWins == m_NumRoundsToWin)
                return players[i];
        }
        return null;
    }

    private void ResetAllShips()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Revive();
        }
    }

    private void EnableShipControl()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EnableScripts();
        }
    }
    private void DisableShipControl()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].DisableScripts();
        }
    }

    private void EnableVolcanos()
    {
        for (int i = 0; i < volcanos.Count; i++)
        {
            volcanos[i].GetComponent<Volcano>().enabled = true;
        }
    }
    private void DisableVolcanos()
    {
        for (int i = 0; i < volcanos.Count; i++)
        {
            volcanos[i].GetComponent<Volcano>().enabled = false;
        }
    }

    private void EnableIslands()
    {
        for (int i = 0; i < islands.Count; i++)
        {
            islands[i].GetComponent<IslandHealth>().enabled = true;
        }
    }
    private void DisableIslands()
    {
        for (int i = 0; i < islands.Count; i++)
        {
            islands[i].GetComponent<IslandHealth>().enabled = false;
        }
    }

    private void EnableWhirlpools()
    {
        for (int i = 0; i < whirlpools.Count; i++)
        {
            whirlpools[i].GetComponent<WhirlpoolMovement>().enabled = true;
        }
    }
    private void DisableWhirlpools()
    {
        for (int i = 0; i < whirlpools.Count; i++)
        {
            whirlpools[i].GetComponent<WhirlpoolMovement>().enabled = false;
        }
    }

    private void EnableMerchanshipMovement()
    {
        for (int i = 0; i < merchantShips.Count; i++)
        {
            merchantShips[i].enabled = true;
        }
    }
    private void DisableMerchanshipMovement()
    {
        for (int i = 0; i < merchantShips.Count; i++)
        {
            merchantShips[i].enabled = false;
        }
    }
}
