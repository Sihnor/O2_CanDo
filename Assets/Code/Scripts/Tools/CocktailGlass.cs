using System.Collections.Generic;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class CocktailGlass : ToolClass, ITransfer
    {
        [SerializeField] private List<SRecipe> SRecipes = new List<SRecipe>();

        public void MakeRecipe(List<SElement> ingredients)
        {
            Debug.Log("Making Recipe");
            SRecipe rightRecipe = new SRecipe(ingredients, null);
            foreach (SRecipe recipe in this.SRecipes)
            {
                // If the recipe has a different number of ingredients, skip it
                if(recipe.Elements.Count != ingredients.Count) continue;

                // if the recipe has the same ingredients with the same state of matter, then it is a recipe
                foreach (SElement ingredient in ingredients)
                {
                    // if the recipe does not contain the ingredient, then it is not a recipe
                    if (!recipe.Elements.Contains(ingredient)) break;
                    
                    // find the element in the recipe
                    SElement recipeElement = recipe.Elements.Find(x => x.Element == ingredient.Element);
                    if (recipeElement.StateOfMatter != ingredient.StateOfMatter) break;
                    
                    rightRecipe = recipe;
                }
                if (rightRecipe.ResultObject != null) break;
            }

            if (rightRecipe.ResultObject == null) return;
            
            // Instantiate the result object
            GameObject resultObject = Instantiate(rightRecipe.ResultObject);
            resultObject.transform.position = new Vector3(0, 0, 0);
        }
        
        public void TransferElements(List<ElementItem> elements)
        {
            MakeRecipe(elements.ConvertAll(x => new SElement(x.Element, x.StateOfMatter)));
        }

        public override void OnMouseUp()
        {
        }
    }
}