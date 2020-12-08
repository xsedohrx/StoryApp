using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects / Data Container")]
public class DataContainer : ScriptableObject
{
    [field: SerializeField] public List<GameObject> gameObjList { get; set; }

    private void OnEnable()
    {
        // Currently ensuring that the scriptable object data values are reset between unity versions
        this.gameObjList.Clear();
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
    }
}