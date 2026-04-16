using CategoryService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryDbContext _context;

        public CategoriesController(CategoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory( Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { category, status = "Category added sucessfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id,Category updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
                return BadRequest("Category ID mismatch.");

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return NotFound($"Category with ID {id} not found.");

            // Update properties
            existingCategory.CategoryName = updatedCategory.CategoryName;
            existingCategory.Description = updatedCategory.Description;

            await _context.SaveChangesAsync();

            return Ok(new { updatedCategory, message = "Category updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { category ,message = "Category deleted successfully" });
        }
    }
}
