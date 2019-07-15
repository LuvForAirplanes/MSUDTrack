﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly TrackerDbContext _context;
        private readonly FoodsService _foodsService;

        public FoodsController(TrackerDbContext context, FoodsService foodsService)
        {
            _context = context;
            _foodsService = foodsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetFood(string query, int page_limit)
        {
            return await _foodsService.Get()
                .Where(f => f.Name 
                    .ToLower() 
                    .Contains(query.ToLower()))
                .Take(page_limit)
                .OrderBy(f => f.LastUsed)
                .OrderBy(f => f.TimesUsed)
                .ToListAsync();
        }

        //We should never update food via the API
        //// POST: api/Foods
        //[HttpPost]
        //public async Task<ActionResult<Food>> PostFood(Food food)
        //{
        //    if (await _foodsService.GetByIdAsync(food.Id) != null)
        //    {
        //        var existingFood = await _foodsService.GetByIdAsync(food.Id);
        //        var newFood = new Food()
        //        {
        //            Id = food.Id,
        //            Created = existingFood.Created,
        //            LeucineMilligrams = food.ProteinGrams * 100,
        //            Name = existingFood.Name,
        //            ProteinGrams = food.ProteinGrams,
        //            Updated = DateTime.Now
        //        };

        //        return await _foodsService.UpdateAsync(newFood, newFood.Id); 
        //    }
        //    else
        //    {
        //        return await _foodsService.CreateAsync(food);
        //    }
        //}

        //private bool FoodExists(string id)
        //{
        //    return _context.Foods.Any(e => e.Id == id);
        //}
    }
}
