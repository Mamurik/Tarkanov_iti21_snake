using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private int pixelSize; // Добавлено новое поле для размера пикселя

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
            pixelSize = 20; // Установите желаемый размер пикселя здесь

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
                    newHead.Y -= speed; // Умножаем скорость на размер пикселя
                    break;
                case Direction.Down:
                    newHead.Y += speed; // Умножаем скорость на размер пикселя
                    break;
                case Direction.Left:
                    newHead.X -= speed; // Умножаем скорость на размер пикселя
                    break;
                case Direction.Right:
                    newHead.X += speed; // Умножаем скорость на размер пикселя
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
                    g.FillEllipse(new SolidBrush(headColor), point.X, point.Y, pixelSize, pixelSize); // Используем цвет головы змеи и размер пикселя
                else
                    g.FillEllipse(Brushes.Navy, point.X, point.Y, pixelSize, pixelSize); // Цвет остальной части тела змеи и размер пикселя
            }
        }

        public bool CheckCollisionWithFood(Point food)
        {
            Point head = body[0];
            return Math.Abs(head.X - food.X) < pixelSize && Math.Abs(head.Y - food.Y) < pixelSize; // Используем размер пикселя для проверки столкновения с едой
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