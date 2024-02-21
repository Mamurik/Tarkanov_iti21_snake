using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp
{
    public class FoodType2 : FoodType
    {
        public double SpawnRate { get; }

        public FoodType2(int score, Image image, double spawnRate, int size)
            : base(score, image, size)
        {
            SpawnRate = spawnRate;
        }
    }

}