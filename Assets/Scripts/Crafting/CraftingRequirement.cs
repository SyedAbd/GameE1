using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Requirement", menuName = "Survival Game/Crafting/New Requirement")]
public class CraftingRequirement : ScriptableObject
{
    public ItemSO data;
    public int amountNeeded;
}
