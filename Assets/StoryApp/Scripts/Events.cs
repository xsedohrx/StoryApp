using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        UserInput.onClick += GetStory;
    }

    void GetStory() {


    }

    private void OnDisable()
    {
        UserInput.onClick -= GetStory;
    }


}
