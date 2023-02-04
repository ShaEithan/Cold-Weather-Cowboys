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

    public int pushBack; // updates on time interval to add distance to, interacts with
    // curDistanceToClaim

    // benefits for getting node
    public int addedPassive;
    public int addedActive;

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
        if (!previousNode.isNodeClaimed() || isBeginningNode)
        {
            return;
        }

        if (curDistanceToClaim <= 0 && !isClaimed)
        { 
            isClaimed = true; // change flag

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
        
    }

    // Update is called once per frame
    void Update()
    {
        // if this is a beginning node and unclaimed -> lose the game! 
        
        // if the previous node isn't claimed, then make sure this one is unclaimed

        // add passive growth as long as it isn't claimed

        // subtract current growth distance with push back value make sure this is claimed

        // check if curDistanceToClaim >= maxDistancetoClaim -> unclaim previous node
        // and remove benefits

        
    }
}
