using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for Types
public class Type
{
   
}

public class RootNode : MonoBehaviour
{
    // List of Nodes in our List;
    List<GameObject> nodeList;

    // How many Nodes are currently claimed
    public int numNodesClaimed;

    // Types 1 and 2 initialized, can change starting count 
    public int type1Count;
    public int type2Count;

    // base amount incremented / generated for 
    private int addType1 = 1;
    private int addType2 = 0;


    private bool addMore = true;

    // Win Condition for Game

    public void winCondition()
    {
        int numNodes = nodeList.Count; // size of our list
        if (numNodes == numNodesClaimed)
        {
            print("You Win!");
        }
    }

    // getter functions

    // getting current amount of a type
    public int getTypeAmount(int typeNumber)
    {
        if (typeNumber == 1)
        {
            return type1Count;
        }
        else if (typeNumber == 2) {
            return type2Count;
        }

        return 0;
    }

    // getting current amount added of a type
    public int getAddAmount(int typeNumber)
    {
        if (typeNumber == 1)
        {
            return addType1;
        }
        else if (typeNumber == 2)
        {
            return addType2;
        }

        return 0;
    }

    // adding or subtracting amount when claiming / unclaiming a node
    public void increaseAddAmount(int typeNumber, int amountToAdd)
    {
        if (typeNumber == 1)
        {
            addType1 += amountToAdd;
        }
        else if (typeNumber == 2)
        {
            addType2 += amountToAdd;
        }

    }

    // subtract resources needed for claiming
    // make sure only to call WHEN WE HAVE ENOUGH
    public void subtractResources(int typeNumber, int amountToSub)
    {
        if (typeNumber == 1 && getTypeAmount(typeNumber) >= amountToSub)
        {
            type1Count -= amountToSub;

        } else if (typeNumber == 2 && getTypeAmount(typeNumber) >= amountToSub)
        {
            type2Count -= amountToSub;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // add more resources by your amountToAdd by pressing Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            type1Count += addType1;
            type2Count += addType2;
        }
    }
}
