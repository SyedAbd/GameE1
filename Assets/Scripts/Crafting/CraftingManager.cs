using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private InventoryManager inventory;
    public RecipeTemplate recipeTemplate;
    public CraftingRecipeSO[] recipes;
    public Transform contentHolder;

    public RecipeTemplate recipeInCraft;
    private bool isCrafting;
    private float currentTimer; 
    private float timer;

    private void Start()
    {
        inventory = GetComponentInParent<InventoryManager>();
        GenerateRecipes();
    }

    private void Update()
    {
        if (isCrafting)
        {
            if (currentTimer < timer)
            {
                recipeInCraft.timerText.text = currentTimer.ToString("f2");
            }
            else
            {
                recipeInCraft.timerText.text = "";
                inventory.AddItem(recipeInCraft.recipe.outcome, recipeInCraft.recipe.outcomeAmount);
                isCrafting = false;
            }
            currentTimer += Time.deltaTime;
        }
    }
    public void GenerateRecipes()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            RecipeTemplate recipe = Instantiate(recipeTemplate.gameObject,contentHolder).GetComponent<RecipeTemplate>();
            recipe.recipe = recipes[i];

            recipe.nameText.text = recipes[i].recipeName;
            recipe.timerText.text = "";

            for (int b = 0; b < recipes[i].requirements.Length; b++)
            {
                if (b == 0)
                recipe.requirementText.text = $"{recipes[i].requirements[b].data.itemName}  {recipes[i].requirements[b].amountNeeded}";
                else
                    recipe.requirementText.text = $"{recipe.requirementText.text},{recipes[i].requirements[b].data.itemName}  {recipes[i].requirements[b].amountNeeded}";
            }
        }
    }

    public void Try_Craft(RecipeTemplate template)
    {
        if (!HasResources(template.recipe) || isCrafting)
            return;
          TakeResources(template.recipe);

    }

    public void Cancel(RecipeTemplate template)
    {
        if (!isCrafting)
            return;
        for (int i = 0; i < template.recipe.requirements.Length; i++)
        {
            inventory.AddItem(template.recipe.requirements[i].data, template.recipe.requirements[i].amountNeeded);
        }
    }

    public bool HasResources(CraftingRecipeSO recipe)
        
    {
        bool canCraft = true;
        int[] stacksNeeded = null;
        int[] stacksAvailable = null;
        List<int>stacksNeededList = new List<int>();

        for (int i = 0;i < recipe.requirements.Length;i++)
        {
            stacksNeededList.Add(recipe.requirements[i].amountNeeded);
        }
        stacksNeeded = stacksNeededList.ToArray();
        stacksAvailable = new int[stacksNeeded.Length];

        for (int i = 0;i< inventory.inventorySlots.Length;i++)
        {
            if (inventory.inventorySlots[i].data == recipe.requirements[i].data)
            {
                stacksAvailable[i] += inventory.inventorySlots[i].stackSize;
            }
        }
        for (int i = 0;i< stacksAvailable.Length; i++)
        {
            if (stacksAvailable[i] < stacksNeeded[i])
            {
                canCraft = false;
                break;
            }
        }

        return canCraft;


    }

    public void TakeResources(CraftingRecipeSO recipe)
    {
        int[] stacksNeeded = null;

        List<int> stacksNeededList = new List<int>();

        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            stacksNeededList.Add(recipe.requirements[i].amountNeeded);
        }
        stacksNeeded = stacksNeededList.ToArray();

        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            for(int b = 0; b< inventory.inventorySlots.Length;b++) 
            {
                if (inventory.inventorySlots[b].IsEmpty)
                    return;
                if (inventory.inventorySlots[b].data == recipe.requirements[i].data)
                {
                    if (stacksNeeded[i] < recipe.requirements[i].amountNeeded)
                    {
                        if (stacksNeeded[i] - inventory.inventorySlots[b].stackSize < 0)
                        {
                            inventory.inventorySlots[b].stackSize -=stacksNeeded[i];
                            stacksNeeded[i] = 0;
                        }
                        else
                        {
                            stacksNeeded[i] -= inventory.inventorySlots[b].stackSize;
                            inventory.inventorySlots[b].Clean();
                        }

                    }

                    inventory.inventorySlots[b].UpdateSlot();
                }
            }
        }
    }   

}
