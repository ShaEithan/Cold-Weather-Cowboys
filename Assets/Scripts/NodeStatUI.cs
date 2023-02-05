using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeStatUI : MonoBehaviour
{

    // Text Property
    public TMP_Text distanceNeededText;
    public NodeController currentNode;

    // Start is called before the first frame update
    void Start()
    {
        // intital distance needed, shows only if node is available to claim
        //if (currentNode.previousNode.isNodeClaimed())
        //{
            //distanceNeededText.gameObject.SetActive(true);
            distanceNeededText.text = currentNode.distance.ToString() + " / " + currentNode.maxDistance;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        // updates distance text with current distance of node
        // only shows if node is available to claim

        if (currentNode.previousNode.isNodeClaimed() && !currentNode.isNodeClaimed())
        {
            distanceNeededText.gameObject.SetActive(true);
            distanceNeededText.text = currentNode.distance.ToString() + " / " + currentNode.maxDistance; ;
        }
        else
        {
            distanceNeededText.gameObject.SetActive(false);
        }
        
    }
}
