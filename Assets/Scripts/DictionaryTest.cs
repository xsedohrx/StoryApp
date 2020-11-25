//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DictionaryTest : MonoBehaviour
//{


//    Dictionary<int, string> testDictionary;
//    // Start is called before the first frame update
//    void Start()
//    {
//        testDictionary = new Dictionary<int, string>();

//        int index = 0;
//        for (int i = 0; i < 9; i++)
//        {
//            index = testDictionary.Count;

//            testDictionary.Add(index + 1, "value" + (index + 1).ToString());
//        }


//        testDictionary.Remove(4);
//        testDictionary.Remove(6);
//        testDictionary.Remove(9);


//        testDictionary.Add(testDictionary.Count + 1, "value" + (testDictionary.Count + 1).ToString());

//        foreach (var key in testDictionary)
//        {
//            Debug.Log(key);
//        }

//    }

//}
