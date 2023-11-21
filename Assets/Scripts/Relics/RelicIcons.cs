using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RelicIcons : MonoBehaviour
{
    public GameObject relicPrefab;
    public List<int> relicLevel;
    public List<string> relicStringList;
    public List<GameObject> relicsToLoad;
    public void UpdateIconMenu(string relicToUpdate)
    {
        if(!relicStringList.Contains(relicToUpdate)) //If player got a new relic
        {
            relicStringList.Add(relicToUpdate);
            relicLevel.Add(1);
        }
    }
    public void LoadRelicIcons()
    {
        UnLoadIcons();
        Transform ip = GameObject.Find("IconPanel").transform;
        int i = 0;
        foreach(string loadIn in relicStringList)
        {
            GameObject objToSpawn = Instantiate(relicPrefab, transform.position, Quaternion.identity);
            objToSpawn.transform.SetParent(ip);
            objToSpawn.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find(loadIn).GetComponent<SpriteRenderer>().sprite;
            objToSpawn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Lv " + relicLevel[i].ToString();
            relicsToLoad.Add(objToSpawn);
            i++;
        }
    }
    public void UnLoadIcons()
    {
        foreach (GameObject loudOut in relicsToLoad)
        {
            Destroy(loudOut);
        }
        relicsToLoad.Clear();
    }
}
