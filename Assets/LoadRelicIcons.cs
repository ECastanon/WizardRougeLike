using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRelicIcons : MonoBehaviour
{
    private GameObject RCPanel;
    public List<string> relicList;
    public List<GameObject> relicsInMenu;
    public GameObject relicIconPrefab;
    void OnEnable()
    {
        Debug.Log("test");
        RCPanel = GameObject.Find("RelicPanel");
        relicList.Clear();
        relicList = RCPanel.GetComponent<RelicIcons>().relicStringList;

        if(relicList.Count > 0)
        {
            LoadIn();
        }
    }

    private void LoadIn()
    {
        foreach(string icon in relicList)
        {
            GameObject spawnedObj = Instantiate(relicIconPrefab, transform.position, Quaternion.identity);
            spawnedObj.transform.SetParent(this.gameObject.transform);
            relicsInMenu.Add(spawnedObj);
            spawnedObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find(icon).GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void UnLoad()
    {
        foreach(var obj in relicsInMenu)
        {
            Destroy(obj);
        }
        relicsInMenu.Clear();
    }
}
