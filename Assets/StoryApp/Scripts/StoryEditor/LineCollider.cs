using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using StoryApp;

public class LineCollider : MonoBehaviour
{

    private void OnEnable()
    {
        Node.OnColliderInitialSettings += ColliderInitialSettings;
    }

    public CapsuleCollider ColliderInitialSettings(GameObject colliderObj, float radius)
    {
        CapsuleCollider capsule = colliderObj.GetComponent<CapsuleCollider>();
        capsule.radius = radius * 0.5f;
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis for easier "LookAt" orientation

        return capsule;
    }
    private void OnDisable()
    {
        Node.OnColliderInitialSettings -= ColliderInitialSettings;
    }
}



//         capsule.radius = lineLink.startWidth * 0.5f;