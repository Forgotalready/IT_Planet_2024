using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake(){
        Instance = this;
    }

    public void Add(Item item)
    {
        clearInvetory();
        Items.Add(item);
        ListItems();
    }

    public void clearInvetory()
    {
        GameObject inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        foreach (Transform item in inventoryObject.transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void Remove(int id){
        int index = 0;
        for (int i = 0; i < Items.Count; i++) {
            if (Items[i].id == id) index = i; 
        }
        Items.RemoveAt(index);


        destroyImage(id);

        clearInvetory();
        ListItems();
    }
    private void destroyImage(int index) {
        GameObject inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        foreach (Transform child in inventoryObject.GetComponent<Transform>()) {
            GameObject childObject = child.GameObject();
            if (child.GetComponent<Item>().id == index)
            {
                Destroy(childObject);
                return;
            }
        }
    }

    public void ListItems()
    {
        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = item.icon;
            Item item1 = obj.GetComponent<Item>();
            item1.id = item.id;
        }
    }
}
