using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    // gameObject Variables
    public GameObject currentObject;
    public GameObject previousObject;

    // needed if we need to access variables from our currentNode
    public NodeController currentNode;

    public LineRenderer lineRenderer; 


    private void drawLineBetweenNodes(Transform firstT, Transform secondT)
    {
        // set line renderer positions
        lineRenderer.SetPosition(0, firstT.position);
        lineRenderer.SetPosition(1, secondT.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // # of positions to count of line renderer
        lineRenderer.positionCount = 2;

        if (currentObject != null && previousObject != null)
        {
            Transform currentLocation = currentObject.transform;
            Transform previousLocation = previousObject.transform;

            drawLineBetweenNodes(currentLocation, previousLocation);
        } 
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
