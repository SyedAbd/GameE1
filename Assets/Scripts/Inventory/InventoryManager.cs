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
}
