using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for Node Game Object

// remember, to specify what type we need. Either 1 or 2 so far.

public class NodeController : MonoBehaviour
{

    // keeps track of what gameObjects are in our path
    // and so we can edit the root node's properties and
    // show certain status of nodes and more

    public NodeController nextNode;
    public NodeController currentNode;
    public NodeController previousNode;
    public RootNode myRoot;

    // flags 
    public bool isFinalNode; // is this the final node in the sequence/path
    public bool isClaimed; // have we claimed this?
    public bool isBeginningNode; // is this one of the original 3 nodes

    // push and pull attributes

    public int curDistanceToClaim; // distance needed to claim the next node, if 0 we can claim
    public int maxDistanceToClaim; // if we want to claim this node, this is the max
    // distance we have to traverse to get this node.

    public int resetCurDistanceToClaim; 
    
    public int pushBack; // updates on time interval to add distance to, interacts with
    // curDistanceToClaim

    // benefits for getting node
    public int addedPassive;
    public int addedActive;


    // timer variables

    protected float Timer;
    public int delayAmount = 1; // 1 second 

    // getters 

    public bool isNodeClaimed()
    {
        return isClaimed;
    }

    // Mouse Button Down Function
    // activates everytime mouse cursor is pressed on collider of node

    void OnMouseDown()
    {
        // if previous node isn't claimed, we can't touch this one yet
        if (isBeginningNode || !previousNode.isNodeClaimed())
        {
            return;
        }

        if (curDistanceToClaim <= 0 && !isClaimed)
        { 
            isClaimed = true; // change flag
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0);

            if (nextNode != null)
            {
                nextNode.GetComponent<Renderer>().material.color = new(0, 0, 255);
            }

            myRoot.changeNumClaimed(1); // add 1 to number of nodes claimed

            // add benefits to having node 
            myRoot.changeActiveGrowth(addedActive);
            myRoot.changePassiveGrowth(addedPassive);


        }
        else if (!isClaimed)
        {
            curDistanceToClaim -= myRoot.getActiveGrowth();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // color stuff
        if (isNodeClaimed() && (isBeginningNode || previousNode.isNodeClaimed())) // claimed
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0);
        }
        else if (!isNodeClaimed() && previousNode.isNodeClaimed()) // clickable
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 0, 255);
        }
        else
        {
            currentNode.GetComponent<Renderer>().material.color = new(255, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinalNode && isNodeClaimed()) // meaning we're done with this path
        {
            return;
        }


        if (!previousNode.isNodeClaimed())
        {
            isClaimed = false;
            return;
        }

        // if the planet has pushed back all the way, we unclaim the previous node and have to reclaim

        if (curDistanceToClaim >= maxDistanceToClaim && !isFinalNode)
        {
            previousNode.isClaimed = false;

            myRoot.numNodesClaimed--; // subtract a node claimed

            previousNode.curDistanceToClaim = previousNode.resetCurDistanceToClaim;

            currentNode.GetComponent<Renderer>().material.color = new(255, 0, 0); // can't click currentNode anymore
            previousNode.GetComponent<Renderer>().material.color = new(0, 0, 255); // previous node should now be clickable

            if (addedActive > 0)
            {
                myRoot.subtractActiveGrowth(previousNode.addedActive); 
            }

            if (addedPassive > 0)
            {
                myRoot.subtractPassiveGrowth(previousNode.addedPassive);
            }

        }

        Timer += Time.deltaTime;

        if (Timer >= delayAmount)
        {
            Timer = 0f;

            // if this node isn't claimed, and the previous one is
            // there will be pushback and passive growth
            if (!isNodeClaimed() && (previousNode.isNodeClaimed()))
            {
                curDistanceToClaim -= myRoot.getPassiveGrowth();
                curDistanceToClaim += pushBack;
            }
        }

        

        // if this is a beginning node and unclaimed -> lose the game! 
        
        // if the previous node isn't claimed, then make sure this one is unclaimed

        // add passive growth as long as it isn't claimed

        // subtract current growth distance with push back value make sure this is claimed

        // check if curDistanceToClaim >= maxDistancetoClaim -> unclaim previous node
        // and remove benefits

        
    }
}
