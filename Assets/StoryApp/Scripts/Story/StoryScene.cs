using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryApp
{
    public class StoryScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(StoryLibraryManager.Instance);
        }

    }
}