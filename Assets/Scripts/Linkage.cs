//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Linkage : MonoBehaviour
//{
//    private LineRenderer _lineRenderer;
//    public void Start()
//    {
//        _lineRenderer = this.gameObject.AddComponent<LineRenderer>();
//        _lineRenderer.startWidth = 0.2f;
//        _lineRenderer.enabled = false;
//    }

//    private Vector3 _initialPosition;
//    private Vector3 _currentPosition;
//    public void Update()
//    {



//        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Collider>() == this.gameObject.GetComponent<Collider>())
//        {
//            //_initialPosition = GetCurrentMousePosition().GetValueOrDefault();
//            _initialPosition = this.gameObject.transform.position;
//            _lineRenderer.SetPosition(0, _initialPosition);
//            _lineRenderer.positionCount = 1;
//            _lineRenderer.enabled = true;
//        }
//        else if (Input.GetMouseButton(0))
//        {
//            _currentPosition = GetCurrentMousePosition().GetValueOrDefault();
//            _lineRenderer.positionCount = 2;
//            _lineRenderer.SetPosition(1, _currentPosition);

//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            //_lineRenderer.enabled = false;
//            var releasePosition = GetCurrentMousePosition().GetValueOrDefault();
//            var direction = releasePosition - _initialPosition;
//            Debug.Log("Process direction " + direction);
//        }
//    }

//    private Vector3? GetCurrentMousePosition()
//    {
//        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        var plane = new Plane(Vector3.forward, Vector3.zero);

//        //RaycastHit hit;
//        //// Does the ray intersect any objects excluding the player layer
//        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit))
//        //{
//        //    if(hit.collider.gameObject.GetComponent<Collider>() == this.gameObject.GetComponent<Collider>())
//        //    {
//        //        return this.transform.position;
//        //    }
//        //}

//        float rayDistance;
//        if (plane.Raycast(ray, out rayDistance))
//        {
//            return ray.GetPoint(rayDistance);

//        }

//        return null;
//    }
//}
