
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public Color player1Color = Color.red;
    public Color player2Color = Color.blue;

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public bool spawnPlayer1;
    public bool spawnPlayer2;

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


    public List<WhirlpoolMovement> whirlpools = new List<WhirlpoolMovement>();
    public List<MerchantShip> merchantShips = new List<MerchantShip>();

    // Start is called before the first frame update
    private void Awake()
    {
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


        startingUI.countDownLength = waitTimeBeforeStarting;






    }

    private void Start()
    {
        StartCoroutine(GameLoop());

        m_GameWinner = null;
    }

    void NextRound()
    {
        /*
        foreach (var player in players)
        {
            if (player.GetComponent<Health>().currentHealth >= 0)
            {
                players[0].transform.position = spawn1.transform.position;
                players[0].transform.rotation = spawn1.transform.rotation;
                players[0].SetActive(true);

                players[1].transform.position = spawn2.transform.position;
                players[1].transform.rotation = spawn2.transform.rotation;
                players[1].SetActive(true);
                return;
            }



        }*/
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStarting());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameEnding());

        Debug.Log("RUNNING GAME LOOP");


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
        DisableWhirlpoolsMovement();
        DisableMerchanshipMovement();

        //m_RoundNumber++;
        //m_MessageText.text = "ROUND " + m_RoundNumber;
        yield return new WaitForSeconds(waitTimeBeforeStarting);
    }


    private IEnumerator GamePlaying()
    {
        EnableShipControl();
        EnableWhirlpoolsMovement();
        EnableMerchanshipMovement();
        //m_MessageText.text = string.Empty;

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
        
        Debug.Log("Gamewinner: " + m_GameWinner);


        gameOver_UI.winningPlayer = players.IndexOf(m_GameWinner);
        gameOver_UI.gameObject.SetActive(true);

        DisableShipControl();
        DisableWhirlpoolsMovement();
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


    private void EnableWhirlpoolsMovement()
    {
        for (int i = 0; i < whirlpools.Count; i++)
        {
            whirlpools[i].enabled = true;
        }
    }

    private void DisableWhirlpoolsMovement()
    {
        for (int i = 0; i < whirlpools.Count; i++)
        {
            whirlpools[i].enabled = false;
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
