using DeliveryORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryORM.Controlls
{
    public class DishTypeController
    {
        private DishesContext _dishesContext = new DishesContext();
        public List<DishType> GetAllDishTypes()
        {
            return _dishesContext.DishTypes.ToList();
        }
        public string GetDishTypeById(int id)
        {
            return _dishesContext.DishTypes.Find(id).Name;
        }
    }
}
