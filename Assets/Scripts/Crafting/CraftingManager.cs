using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private InventoryManager inventory;
    public RecipeTemplate recipeTemplate;
    public CraftingRecipeSO[] recipes;
    public Transform contentHolder;



    private void Start()
    {
        inventory = GetComponentInParent<InventoryManager>();
        GenerateRecipes();
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
        if (!HasResource() || isCrafting)
            return;
    }

    public void Cancel(RecipeTemplate template)
    {
        
    }

    public bool HasRecourses(CraftingRecipeSO recipe)
        
    {
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





    }

}
