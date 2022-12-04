using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolMovement : MonoBehaviour
{
    public bool huntPlayers;
    public float changeDirectionTimer;
    public float stepSpeed;

    float targetX;
    float targetY;
    float timerHolder;
    float mapL;
    float mapH;

    int randomPlayerToHunt;

    ShipManager leadPlayer;

    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        mapL = TeleportManager.instance.mapL;
        mapH = TeleportManager.instance.mapH;
    }

    ShipManager GetLeadPlayer()
    {
        ShipManager player1 = GameManager.Instance.players[0];
        ShipManager player2 = GameManager.Instance.players[1];
        if (player1.roundWins > player2.roundWins)
            return player1;
        else if (player1.roundWins < player2.roundWins)
            return player2;
        else
            return null;
    } 


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time >= timerHolder)
        {
            timerHolder = Time.time + changeDirectionTimer;

            targetX = Random.Range(0, mapL); 
            targetY = Random.Range(0, mapH);

            if(huntPlayers)
            {
                leadPlayer = GetLeadPlayer();
                if (leadPlayer != null)
                    target = leadPlayer.transform.position;

                else
                {
                    randomPlayerToHunt = Random.Range(0, 2);
                    target = GameManager.Instance.players[randomPlayerToHunt].transform.position;
                }
            }

            else
            {
                target = new Vector3(targetX-mapL/2, targetY-mapH/2, 0);
            }

        }

        transform.position = Vector3.MoveTowards(transform.position, target, stepSpeed);
    }
}
