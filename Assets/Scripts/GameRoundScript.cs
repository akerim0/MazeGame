using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundScript : MonoBehaviour
{
    public int round = 1;
    private float timer;
    private static float avgTime = 10.0f;
    private int noOfZombies, noOfMutant;
    public List<GameRound> roundList;
    // Start is called before the first frame update
    void Start()
    {
        roundList = new List<GameRound>();
        roundList.Add(new GameRound(1, 10, 4, 0));
        roundList.Add(new GameRound(2, 10, 10, 0));
        roundList.Add(new GameRound(3, 30, 20, 0));
        roundList.Add(new GameRound(4, 60, 20, 0));
        roundList.Add(new GameRound(5, 120, 20, 0));
        roundList.Add(new GameRound(6, 180, 25, 0));
        roundList.Add(new GameRound(7, 240, 25, 1));
        roundList.Add(new GameRound(8, 600, 40, 1));
        roundList.Add(new GameRound(9, 1200, 80, 2));
        roundList.Add(new GameRound(10,1200, 80, 2));
        

    }

    // Update is called once per frame
    void Update()
    {   
        
        if(timer > 0)
        {
            timer -= Time.deltaTime; 
        }
        else
        {
            round++;
            
        }
        
    }

   public void StartCountDown(float time)
   {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            round ++;
        }
        
   }
   public int getRound()
    {
        return round;
    }
}

public class GameRound
{
     public int numOfMutant, numOfZombies;
     int round;
     public float duration;

    public GameRound(int round, float duration,int numOfZombies, int numOfMutant)
    {
        this.round = round;
        this.duration = duration;
        this.numOfMutant = numOfMutant;
        this.numOfZombies = numOfZombies;
    }
}