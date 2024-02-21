using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp
{

    public class FoodType
    {
        public int Score { get; }
        public Image Image { get; }
        public int Size { get; }

        public FoodType(int score, Image image, int size)
        {
            Score = score;
            Image = image;
            Size = size;
        }
    }

}