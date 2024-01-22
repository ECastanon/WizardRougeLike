using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicIcons : MonoBehaviour
{
    public GameObject relicPrefab;
    public List<int> relicLevel = new List<int>();
    public List<string> relicStringList = new List<string>();
    public List<GameObject> relicsToLoad = new List<GameObject>();
    public void UpdateIconMenu(string relicToUpdate)
    {
        //Debug.Log(relicStringList.Count);
        Debug.Log("TEST-1 : " + relicToUpdate); //If players picks a first relic
        if(!relicStringList.Contains(relicToUpdate)) //If player got a new relic
        {
            Debug.Log("TEST-2");
            relicStringList.Add(relicToUpdate);
            relicLevel.Add(1);
        } else
        if(relicStringList.Contains(relicToUpdate)) //If player got a duplicate relic
        {
            Debug.Log("TEST-4");
            int index = relicStringList.IndexOf(relicToUpdate);
            relicLevel[index] += 1;
        }
    }
    public void LoadRelicIcons()
    {
        Debug.Log(relicStringList.Count);
        if(relicStringList.Count > 0)
        {
            UnLoadIcons();
            Transform ip = GameObject.Find("IconPanel").transform;
            int i = 0;
            foreach(string loadIn in relicStringList)
            {
                GameObject objToSpawn = Instantiate(relicPrefab, transform.position, Quaternion.identity);
                objToSpawn.transform.SetParent(ip);
                objToSpawn.transform.GetChild(3).GetComponent<Image>().sprite = GameObject.Find(loadIn).GetComponent<SpriteRenderer>().sprite;
                objToSpawn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Lv " + relicLevel[i].ToString();
                relicsToLoad.Add(objToSpawn);
                i++;
            }  
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
    public void StoreData() //Sends current information to StaticData
    {
        StaticData.relicLevel_ = relicLevel;
        StaticData.relicStringList_ = relicStringList;
    }
    public void LoadData() //Applies information from StaticData
    {
        relicLevel = StaticData.relicLevel_;
        relicStringList = StaticData.relicStringList_;
    }
}
