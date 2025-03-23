using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class RecipesController : Controller
    {

        public static List<Recipe> _recipes = new List<Recipe>
        {
            new Recipe { ID = 1, Name = "Spaghetti Carbonara", Time = "20 minutes", Difficulty = "Medium", NumberOfLikes = 5, Ingredients = "Pasta, Eggs, Pancetta" , Process = "Boil pasta, cook pancetta, mix with eggs and cheese", TipsAndTricks = "Use fresh eggs" },
            new Recipe { ID = 2, Name = "Classic Cheeseburger", Time = "25 minutes", Difficulty = "Easy", NumberOfLikes = 3, Ingredients = "Ground beef, Cheese, Buns" , Process = "Grill beef, melt cheese on top, assemble burger", TipsAndTricks = "Toast the buns for extra crunch" }
            
        };


        public IActionResult Index()
        {
            return  View(_recipes);
        }

        public IActionResult Details(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.ID = _recipes.Max(r => r.ID) + 1; // Simple ID assignment for our in-memory list
                _recipes.Add(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public IActionResult Edit(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Recipe recipe)
        {
            if (id != recipe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var recipeIndex = _recipes.FindIndex(r => r.ID == id);
                if (recipeIndex != -1)
                {
                    _recipes[recipeIndex] = recipe; // Update the recipe
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(recipe);
        }

        public IActionResult Delete(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.ID == id);
            if (recipe != null)
            {
                _recipes.Remove(recipe);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}