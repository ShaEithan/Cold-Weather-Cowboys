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
    public NodeController previousnode;
    public RootNode myRoot;

    // flags 
    public bool isFinalNode; // is this the final node in the sequence/path
    public bool isClaimed; // have we claimed this?
    public bool isBeginningNode; // is this one of the original 3 nodes

    // amount & type needed to claim

    public int TypeNumberNeeded;
    public int amountNeeded;

    // once claimed, tells us what the root node benefits from

    public int TypeNumberGiven; // what type are we increasing production of 
    public int amountGiven; // amount added per frame etc
    

    // add extra benefits here maybe

    // getter functions

    public bool isNodeClaimed()
    {
        return isClaimed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        // ensures that the previous node must be claimed before we can even consider claiming
        if (Input.GetMouseButtonDown(0) && (isBeginningNode || previousnode.isNodeClaimed()))
        {
            // if it isn't claimed and we have enough resources, we claim
            if (!isClaimed && myRoot.getTypeAmount(TypeNumberNeeded) >= amountNeeded)
            {
                isClaimed = true; // we've now claimed this node

                currentNode.GetComponent<Renderer>().material.color = new(0, 255, 0); // change color

                // subtract resources needed for claiming
                myRoot.subtractResources(TypeNumberNeeded, amountNeeded); 

                // get increased production benefits for claiming this node 
                myRoot.increaseAddAmount(TypeNumberGiven, amountGiven);

                // increment number of nodes claimed.
                myRoot.numNodesClaimed++;
            }
        }

        // need condition on what to do when planet starts to push back
        
    }
}
