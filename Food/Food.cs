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
                Image image = Image.FromFile("head2.png"); // Замените "food2.png" на путь к вашему изображению
                return new Food(position, new FoodType2(40, image, 0.3, 20));
            }
            else
            {
                Image image = Image.FromFile("head.png"); // Замените "food1.png" на путь к вашему изображению
                return new Food(position, new FoodType(10, image, 35));
            }
        }
    }
}