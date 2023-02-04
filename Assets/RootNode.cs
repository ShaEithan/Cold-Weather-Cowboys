using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RootNode : MonoBehaviour
{
    // List of Nodes in our List;
    List<GameObject> nodeList;

    // How many Nodes are currently claimed
    // tentative to change because 
    public int numNodesClaimed;

    // growth variables
    private int passiveGrowth = 0; // how much growth rate without pressing (defense)
    private int activeGrowth = 1; // how much growth rate when clicking (attack)


    // getters

    // useful when needing the current growth rate, but also for UI

    public int getPassiveGrowth()
    {
        return passiveGrowth;
    }

    public int getActiveGrowth()
    {
        return activeGrowth;
    }

    // setters

    // Used for rewards for claiming more nodes 
    public void changePassiveGrowth(int changeAmount)
    {
        if (passiveGrowth + changeAmount < 0)
        {
            passiveGrowth = 0;
            
        }
        else
        {
            passiveGrowth += changeAmount;
        }
    }

    public void changeActiveGrowth(int changeAmount)
    {
        if (activeGrowth + changeAmount < 1)
        {
            activeGrowth = 1;
        }
        else
        {
            activeGrowth += changeAmount;
        }
    }

    // Win Condition for Game

    public void winCondition()
    {
        int numNodes = nodeList.Count; // size of our list
        if (numNodes == numNodesClaimed)
        {
            print("You Win!");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }
}
