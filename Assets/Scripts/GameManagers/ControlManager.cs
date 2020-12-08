//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using UnityEngine;

////control manager is drag lines in the samplescene
//public class ControlManager : MonoBehaviour
//{
//    private static ControlManager _instance;
//    public static ControlManager Instance
//    {
//        get
//        {
//            if (_instance == null)
//                Debug.LogError("ControlManager is NULL");

//            return _instance;
//        }
//    }

//    void Awake()
//    {
//        _instance = this;
//    }

//    private LineRenderer _lineRenderer;
//    private Vector3 _initialPosition;
//    private Vector3 _currentPosition;


//    public void Start()
//    {
//        _lineRenderer = gameObject.AddComponent<LineRenderer>();
//        _lineRenderer.startWidth = 0.2f;
//        _lineRenderer.enabled = false;
//    }

//    private void Update()
//    {
//        ElementConnection();
//    }

//    private void ElementConnection()
//    {
//        var selectedObject = SelectStory(out GameObject hitReturn);
//        var releaseObject = UnSelectStory(out GameObject hitDeselect);


//        //Button press detection
//        if (Input.GetMouseButtonDown(0) && selectedObject != null)
//        {
//            _initialPosition = selectedObject.transform.position;
//            _lineRenderer.SetPosition(0, _initialPosition);
//            _lineRenderer.positionCount = 1;
//            _lineRenderer.enabled = true;
//            //TODO Pressed element should be the output node
//        }
//        else if (Input.GetMouseButton(0))
//        {
//            _currentPosition = GetCurrentMousePosition().GetValueOrDefault();
//            _lineRenderer.positionCount = 2;
//            _lineRenderer.SetPosition(1, _currentPosition);


//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            //If there is nothing to drag to reset position of the line.
//            if (objectClicked())
//            {
//                //_lineRenderer.enabled = false;
//                var releasePosition = releaseObject.transform.position;
//                _lineRenderer.positionCount = 2;
//                _lineRenderer.SetPosition(1, releasePosition);
//            }
//            else
//            {
//                _lineRenderer.positionCount = 2;
//                _lineRenderer.SetPosition(1, _initialPosition);

//                _lineRenderer.enabled = false;
//            }
//        }
//    }

//    private static GameObject SelectStory(out GameObject hitReturn)
//    {
//        hitReturn = null;
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hitInfo;

//            if (Physics.Raycast(rayOrigin, out hitInfo))
//            {
//                var obj = hitInfo.collider.gameObject; // This!
//                if (obj != null)
//                {
//                    Debug.Log(obj.ToString());
//                    hitReturn = obj;
//                }
//            }
//        }
//        return hitReturn;
//    }

//    private static GameObject UnSelectStory(out GameObject hitDeselect)
//    {
//        hitDeselect = null;
//        if (Input.GetMouseButtonUp(0))
//        {
//            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hitInfo;

//            if (Physics.Raycast(rayOrigin, out hitInfo))
//            {
//                var obj = hitInfo.collider.gameObject; // This!
//                if (obj != null)
//                {
//                    Debug.Log(obj.ToString());
//                    hitDeselect = obj;
//                }
//            }
//        }
//        return hitDeselect;
//    }

//    private Vector3? GetCurrentMousePosition()
//    {
//        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        var plane = new Plane(Vector3.forward, Vector3.zero);

//        float rayDistance;
//        if (plane.Raycast(ray, out rayDistance))
//        {
//            return ray.GetPoint(rayDistance);

//        }

//        return null;
//    }

//    //Object detection when clicked
//    bool objectClicked()
//    { 
//        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hitInfo;

//        if (Physics.Raycast(rayOrigin, out hitInfo))
//        {
//            var obj = hitInfo.collider.gameObject; // This!
//            if (obj != null)
//            {
//                Debug.Log(obj.ToString());
//                return obj;
//            }
//        }
//        return false;

//    }
//}
