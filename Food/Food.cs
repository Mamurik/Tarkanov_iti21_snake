using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp
{
    public class Food
    {
        public Point Position { get; set; }
        public FoodType Type { get; }
        public int Size => Type.Size; // Добавленное свойство Size

        public Food(Point position, FoodType type)
        {
            Position = position;
            Type = type;
        }
    }
}