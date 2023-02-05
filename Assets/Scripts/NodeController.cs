using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// script for Node Game Object

// NOTE: IF THE NODE IS A BEGINNING NODE THERE IS NO PREVIOUS NODE
// ALSO MAKE SURE THERE IS A COLLIDER ON NODES SO THEY CAN CLICK PROPERLY

public class NodeController : MonoBehaviour
{

    // keeps track of what gameObjects are in our path
    // and so we can edit the root node's properties and
    // show certain status of nodes and more

    public NodeController nextNode1;
    public NodeController nextNode2;
    public NodeController nextNode3;

    public NodeController currentNode;
    public NodeController previousNode;
    public RootNode myRoot;

    public ButtonTypes spriteChanger;

    private bool hasSetStats = false;

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

    public int distance; // distance needed to claim the next node, if 0 we can claim
    public int maxDistance; // if we want to claim this node, this is the max
    // distance we have to traverse to get this node.

    public int resetDistance; // variable to reset once the current nodes is declaimed 
    
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

        if (distance >= maxDistance && !isClaimed)
        { 
            isClaimed = true; // change flag

            currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.claimedButton;// claimed node turns green

            if (nextNode1 != null)
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.claimableButton; // if we have a nextNode turn it yellow
            }

            if (nextNode2 != null)
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.claimableButton; // if we have a nextNode turn it yellow
            }

            if (nextNode3 != null)
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.claimableButton; ; // if we have a nextNode turn it yellow
            }

            myRoot.changeNumClaimed(1); // add 1 to number of nodes claimed

            // add benefits to having node 
            myRoot.changeActiveGrowth(addedActive);
            myRoot.changePassiveGrowth(addedPassive);


        }

        if (!isClaimed)
        {
            distance += myRoot.getActiveGrowth();
        }
    }

    void colorChange()
    {
        // unreachable: if previous node not claimed
        if (!isBeginningNode && !previousNode.isNodeClaimed() && !isClaimed) 
        {
            // normal node
            if (!isFinalNode)
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.UnclaimableButton;
            }
            // final node  
            else 
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.BossButton;
            }
        }

        // claimed
        else if (isNodeClaimed()) // claimed nodes, but not beginning node (green)
        {
            // normal node
            if (!isBeginningNode)
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.claimedButton;
            }
            // beginner node
            else
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.BeginningNode;
            }
        }

        // clickable: if previous node claimed and not isclaimed
        else
        {
            // normal node
            if (!isFinalNode)
            {
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.claimableButton;
            }
            // final node
            else
            {
                // Play SFX Danger
                currentNode.GetComponent<SpriteRenderer>().sprite = spriteChanger.BossButtonClickable;
            }
        }
           
    }

    // Start is called before the first frame update
    void Start()
    {
        // color stuff (initial)
        colorChange();
    }


    // Update is called once per frame
    void Update()
    {
        if (hasSetStats == false)
        {
            distance = maxDistance / 2;
            resetDistance = maxDistance / 2;

            pushBack = maxDistance / 20;
            addedActive = maxDistance / 20;
            addedPassive = maxDistance / 50;
            hasSetStats = true;
        }

        // lose condition goes here... if we lose initial beginner nodes, the planet can attack the main root and we die

        if (isBeginningNode && !isNodeClaimed()) 
        {
          SceneManager.LoadScene("GameOver");
        }

        if (!isBeginningNode && !previousNode.isNodeClaimed())
        {
            isClaimed = false;
            return;
        }

        if (isFinalNode && isNodeClaimed()) // meaning we're done with this path
        {
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
            if (!isNodeClaimed() && previousNode != null && (previousNode.isNodeClaimed()))
            {
                distance += myRoot.getPassiveGrowth();
                distance -= pushBack;
            }

            //  unclaim condition
            if (distance <= 0)
            {
                if (isBeginningNode)
                {
                    colorChange();
                    return;
                }

                previousNode.isClaimed = false;

               

                colorChange();

                myRoot.numNodesClaimed--; // subtract a node claimed

                previousNode.distance = previousNode.resetDistance;
                currentNode.distance = currentNode.resetDistance;

                

                // removes benefits
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
