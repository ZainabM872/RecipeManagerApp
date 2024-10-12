using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Models;
using System.Threading.Tasks;

namespace RecipeManagerApp.Controllers //namespace that groups all the controllers together
{
    public class RecipeController : Controller //defines a controller class named RecipeController that inherits from the base class Controller
    {
        private readonly RecipeContext _context; //represents the database context class

        public RecipeController(RecipeContext context) //used to interct with database
        {
            //ASP.NET creates an instance of RecipeContext called context
            _context = context; //context is assigned to the private field _context
        }

        // displays a list of recipes
        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes.ToListAsync(); //fetches all recipies from the database asychronously using entity framework
            return View(recipes); //passes the info to a view to be rendered
        }

        // display the form to create a new recipie
        public IActionResult Create()
        {
            return View(); //returns the create view
        }

        // handles form submission and adds new recipie to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Cuisine,MealType,Ingredients,Instructions")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe); //add the recipie to the context and saved to the database
                await _context.SaveChangesAsync(); //save changes to the databse
                return RedirectToAction(nameof(Index)); //go to index method 
            }
            return View(recipe);
        }

        // GET: displays the form to edit an existing recipe
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); //if the recpie doesnt exist
            }

            var recipe = await _context.Recipes.FindAsync(id); //asynch fetching usng the id
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe); //return the view that allows for editing
        }

        // POST: handle edited data submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeId,Title,Cuisine,MealType,Ingredients,Instructions")] Recipe recipe)
        {
            if (id != recipe.RecipeId)  //check if the recipie id in the form matches the one being edited
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe); //updated recipe is saved to database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) //if >1 user tries to edit the same recipe
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: display confirmation page for deleting a recipe
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes //get the recipe from the database
                .FirstOrDefaultAsync(m => m.RecipeId == id); //fetches the first element that has a recipie id that matches id
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe); //display the details
        }

        // POST: delete the recipe
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe); //remove the specified recipe
            await _context.SaveChangesAsync(); //save the changes to the database
            return RedirectToAction(nameof(Index)); //redirect back to list of recipies
        }

        //helper method: checks if a recpie exists using its id
        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id); //checks if any records in the Recipies table have a recipieid that matches id
            //Look for any recipe (e) where RecipeId equals the provided id.

        }
    }
}
