﻿using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class RecipeCategoryRepository : IRecipeCategoryRepository
    {

        private readonly DbSet<RecipeCategory> _dbSet;


        public RecipeCategoryRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<RecipeCategory>();
        }

        public List<RecipeCategory> GetAll()
        {
            return _dbSet.ToList(); // SELECT * FROM RecipeCategorys;
        }

        public RecipeCategory GetById(int id) 
        {
            return _dbSet.FirstOrDefault(recipeCategory => recipeCategory.Id == id);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(recipeCategory => recipeCategory.Id == id);
        }

        public RecipeCategory Add (RecipeCategory recipeCategory) 
        {
            
            _dbSet.Add(recipeCategory);

            return recipeCategory;

        }

        public RecipeCategory Update (RecipeCategory recipeCategory) 
        {
            
            _dbSet.Update(recipeCategory);

            return recipeCategory;

        }

        public void Remove (RecipeCategory recipeCategory)
        {
            _dbSet.Remove(recipeCategory);
        }
    }
}