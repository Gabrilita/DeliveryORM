﻿using DeliveryORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryORM.Controlls
{
    public class DishController
    {
        private DishesContext _dishesContext = new DishesContext();
        public Dish Get(int id)
        {
            using (_dishesContext=new DishesContext()) 
            {
                Dish findedDish = _dishesContext.Dishes.Find(id);
                if (findedDish != null)
                {
                    _dishesContext.Entry(findedDish).Reference(x => x.DishTypes).Load();
                }
                return findedDish;
            }
        }
        public List<Dish> GetAll()
        {
            using (_dishesContext = new DishesContext()) 
            {
                return _dishesContext.Dishes.Include("DishTypes").ToList();
            }
        }
        public void Create(Dish dish)
        {
            using (_dishesContext = new DishesContext())
            {
                _dishesContext.Dishes.Add(dish);
                _dishesContext.SaveChanges();
            }
        }
        public void Update(int id, Dish dish)
        {
            using (_dishesContext = new DishesContext())
            {
                Dish findedDish = _dishesContext.Dishes.Find(id);
                if (findedDish == null)
                {
                    return;
                }
                findedDish.Name = dish.Name;
                findedDish.Description = dish.Description;
                findedDish.Price = dish.Price;
                findedDish.Weight = dish.Weight;
                findedDish.DishTypeId = dish.DishTypeId;
                _dishesContext.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (_dishesContext = new DishesContext())
            {
                Dish findedDish = _dishesContext.Dishes.Find(id);
                _dishesContext.Dishes.Remove(findedDish);
                _dishesContext.SaveChanges();
            }
        }
    }
}
