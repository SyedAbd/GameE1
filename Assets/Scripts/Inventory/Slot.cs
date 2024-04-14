using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private DragDropHandler dragDropHandler;
    private InventoryManager inventory;



    public ItemSO data;
    public int stackSize;

    public Image icon;
    public Text stackText;

    private bool isEmpty;

    public bool IsEmpty => isEmpty;

    private void Start()
    {
        dragDropHandler = GetComponentInParent<DragDropHandler>();
        inventory = GetComponentInParent<InventoryManager>();

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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedFrom = this; 
            dragDropHandler.isDragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            if (dragDropHandler.slotDraggedTo == null)
            {
                dragDropHandler.slotDraggedFrom.Drop();
                dragDropHandler.isDragging = false;
            }

            else if (dragDropHandler.slotDraggedTo != null)
            {
                inventory.DragDrop (dragDropHandler.slotDraggedFrom, dragDropHandler.slotDraggedTo);
                dragDropHandler.isDragging = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedTo = this;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedTo = null;
        }
    }
}
