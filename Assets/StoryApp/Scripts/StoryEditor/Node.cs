using System;
using System.Collections.Generic;
using UnityEngine;


namespace StoryApp
{
    // Required Components
    [RequireComponent(typeof(LineCollider))]
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(LineRendererSettings))]
    [RequireComponent(typeof(ColorPallete))]
    public class Node : MonoBehaviour
    {
        //Prefabs
        public GameObject linkPrefab;

        // Enums
        public enum Access
        {
            open,
            closed,
        }

        public enum NodeColor
        {
            draggable,
            highlighted,
            selectable
        }

        public enum ProcessOutLinks
        {
            linkA = 0,
            linkB = 1,
            linkC = 2,
            linkD = 3
        }
        // ----------------------------------

        // Properties
        public Access access { get; set; }
        public NodeColor nodeColor { get; set; }
        public ProcessOutLinks processOutLinks { get; set; }
        public int nodeID { get; set; }
        // ----------------------------------

        //INPUT NODES
        public GameObject inNode { get; set; }

        [SerializeField] public int inNodeCount;
        public SortedDictionary<string, GameObject> inNodeDictionary { get; set; }

        // OUTPUTS NODES
        [SerializeField] public int outNodeCount;
        public GameObject outNodeA { get; set; }
        public GameObject outNodeB { get; set; }
        public SortedDictionary<string, GameObject> outNodeDictionary { get; set; }
        // ----------------------------------

        // INPUTS LINKS
        [SerializeField] public int inLinkCount;
        public GameObject inLink { get; set; }
        public SortedDictionary<string, GameObject> inLinkDictionary { get; set; }
        // ----------------------------------

        // OUTPUTS LINKS
        [SerializeField] public int outLinkCount;
        public int maxOutLinks { get; set; }
        public GameObject outLinkA { get; set; }
        public GameObject outLinkB { get; set; }
        public GameObject outLinkC { get; set; }
        public GameObject outLinkD { get; set; }
        public SortedDictionary<string, GameObject> outLinkDictionary { get; set; }
        // ----------------------------------
        // Actions& Events
        public static Action<int, LineRenderer, Vector3, Vector3> OnLineInitalSettings;
        public static Func<GameObject, float, CapsuleCollider> OnColliderInitialSettings;
        // ----------------------------------


        private void Awake()
        {
            inNodeCount = 0;
            inNodeDictionary = new SortedDictionary<string, GameObject>();

            outNodeCount = 0;
            outNodeDictionary = new SortedDictionary<string, GameObject>();

            inLinkCount = 0;
            inLinkDictionary = new SortedDictionary<string, GameObject>();

            maxOutLinks = 4;
            outLinkCount = 0;
            outLinkDictionary = new SortedDictionary<string, GameObject>();

            access = Access.open;
        }

        // Node Helper Functions
        public void AddInNodeDict(string nodeName, GameObject gameObject)
        {
            if (!inNodeDictionary.ContainsKey(nodeName))
            {
                inNodeDictionary.Add(nodeName, gameObject);
                inNodeCount = inNodeDictionary.Count;
            }
            else
            {
                Debug.Log("These Nodes are already connected");
            }
        }
        public void RemoveInNodeDict(string nodeName)
        {
            if (inNodeDictionary.TryGetValue(nodeName, out GameObject gameObject))
            {
                inNodeDictionary.Remove(nodeName);
                //Destroy(linkObject);
                inNodeCount = inNodeDictionary.Count;
            }
        }

        public void AddOutNodeDict(string nodeName, GameObject gameObject)
        {
            if (!outNodeDictionary.ContainsKey(nodeName))
            {
                outNodeDictionary.Add(nodeName, gameObject);
            }
            else
            {
                Debug.Log("These Nodes are already connected");
            }
        }
        public bool duplicateNodeConnectionCheck(string nodeName)
        {
            if (inNodeDictionary.ContainsKey(nodeName))
            {
                Debug.Log("These Nodes were connected - DELETING");
                return true;
            }
            else
            {
                Debug.Log("These Nodes are NOT connected");
                return false;
            }
        }
        public bool isCapacity()
        {
            return this.outLinkCount == this.maxOutLinks ? true : false;
        }
        // ---------------------

        // Link Helper Functions

        public void AssignOutLink(int linknumber, GameObject outLink, GameObject outNodeObj, out string name)
        {
            processOutLinks = (ProcessOutLinks)linknumber;
            switch (processOutLinks)
            {
                case ProcessOutLinks.linkA:
                    name = outNodeObj.name + "Link" + "A";
                    this.outLinkA = outLink;
                    break;
                case ProcessOutLinks.linkB:
                    name = outNodeObj.name + "Link" + "B";
                    this.outLinkB = outLink;
                    break;
                case ProcessOutLinks.linkC:
                    name = outNodeObj.name + "Link" + "C";
                    this.outLinkC = outLink;
                    break;
                case ProcessOutLinks.linkD:
                    name = outNodeObj.name + "Link" + "D";
                    this.outLinkD = outLink;
                    break;
                default:
                    name = "No Link?";
                    break;
            }
        }

        public void AddOutLinkDict(string nodeName, GameObject gameObject)
        {
            if (!outLinkDictionary.ContainsKey(nodeName))
            {
                outLinkDictionary.Add(nodeName, gameObject);
                outLinkCount = this.outLinkDictionary.Count;
            }
        }
        public void RemoveOutLinkDict(string linkName)
        {
            if (outLinkDictionary.TryGetValue(linkName, out GameObject linkObject))
            {
                outLinkDictionary.Remove(linkName);
                Destroy(linkObject);
                outLinkCount = outLinkDictionary.Count;
            }
        }
        public void AddInLinkDict(string nodeName, GameObject gameObject)
        {
            if (!inLinkDictionary.ContainsKey(nodeName))
            {
                inLinkDictionary.Add(nodeName, gameObject);
                inLinkCount = inLinkDictionary.Count;
            }
        }
        public void RemoveInLinksDict(string linkName)
        {
            if (inLinkDictionary.TryGetValue(linkName, out GameObject linkObject))
            {
                inLinkDictionary.Remove(linkName);
                //Destroy(linkObject);
                inLinkCount = inLinkDictionary.Count;
            }
        }
        public void DragInLinks(Vector3 mousePos)
        {
            foreach (KeyValuePair<string, GameObject> valuePair in inLinkDictionary)
            {
                for (int i = 0; i < valuePair.Value.transform.childCount; i++)
                {
                    Debug.Log(valuePair.Key);
                    if (valuePair.Value.transform.GetChild(i).gameObject.name == valuePair.Key.ToString())
                    {
                        valuePair.Value.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(1, mousePos);
                    }
                }
            }
        }
        public void DragOutLinks(Vector3 mousePos)
        {
            foreach (KeyValuePair<string, GameObject> valuePair in outLinkDictionary)
            {
                valuePair.Value.GetComponent<LineRenderer>().SetPosition(0, mousePos);
            }
        }
        // Spawn Link start position
        public GameObject LinkSpawner(GameObject nodeGameObj, out LineRenderer lineLink, out CapsuleCollider capsule, out Link currentLink, out string linkName, out GameObject currentGO)
        {
            currentGO = Instantiate(linkPrefab, nodeGameObj.transform, true);
            {
                //Assignments
                currentLink = currentGO.GetComponent<Link>();
                Node startNode = nodeGameObj.GetComponent<Node>();
                lineLink = currentLink.GetComponent<LineRenderer>();

                // Link Assignments
                currentLink.linkIndex = 0;
                currentLink.startPos = nodeGameObj.transform.position;
                currentLink.linkOriginNode = nodeGameObj;

                //Node Assignments for the node that has just been clicked on to initiate the LineRenderer;
                AssignOutLink(outLinkCount, currentGO, nodeGameObj, out string name);
                linkName = name;
                currentLink.name = name;

                // LineRenderer Settings
                OnLineInitalSettings?.Invoke(outLinkCount, lineLink, nodeGameObj.transform.position, nodeGameObj.transform.position);

                // Edge Collider Assignments
                capsule = OnColliderInitialSettings?.Invoke(currentGO, lineLink.startWidth);
            }
            // Add Link GameObejct to List
            return currentGO;
        }
        // ---------------------
        // Collider Helper Functions
        public void DragInColliders(Vector3 endPos)
        {
            foreach (KeyValuePair<string, GameObject> valuePair in inLinkDictionary)
            {
                for (int i = 0; i < valuePair.Value.transform.childCount; i++)
                {
                    Debug.Log(valuePair.Key);
                    if (valuePair.Value.transform.GetChild(i).gameObject.name == valuePair.Key.ToString())
                    {
                        Vector3 startPos = valuePair.Value.transform.position;
                        //Vector3 endPos = this.gameObject.transform.position;

                        CapsuleCollider capsule = valuePair.Value.transform.GetChild(i).gameObject.GetComponent<CapsuleCollider>();
                        capsule.transform.position = startPos + (endPos - startPos) / 2;
                        capsule.transform.LookAt(startPos);
                        capsule.height = (endPos - startPos).magnitude;
                    }
                }
            }
        }
        public void DragOutColliders(Vector3 startPos)
        {
            foreach (KeyValuePair<string, GameObject> valuePair in outLinkDictionary)
            {
                Vector3 endPos = valuePair.Value.GetComponent<Link>().linkDestinationNode.gameObject.transform.position;

                CapsuleCollider capsule = valuePair.Value.GetComponent<CapsuleCollider>();
                capsule.transform.position = startPos + (endPos - startPos) / 2;
                capsule.transform.LookAt(startPos);
                capsule.height = (endPos - startPos).magnitude;
            }
        }
        // ---------------------
    }
}