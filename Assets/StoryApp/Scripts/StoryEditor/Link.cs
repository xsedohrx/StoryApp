using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryApp
{
    public class Link : MonoBehaviour
    {
        // Link Properties
        public string linkName { get; set; }
        public int linkID { get; set; }
        public int linkIndex { get; set; }
        public Vector2 startPos { get; set; }
        public Vector2 endPos { get; set; }
        public Vector2 currentPos { get; set; }

        // Node Information
        public GameObject linkOriginNode { get; set; }
        public GameObject linkDestinationNode { get; set; }

      
        public void Reset(GameObject nullObj)
        {
            nullObj = null;
        }

    }
}
