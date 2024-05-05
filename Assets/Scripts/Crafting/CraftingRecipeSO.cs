using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Recipe",menuName = "Survival Game/Crafting/New Recipe")]
public class CraftingRecipeSO : ScriptableObject
{
    public Sprite icon;
    public string recipeName;

    public CraftingRequirement[] requirements;

    public float craftingTime;

    public ItemSO outcome;
    public int outcomeAmount = 1;
}
