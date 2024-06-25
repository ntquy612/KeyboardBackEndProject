﻿using Catalog.API.Data;
using Catalog.API.DTOs;
using Catalog.API.Models;
using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("/api/v1/category")]
    public class CategoryController : ControllerBase
    {
        private readonly CatalogContext _context;

        public CategoryController(CatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Category>> GetById(Guid Id)
        {
            var category = await _context.Categories.FindAsync(Id);

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            var category = new Category(createCategoryDTO.CategoryName);

            category.CategoryId = Guid.NewGuid();

            await _context.Categories.AddAsync(category);

            //var newCategory = _mapper.Map<Category>(category);

            await _context.SaveChangesAsync();

            return Ok();
                
        }
    }
}
