using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kp
{
    public class Snake
    {
        private List<Point> body;
        private Direction direction;
        private int speed;
        private int screenWidth;
        private int screenHeight;
        private GameForm gameForm;
        private int score;
        private Color headColor;

   
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Snake(Point startingPosition, Direction startingDirection, int startingSpeed, int width, int height, GameForm form, Color headColor)
        {
            body = new List<Point>();
            body.Add(startingPosition);
            direction = startingDirection;
            speed = startingSpeed;
            screenWidth = width;
            screenHeight = height;
            gameForm = form;
            this.headColor = headColor;

        }

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public void Move()
        {
            Point head = body[0];
            Point newHead = new Point(head.X, head.Y);

            switch (direction)
            {
                case Direction.Up:
                    newHead.Y -= speed;
                    break;
                case Direction.Down:
                    newHead.Y += speed;
                    break;
                case Direction.Left:
                    newHead.X -= speed;
                    break;
                case Direction.Right:
                    newHead.X += speed;
                    break;
            }

            // Проверка на столкновение с границами экрана
            if (newHead.X < 0 || newHead.X >= screenWidth || newHead.Y < 0 || newHead.Y >= screenHeight)
            {
                gameForm.GameOver();
                return;
            }

            body.Insert(0, newHead);
            body.RemoveAt(body.Count - 1);
        }

        public bool CheckCollisionWithSelf()
        {
            Point head = body[0];
            for (int i = 1; i < body.Count; i++)
            {
                if (head == body[i])
                    return true;
            }
            return false;
        }
        public void IncreaseLength()
        {
            Point tail = body[body.Count - 1];
            body.Add(tail);
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < body.Count; i++)
            {
                Point point = body[i];

                if (i == 0) // Проверяем, является ли текущая точка головой
                    g.FillRectangle(new SolidBrush(headColor), point.X, point.Y, 10, 10); // Используем цвет головы змеи
                else
                    g.FillRectangle(Brushes.Black, point.X, point.Y, 10, 10); // Цвет остальной части тела змеи
            }
        }

        public bool CheckCollisionWithFood(Point food)
        {
            Point head = body[0];
            return Math.Abs(head.X - food.X) < 10 && Math.Abs(head.Y - food.Y) < 10;
        }

        public bool CheckCollisionWithPlayer(Snake otherPlayer)
        {
            for (int i = 1; i < body.Count; i++)
            {
                if (body[i] == otherPlayer.body[0])
                    return true;
            }
            return false;
        }
    }
}