using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberOfGears : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemScript[] allItems = FindObjectsOfType<ItemScript>();
        int allGears = 0;
        
        foreach(ItemScript item in allItems)
        {
            if(item.ItemType == ItemType.Gear)
            {
                allGears += 1;
            }
        }

        TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>();
        text.SetText(allGears.ToString());
    }
}
