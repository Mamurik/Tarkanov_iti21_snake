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
    public interface IGameObjectFactory
    {
        Food CreateGameObject(Point position);
    }
    public class FoodFactory : IGameObjectFactory
    {
        private Random random;

        public FoodFactory()
        {
            random = new Random();
        }

        public Food CreateGameObject(Point position)
        {
            double spawnRate = random.NextDouble();

            if (spawnRate <= 0.3)
            {
                return new Food(position, new FoodType2(40, Color.Blue, 0.3, 10)); // Пример размера 10
            }
            else
            {
                return new Food(position, new FoodType(10, Color.Red, 15)); // Пример размера 15
            }
        }
    }
}