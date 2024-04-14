using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemSO data;
    public int stackSize;

    public Image icon;
    public Text stackText;

    private bool isEmpty;

    public bool IsEmpty => isEmpty;

    private void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (data == null)
        {
            isEmpty = true;

            icon.gameObject.SetActive(false);
            stackText.gameObject.SetActive(false);
        }

        else
        {
            isEmpty = false;

            icon.sprite = data.icon;
            stackText.text = $"x{stackSize}";

            icon.gameObject.SetActive(true);
            stackText.gameObject.SetActive(true);
        }
    }

    public void AddItemToSlot(ItemSO data_, int stackSize_)
    {
        data = data_;
        stackSize = stackSize_;
    }

    public void AddStackAmount( int stackSize_)
    {
        
        stackSize += stackSize_;
    }

    public void Drop()
    {
        GetComponentInParent<InventoryManager>().DropItem(this);
    }

    public void Clean()
    {
        data = null;
        stackSize = 0;

        UpdateSlot();
    }
}
