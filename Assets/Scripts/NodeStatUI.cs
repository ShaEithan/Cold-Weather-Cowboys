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
    public float distancePercent;
    private float rVal;
    private float gVal = .94f;
    private float bVal;

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

        if (currentNode.previousNode != null && currentNode.previousNode.isNodeClaimed() && !currentNode.isNodeClaimed())
        {
            distanceNeededText.gameObject.SetActive(true);

            //rgb1: 245, 240, 130 (.96, .94, .51)
            //rgb2: 145, 240, 205 (.57, .94, .80)
            distancePercent = currentNode.distance/currentNode.maxDistance;

            rVal = .96f - ((distancePercent/100.0f)*.39f);
            bVal = .51f + ((distancePercent/100.0f)*.29f);

            distanceNeededText.text = currentNode.distance.ToString() + " / " + currentNode.maxDistance;
            //distanceNeededText.color = new Color(rVal, gVal, bVal, 1);
            //Debug.Log(distancePercent);

        }
        else
        {
            distanceNeededText.gameObject.SetActive(false);
        }
        
    }
}

