using System.Collections.Generic;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class CocktailGlass : ToolClass, ITransfer
    {
        [SerializeField] private List<SRecipe> SRecipes = new List<SRecipe>();
        
        [SerializeField] private List<GameObject> WrongRecipes = new List<GameObject>();

        public void MakeRecipe(List<SElement> ingredients)
        {
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

            Debug.Log(rightRecipe.ResultObject);
            if (rightRecipe.ResultObject == null)
            {
                int index = Random.Range(0, this.WrongRecipes.Count);
                GameObject wrongRecipe = Instantiate(this.WrongRecipes[index]);
                wrongRecipe.transform.position = new Vector3(3.26f, -3.46f, -1f);
                return;
            }
            // Instantiate the result object
            GameObject resultObject = Instantiate(rightRecipe.ResultObject);
            resultObject.transform.position = new Vector3(3.26f, -3.46f, -1f);
        }
        
        public void TransferElements(List<ElementItem> elements)
        {
            MakeRecipe(elements.ConvertAll(x => new SElement(x.Element, x.StateOfMatter)));
        }

        public override void OnMouseUp()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/place_device");
        }
    }
}