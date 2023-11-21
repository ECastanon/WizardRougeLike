using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicIcons : MonoBehaviour
{
    public List<int> relicLevel;
    public List<string> relicStringList;
    public void UpdateIconMenu(string relicToUpdate)
    {
        if(!relicStringList.Contains(relicToUpdate)) //If player got a new relic
        {
            relicStringList.Add(relicToUpdate);
            relicLevel.Add(1);
        }
    }
}
