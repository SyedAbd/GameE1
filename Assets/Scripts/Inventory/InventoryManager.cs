using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool opened;
    public KeyCode inventoryKey = KeyCode.Tab;


    [Header("Settings")]
    public int inventorySize = 24;


    [Header("Refs")]
    public GameObject dropModel;
    public Transform dropPos;
    public GameObject slotTemplate;
    public Transform contentHolder;


    private Slot[] inventorySlots;
    [SerializeField]private Slot[] allSlots;


    private void Start()
    {
        GenerateSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
            opened = !opened;


        if (opened)
        {
            transform.localPosition = new Vector3(0,0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(-1000,0,0);
        }
    }


    private void GenerateSlots()
    {
        List<Slot>inventorySlots_ = new List<Slot>();
        List<Slot> allSlots_ = new List<Slot>();

        for (int i =0; i < allSlots.Length;i++)
        {
            allSlots_.Add(allSlots[i]);
        }


        for(int i = 0; i < inventorySize; i++)
        {
            Slot slot = Instantiate(slotTemplate.gameObject,contentHolder).GetComponent<Slot>();

            inventorySlots_.Add(slot);
            allSlots_.Add(slot);
        }

        inventorySlots = inventorySlots_.ToArray();
        allSlots = allSlots_.ToArray();
    }

    public void AddItem(Pickup pickUp)
    {
        if (pickUp.data.isStackable)
        {
            Slot stackableSlot = null;

            // TRY FINDING STACKABLE SLOT
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (!inventorySlots[i].IsEmpty)
                {
                    if (inventorySlots[i].data == pickUp.data && inventorySlots[i].stackSize < pickUp.data.maxStack)
                    {
                        stackableSlot = inventorySlots[i];
                        break;
                    }

                }
            }

            if (stackableSlot != null)
            {

                // IF IT CANNOT FIT THE PICKED UP AMOUNT
                if (stackableSlot.stackSize + pickUp.stackSize > pickUp.data.maxStack)
                {
                    int amountLeft = (stackableSlot.stackSize + pickUp.stackSize) - pickUp.data.maxStack;



                    // ADD IT TO THE STACKABLE SLOT
                    stackableSlot.AddItemToSlot(pickUp.data, pickUp.data.maxStack);

                    // TRY FIND A NEW EMPTY STACK
                    for (int i = 0; i < inventorySlots.Length; i++)
                    {
                        if (inventorySlots[i].IsEmpty)
                        {
                            inventorySlots[i].AddItemToSlot(pickUp.data, amountLeft);
                            inventorySlots[i].UpdateSlot();

                            break;
                        }
                    }



                    Destroy(pickUp.gameObject);
                }
                // IF IT CAN FIT THE PICKED UP AMOUNT
                else
                {
                    stackableSlot.AddStackAmount(pickUp.stackSize);

                    Destroy(pickUp.gameObject);
                }

                stackableSlot.UpdateSlot();
            }
            else
            {
                Slot emptySlot = null;


                // FIND EMPTY SLOT
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].IsEmpty)
                    {
                        emptySlot = inventorySlots[i];
                        break;
                    }
                }

                // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
                if (emptySlot != null)
                {
                    emptySlot.AddItemToSlot(pickUp.data, pickUp.stackSize);
                    emptySlot.UpdateSlot();

                    Destroy(pickUp.gameObject);
                }
                else
                {
                    pickUp.transform.position = dropPos.position;
                }
            }

        }
        else
        {
            Slot emptySlot = null;


            // FIND EMPTY SLOT
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].IsEmpty)
                {
                    emptySlot = inventorySlots[i];
                    break;
                }
            }

            // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
            if (emptySlot != null)
            {
                emptySlot.AddItemToSlot(pickUp.data, pickUp.stackSize);
                emptySlot.UpdateSlot();

                Destroy(pickUp.gameObject);
            }
            else
            {
                pickUp.transform.position = dropPos.position;
            }

        }
    }

    public void DropItem(Slot slot)
    {
        Pickup pickup = Instantiate(dropModel, dropPos).AddComponent<Pickup>();

        pickup.data = slot.data;
        pickup.stackSize = slot.stackSize;

        slot.Clean();
    }

}
