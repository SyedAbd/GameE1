using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public RecipeTemplate recipeTemplate;
    public CraftingRecipeSO[] recipes;
    public Transform contentHolder;

    private void Start()
    {
        GenerateRecipes();
    }
    public void GenerateRecipes()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            RecipeTemplate recipe = Instantiate(recipeTemplate.gameObject,contentHolder).GetComponent<RecipeTemplate>();

            recipe.nameText.text = recipes[i].recipeName;

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
        
    }

    public void Cancel(RecipeTemplate template)
    {
        
    }

}
