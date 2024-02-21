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

        public FoodType2(int score, Color color, double spawnRate, int size)
            : base(score, color, size)
        {
            SpawnRate = spawnRate;
        }
    }
}