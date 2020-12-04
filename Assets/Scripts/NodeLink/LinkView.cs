using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkView : MonoBehaviour
{
    // corresponding node
    public Link _link;

    // object / geometry
    public void Init(Link _link)
    {
        gameObject.name = "Link" + _link.linkIndex.ToString();
        gameObject.transform.position = _link.startPos;
        //gameObject.GetComponent<LineRenderer>();
    }
    
}
