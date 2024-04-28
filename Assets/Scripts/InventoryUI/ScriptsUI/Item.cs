using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(fileName="Item Name", menuName = "Item/Create New Item")]
public class Item : MonoBehaviour
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
}
