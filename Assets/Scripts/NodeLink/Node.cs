using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName { get; set; }

    public int nodeID { get; set; }

    // INPUTS
    public GameObject inLink { get; set; }
    public int inputLinkID { get; set; }
    public GameObject inNode { get; set; }
    public int inputNodeID { get; set; }


    // OUTPUTS 
    // A
    public GameObject outLinkA { get; set; }  // Possibly Temp
    public int outLinkIdA { get; set; }
    public GameObject outNodeA { get; set; } // Possibly Temp
    public int outNodeIdA { get; set; }

    // B
    public GameObject outLinkB { get; set; }
    public int outLinkIdB { get; set; }

    public GameObject outNodeB { get; set; }
    public int outNodeIdB { get; set; }

    // ----------------------------------

    public int linkCount { get; set; }

    public int maxLinks { get; set; }

    public bool noSpace { get; set; }

    public static bool queriesHitTriggers = true;


    private void Awake()
    {
        nodeName = "node";

        maxLinks = 2;

        noSpace = false;
    }


    public bool isCapacity()
    {
        if(this.linkCount == this.maxLinks)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string ReturnName()
    {
        return this.nodeName;
    }


    // OnMouseDown
    private void OnMouseDown()
    {
        ReturnDownNode();
    }
    public void ReturnDownNode()
    {
        Controller.Instance.returnHitDownNodeGO = this.gameObject;
    }

    // OnMouseUp
    private void OnMouseOver()
    {
        ReturnOverNode();
    }
    public void ReturnOverNode()
    {
        Controller.Instance.returnHitUpNodeGO = this.gameObject;
    }

    void OnMouseExit()
    {
        ReturnOffNode();
    }
    public void ReturnOffNode()
    {
        Controller.Instance.returnHitUpNodeGO = null;
    }

    // OnMouseButtonUp (Up on the same object)
    private void OnMouseUpAsButton()
    {
        ReturnUpButtonNode();
    }
    public void ReturnUpButtonNode()
    {
        Controller.Instance.returnHitButtonUpNodeGO = this.gameObject;
    }

    public GameObject ReturnDownGameobject()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            var obj = hitInfo.collider.gameObject;
            if (obj != null)
            {
                //Debug.Log("This was called from the Node" + obj.ToString());
                return obj;
            }
        }
        Debug.Log("Object Inactive - Might receive a null error");
        return null;
    }




    public GameObject ReturnUpGameobject()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            var obj = hitInfo.collider.gameObject; // This!
            if (obj != null)
            {
                //Debug.Log(obj.ToString());
                return obj;
            }
        }
        Debug.Log("Object Inactive - Might receive a null error");
        return null;
    }








    /*TBA
     * text box
     * image
     * 
     */

}