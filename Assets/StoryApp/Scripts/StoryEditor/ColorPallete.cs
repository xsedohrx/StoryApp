using StoryApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPallete : MonoBehaviour
{
    //Serialized Fields
    public Color selectedColor;

    public Color Red = Color.red;
    public Color Green = Color.green;
    public Color Blue = Color.blue;
    public Color Purple = Color.magenta;
    public Color White = Color.white;

    public Color Yellow = Color.yellow;
    public Color Grey = Color.grey;
    public enum NodeColor
    {
        draggable,
        highlighted,
        selectable
    }
    public NodeColor nodeColor;


    public enum OutLinkColor
    {
        linkA = 0,
        linkB = 1,
        linkC = 2,
        linkD = 3
    }
    public OutLinkColor outLinkColor;
    // ----------------------------------


    private void OnEnable()
    {
        MouseController.OnNodeColor += ColorNode;
        //Node.OnLineColor += ColorLinks;
        LineRendererSettings.OnLineColor += ColorLinks;
    }
    private void Awake()
    {

    }

    public void ColorLinks(int linknumber, LineRenderer lineToColor)
    {
        outLinkColor = (OutLinkColor)linknumber;
        switch (outLinkColor)
        {
            case OutLinkColor.linkA:
                lineToColor.startColor = Red;
                lineToColor.endColor = Red;
                break;
            case OutLinkColor.linkB:
                lineToColor.startColor = Green;
                lineToColor.endColor = Green;
                break;
            case OutLinkColor.linkC:
                lineToColor.startColor = Blue;
                lineToColor.endColor = Blue;
                break;
            case OutLinkColor.linkD:
                lineToColor.startColor = Purple;
                lineToColor.endColor = Purple;
                break;
            default:
                lineToColor.startColor = Grey;
                lineToColor.endColor = Grey;
                break;
        }
    }

public void ColorNode(GameObject objToColor, NodeColor nodeColor)
    {
        switch (nodeColor)
        {
            case NodeColor.draggable:
                objToColor.GetComponent<MeshRenderer>().material.color = Grey;
                break;
            case NodeColor.highlighted:
                objToColor.GetComponent<MeshRenderer>().material.color = Yellow;
                break;
            case NodeColor.selectable:
                objToColor.GetComponent<MeshRenderer>().material.color = new Color(0.9f, 1, 0);
                break;
        }
    }

}
