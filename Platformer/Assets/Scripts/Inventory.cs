using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectCanBeUse;
    public GameObject selectedItem;
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        selectedItem = objectCanBeUse[0];
    }

    public void ChangeItem(int index)
    {
        if (objectCanBeUse != null) selectedItem = objectCanBeUse[index];
        else Debug.Log("No list of object Found for inventory");
    }
}
