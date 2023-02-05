using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for Node Game Object

// NOTE: IF THE NODE IS A BEGINNING NODE THERE IS NO PREVIOUS NODE
// ALSO MAKE SURE THERE IS A COLLIDER ON NODES SO THEY CAN CLICK PROPERLY

public class NodeController : MonoBehaviour
{

    // keeps track of what gameObjects are in our path
    // and so we can edit the root node's properties and
    // show certain status of nodes and more

    public NodeController nextNode;
    public NodeController currentNode;
    public NodeController previousNode;
    public RootNode myRoot;

    // connector
    /*
     connect from previous to current (Game Object)
     connect to current to next (Game Object)
    */

    // flags 
    public bool isFinalNode; // is this the final node in the sequence/path
    public bool isClaimed; // have we claimed this?
    public bool isBeginningNode; // is this one of the original 3 nodes

    // push and pull attributes

    public int curDistanceToClaim; // distance needed to claim the next node, if 0 we can claim
    public int maxDistanceToClaim; // if we want to claim this node, this is the max
    // distance we have to traverse to get this node.

    public int resetCurDistanceToClaim; // variable to reset once the current nodes is declaimed 
    
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
        // if it's a beginning node, we shouldn't be bale
        if (isBeginningNode || !previousNode.isNodeClaimed())
        {
            return;
        }

        if (curDistanceToClaim <= 0 && !isClaimed)
        { 
            isClaimed = true; // change flag

            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0); // claimed node turns green

            if (nextNode != null)
            {
                nextNode.GetComponent<Renderer>().material.color = new(255, 255, 0); // if we have a nextNode turn it yellow
            }

            myRoot.changeNumClaimed(1); // add 1 to number of nodes claimed

            // add benefits to having node 
            myRoot.changeActiveGrowth(addedActive);
            myRoot.changePassiveGrowth(addedPassive);


        }

        if (!isClaimed)
        {
            curDistanceToClaim -= myRoot.getActiveGrowth();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // color stuff (initial)
        if (isBeginningNode) // beginner node color (cyan)
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 255);
        }
        else if (isFinalNode) // final node color (red)
        {
            currentNode.GetComponent<Renderer>().material.color = new(255, 0, 0);
        }
        else if (isNodeClaimed() && (isBeginningNode || previousNode.isNodeClaimed())) // claimed nodes, but not beginning node (green)
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0);
        }
        else if (!isNodeClaimed() && previousNode.isNodeClaimed()) // clickable and not final boss node (yellow)
        {
            currentNode.GetComponent<Renderer>().material.color = new(255, 255, 0);
        }
        else // unreachable (black) 
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // lose condition goes here... if we lose initial beginner nodes, the planet can attack the main root and we die
        
        /*
           if (isBeginnerNode && !isNodeClaimed()) 
           {
                go to lose scene ? 
           }
        */ 
        
        if (isFinalNode && isNodeClaimed()) // meaning we're done with this path
        {
            return;
        }


        if (!previousNode.isNodeClaimed())
        {
            isClaimed = false;
            return;
        }

        // if the planet has pushed back all the way, we unclaim the previous node and have to reclaim the previous one to start getting this one
        // again

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

            if (curDistanceToClaim >= maxDistanceToClaim)
            {
                previousNode.isClaimed = false;

                myRoot.numNodesClaimed--; // subtract a node claimed

                previousNode.curDistanceToClaim = previousNode.resetCurDistanceToClaim;
                currentNode.curDistanceToClaim = currentNode.resetCurDistanceToClaim;

                if (currentNode.isFinalNode)
                {
                    // change back to final Node color bc this should be inaccessble
                    currentNode.GetComponent<Renderer>().material.color = new(255, 0, 0);
                }
                else
                {
                    currentNode.GetComponent<Renderer>().material.color = new(0, 0, 0); // should be inaccessible node color 
                }
                
                previousNode.GetComponent<Renderer>().material.color = new(255, 255, 0); // previous node should now be clickable

                if (addedActive > 0)
                {
                    myRoot.subtractActiveGrowth(previousNode.addedActive);
                }

                if (addedPassive > 0)
                {
                    myRoot.subtractPassiveGrowth(previousNode.addedPassive);
                }

            }
        }
    }
}
