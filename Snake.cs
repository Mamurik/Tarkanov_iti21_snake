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
        private int bodySize; // Добавлено новое поле для размера тела змеи


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
            bodySize = 35; // Установите желаемый размер тела змеи здесь
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

            if (body.Count > bodySize)
                body.RemoveAt(0); // Удалите самый первый элемент, чтобы поддерживать желаемый размер
        }
        public void Draw(Graphics g, bool isPlayer1)
        {
            for (int i = 1; i < body.Count; i++) // Начинаем с индекса 1, чтобы сначала отрисовать тело
            {
                Point point = body[i];

                // Вычисляем координаты для отрисовки квадрата тела змеи
                int bodyX = point.X + (int)(bodySize * 0.2);
                int bodyY = point.Y + (int)(bodySize * 0.2);
                int bodyWidth = (int)(bodySize * 0.6);
                int bodyHeight = (int)(bodySize * 0.6);

                // Рисуем квадрат для тела змеи
                Brush bodyColor = isPlayer1 ? Brushes.Green : Brushes.Orange;
                g.FillEllipse(bodyColor, bodyX, bodyY, bodyWidth, bodyHeight);
            }

            // Отрисовываем голову змеи (последний элемент в списке)
            Point headPoint = body[0];
            Image headTexture;

            if (isPlayer1)
            {
                headTexture = Image.FromFile("head2.png");
            }
            else
            {
                headTexture = Image.FromFile("head.png");
            }

            g.DrawImage(headTexture, headPoint.X, headPoint.Y, bodySize, bodySize);
        }

        public bool CheckCollisionWithFood(Point food)
        {
            Point head = body[0];
            return Math.Abs(head.X - food.X) < bodySize && Math.Abs(head.Y - food.Y) < bodySize; // Используем размер тела для проверки столкновения с едой
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