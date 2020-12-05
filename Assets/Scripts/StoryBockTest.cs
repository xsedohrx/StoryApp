using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBockTest : MonoBehaviour  // NOTE: Since IDamageable is generic we had to choose the type when including
{
    //public GameObject storyBlock { get; set; }

    public GameObject GetStoryGO()
    {
        //storyBlock = this.gameObject;

        return this.gameObject;
        //GetComponent<MeshRenderer>().material.color = Color.red;
    }
}