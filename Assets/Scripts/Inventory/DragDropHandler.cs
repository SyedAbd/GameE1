using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragDropHandler : MonoBehaviour
{
    [HideInInspector] public bool isDragging;
    public Slot slotDraggedFrom;
    public Slot slotDraggedTo;
    [Space]
    public Image dragDropIconImage;

    private void Update()
    {
        if (isDragging && slotDraggedFrom != null)
        {
            dragDropIconImage.sprite = slotDraggedFrom.icon.sprite;
            dragDropIconImage.transform.position = Input.mousePosition;

        }
        else
        {
            dragDropIconImage.transform.position = new Vector3(-10000, 0, 0);
        }
    }
}
