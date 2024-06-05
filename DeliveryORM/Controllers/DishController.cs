using DeliveryORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeliveryORM.Controlls
{
    public class DishController
    {
        private DishesContext _dishesDbContext = new DishesContext();
        public Dish Get(int id)
        {
            Dish findedDish = _dishesDbContext.Dishes.Find(id);
            if (findedDish != null)
            {
                _dishesDbContext.Entry(findedDish).Reference(x => x.DishTypes).Load();
            }
            return findedDish;
        }
        public List<Dish> GetAll()
        {
            return _dishesDbContext.Dishes.Include("DishTypes").ToList();
        }
        public void Create(Dish dish)
        {
            _dishesDbContext.Dishes.Add(dish);
            _dishesDbContext.SaveChanges();
        }
        public void Update(int id, Dish dish)
        {
            Dish findedDish = _dishesDbContext.Dishes.Find(id);
            if (findedDish == null)
            {
                return;
            }
            findedDish.Name = dish.Name;
            findedDish.Description = dish.Description;
            findedDish.Price = dish.Price;
            findedDish.Weight = dish.Weight;
            findedDish.DishTypeId = dish.DishTypeId;
            _dishesDbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            Dish findedDish = _dishesDbContext.Dishes.Find(id);
            _dishesDbContext.Dishes.Remove(findedDish);
            _dishesDbContext.SaveChanges();
        }
    }
}
