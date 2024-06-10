using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryORM.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
        //M:1
        public int DishTypeId { get; set; }//FK
        public DishType DishTypes { get; set; }//таблицата, с която се осъществява връзката
    }
}
