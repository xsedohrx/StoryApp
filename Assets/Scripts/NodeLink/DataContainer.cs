using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects / Data Container")]
public class DataContainer : ScriptableObject
{
    public List<GameObject> gameObjList { get; set; }
    public List<GameObject> linkList { get; set; }
    [field: SerializeField] public string mySeriealizedString { get; set; }


    //public Dictionary<int, GameObject> gameObjDictionary { get; set; }
    //public Link link;


    private int temp;

    public string myString;


    private void OnEnable()
    {
        temp = 0;

        gameObjList.Clear();
        linkList.Clear();
    }

    public void AddLinkToList(GameObject link)
    {
        linkList.Add(link);
    }

    public GameObject ReturnLinkFromList(int index)
    {
        return linkList[index];
    }

    public GameObject RemoveLinkFromList(int index)
    {
        linkList.RemoveAt(index);
        return linkList[index];
    }


    public LineRenderer ReturnLineRendererFromList(int index)
    {
        return gameObjList[index].gameObject.GetComponent<LineRenderer>();
    }

    public void AddGameObjList(GameObject gameObj)
    {
        gameObjList.Add(gameObj);
    }
    public void RemoveGameObjList(int index)
    {
        gameObjList.RemoveAt(index);
        //return gameObjList[index];
    }

}

// SAVED CODE
//public void AddGameobjectDic(int index, GameObject gameObject)
//{
//    gameObjDictionary.Add(index, gameObject);
//}
//public GameObject RemoveGameobjectDic(int index, GameObject gameObj)
//{
//    gameObjDictionary.TryGetValue(index, out gameObj);
//    return gameObj;
//}