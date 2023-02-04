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

    // push and pull attributes
    public int curDistanceToClaim; // distance needed to claim the next node, if 0 we can claim
    public int maxDistanceToClaim; // if curDistanceToClaim == maxDistanceToClaim, we have to unclaim...
    public int pushBack; // updates on time interval to add distance to 


    // benefits for getting node
    public int addedPassive;
    public int addedActive;

    // Mouse Button Down Function
    // activates everytime mouse cursor is pressed on collider of node

    void OnMouseDown()
    {
        curDistanceToClaim -= myRoot.getActiveGrowth();

        if (curDistanceToClaim <= 0)
        {
            isClaimed = true;
        }
    }

    // getters 

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

        // need condition on what to do when planet starts to push back & for passive growth
        
    }
}
