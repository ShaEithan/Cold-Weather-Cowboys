using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script for Node Game Object
// NOTE: IF THE NODE IS A BEGINNING NODE THERE IS NO PREVIOUS NODE
// ALSO MAKE SURE THERE IS A COLLIDER ON NODES SO THEY CAN CLICK PROPERLY

public class NodeController : MonoBehaviour
{
    // keeps track of what gameObjects are in our path so we can edit the root node's properties and show certain status of nodes and more

    // old
    public NodeController nextNode;
    public NodeController previousNode;

    public NodeController currentNode;
    public RootNode myRoot;

    //public GameObject[] neighborNode;
    public NodeController previousNode;
    public NodeController neighborNode1;
    public NodeController neighborNode2;
    //public List<GameObject> neighborNode;


    // connector
    /*
    connect from previous to current (Game Object), connect to current to next (Game Object)
    */

    // flags 
    public bool isClaimed = false; // have we claimed this?
    public bool isActive = false;
    public bool isBeginningNode = false; // is this one of the original nodes
    public bool isEndNode = false; // is this the end node in the sequence/path

    // push and pull attributes
    public int curDistanceToClaim = 30; // distance needed to claim the next node, if 0 we can claim
    
    public int maxDistanceToClaim = 60; // if we want to claim this node, this is the max distance we have to traverse to get this node.
    public int resetCurDistanceToClaim = 30; // variable to reset once the current nodes is declaimed 
    
    public int pushBack; // updates on time interval to add distance to, interacts with curDistanceToClaim

    // benefits for getting node
    public int addedPassive;
    public int addedActive;

    // timer variables
    protected float Timer;
    public int delayAmount = 5; // 1 second 

    // scratch work
    /*
        if one is claimed before another 
        if one is lost before another

    O
    if win: make self claimed/unclickable

    O
    if lose: become unclickable and set 

    O
    if lose: become clickable

    */

    // Mouse Button Down Function, activates everytime mouse cursor is pressed on collider of node
    void OnMouseDown()
    {
        // SFX Click
        // can't touch if not active: a neighbor node isn't claimed and it's not a beginning node

        if (!neighborNode1 == null)
        {

        }

        if (!isBeginningNode && !neighborNode1.isNodeClaimed())
        {
            return;
        }


        if (!isClaimed) // Active Growth
        {
            curDistanceToClaim -= myRoot.getActiveGrowth();
        }

        // claim
        if (curDistanceToClaim <= 0 && !isClaimed)
        { 
            isClaimed = true; // change flag
            // SFX Victory/Uprooted
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0); // claimed node turns green
            myRoot.changeNumClaimed(1); // add 1 to number of nodes claimed
            // add benefits to having node 
            myRoot.changeActiveGrowth(addedActive);
            myRoot.changePassiveGrowth(addedPassive);

            // activate unclaimed neighbors
            if (!neighborNode1.isActive && !neighborNode1.isClaimed)
            {
                neighborNode1.isActive = true;
            }
        }
    }

    public bool isNodeClaimed()
    {
        return isClaimed;
    }

    void resetDistance()
    {
        curDistanceToClaim = resetCurDistanceToClaim;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        //For creating line renderer object
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        //lineRenderer.startColor = Color.black;
        //lineRenderer.endColor = Color.black;
        //lineRenderer.startWidth = 0.01f;
        //lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;    
                        
        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, new Vector3(x,y,z)); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, new Vector3(x,y,z)); //x,y and z position of the end point of the line
        */

        // color stuff (initial)
        if (isBeginningNode) // beginner node
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 255);
        }
        else if (isEndNode) // end node (red)
        {
            currentNode.GetComponent<Renderer>().material.color = new(255, 0, 0);
        }
        else if (isNodeClaimed() && (isBeginningNode || neighborNode1.isNodeClaimed())) // claimed nodes, but not beginning node (green)
        {
            currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0);
        }
        else if (!isNodeClaimed() && neighborNode1.isNodeClaimed()) // clickable and not end boss node (yellow)
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
        // if neighboring node contested, do nothing
        // if neighboring node captured, begin

        // endpath claimed
        if (isEndNode && isNodeClaimed()) 
        {
            return;
        }

        // game over if we lose beginning node(s)
        if (isBeginningNode && isClaimed == false) 
        {
            SceneManager.LoadScene("GameOver");
        } 

        if (!neighborNode1.isNodeClaimed())
        {
            //isClaimed = false;
            isActive = false;
            return;
        }

        // Non-click battle elements
        Timer += Time.deltaTime;
        if (Timer >= delayAmount)
        {
            Timer = 0f;

            // if this node isn't claimed, and the previous one is, there will be pushback and passive growth
            if (!isClaimed && (!isActive))
            {
                curDistanceToClaim -= myRoot.getPassiveGrowth();
                curDistanceToClaim += pushBack;
            }

            // lose 
            if (curDistanceToClaim >= maxDistanceToClaim)
            {
                // SFX Lose
                neighborNode.isClaimed = false;
                myRoot.numNodesClaimed--; // subtract a node claimed

                neighborNode.resetDistance();
                currentNode.resetDistance();

                //s sprite change
                if (currentNode.isEndNode)
                {
                    // change back to end Node color bc this should be inaccessble
                    currentNode.GetComponent<Renderer>().material.color = new(255, 0, 0);
                }
                else
                {
                    currentNode.GetComponent<Renderer>().material.color = new(0, 0, 0); // should be inaccessible node color 
                }
                
                // need to make neighbor unclaimed
                //previousNode.GetComponent<Renderer>().material.color = new(255, 255, 0); // previous node should now be clickable
                
                // -- ! --
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
