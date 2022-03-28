using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemClass> inventoryItems;




    public void AddToInventory(ItemClass ItemToAdd)
    {
        inventoryItems.Add(ItemToAdd);
    }

    public void RemoveFromInventory(ItemClass ItemToRemove)
    {
        inventoryItems.Remove(ItemToRemove);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
