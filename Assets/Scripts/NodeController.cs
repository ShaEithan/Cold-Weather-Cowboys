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
    // so we can edit the root node's properties and
    // show certain status of nodes and more

    // old
    public NodeController nextNode;
    public NodeController previousNode;

    public NodeController currentNode;
    public RootNode myRoot;

    //public GameObject[] neighborNode;

    public NodeController[] neighborNode;

    // connector
    /*
    connect from previous to current (Game Object), connect to current to next (Game Object)
    */

    // flags 
    public bool isFinalNode; // is this the final node in the sequence/path
    public bool isClaimed; // have we claimed this?
    public bool isBeginningNode; // is this one of the original nodes

    // push and pull attributes

    public int curDistanceToClaim; // distance needed to claim the next node, if 0 we can claim
    public int maxDistanceToClaim; // if we want to claim this node, this is the max distance we have to traverse to get this node.

    public int resetCurDistanceToClaim; // variable to reset once the current nodes is declaimed 
    
    public int pushBack; // updates on time interval to add distance to, interacts with curDistanceToClaim

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

    // if neighboring node contested, do nothing
    // if neighboring node captured, begin

    // Mouse Button Down Function, activates everytime mouse cursor is pressed on collider of node
    void OnMouseDown()
    {
        // if previous node isn't claimed, we can't touch this one yet
        // if it's a beginning node, we shouldn't be able
        if (isBeginningNode || !neighborNode.isNodeClaimed())
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
        // endpath claimed
        if (isFinalNode && isNodeClaimed()) 
        {
            return;
        }

        // game over if we lose beginning node(s)
        if (isBeginningNode && !isNodeClaimed()) 
        {
            SceneManager.LoadScene("GameOver");
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
                //previousNode.isClaimed = false;
                neighborNode.isClaimed = false;

                myRoot.numNodesClaimed--; // subtract a node claimed

                //previousNode.curDistanceToClaim = previousNode.resetCurDistanceToClaim;
                neighborNode.curDistanceToClaim = neighborNode.resetCurDistanceToClaim;
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
                
                //previousNode.GetComponent<Renderer>().material.color = new(255, 255, 0); // previous node should now be clickable

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
