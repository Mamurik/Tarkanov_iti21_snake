using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kp
{
    public class GameForm : Form
    {
        private Timer timer;
        private Random random;
        private Snake player1;
        private Snake player2;
        private List<Food> food;
        private Label scoreLabel1;
        private Label scoreLabel2;
        private IGameObjectFactory gameObjectFactory;

        public GameForm()
        {


            // Настройка формы
            Width = 800;
            Height = 600;
            DoubleBuffered = true;

            // Инициализация игровых объектов
            player1 = new Snake(new Point(100, 100), Direction.Right, 10, Width, Height, this, Color.Blue); // Голова змеи player1 будет синей
            player2 = new Snake(new Point(700, 500), Direction.Left, 10, Width, Height, this, Color.Green); // Голова змеи player2 будет зеленой
            food = new List<Food>();
            random = new Random();

            // Настройка элементов управления
            scoreLabel1 = new Label();
            scoreLabel1.Text = "Игрок 1: 0";
            scoreLabel1.Location = new Point(10, 10);
            scoreLabel1.AutoSize = true;

            // Инициализация фабрики объектов
            gameObjectFactory = new FoodFactory();

            scoreLabel2 = new Label();
            scoreLabel2.Text = "Игрок 2: 0";
            scoreLabel2.Location = new Point(10, 30);
            scoreLabel2.AutoSize = true;

            Controls.Add(scoreLabel1);
            Controls.Add(scoreLabel2);

            // Настройка таймера
            timer = new Timer();
            timer.Interval = 100; // Обновление игры каждые 100 миллисекунд
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            player1.Move();
            player2.Move();


            // Проверка столкновения змейки player1 с самой собой
            if (player1.CheckCollisionWithSelf())
            {
                GameOver();
                return;
            }

            // Проверка столкновения змейки player2 с самой собой
            if (player2.CheckCollisionWithSelf())
            {
                GameOver();
                return;
            }
            // Проверка столкновения с едой для player1
            for (int i = 0; i < food.Count; i++)
            {
                Food f = food[i];
                if (player1.CheckCollisionWithFood(f.Position))
                {
                    if (f.Type is FoodType)
                    {
                        player1.IncreaseLength();
                        player1.Score += ((FoodType)f.Type).Score;
                        scoreLabel1.Text = "Игрок 1: " + player1.Score;
                    }
                    else if (f.Type is FoodType)
                    {
                        player1.IncreaseLength();
                        player1.Score += ((FoodType)f.Type).Score;
                        scoreLabel1.Text = "Игрок 1: " + player1.Score;
                    }

                    food.RemoveAt(i);
                    i--;
                    break;
                }
            }

            // Проверка столкновения с едой для player2
            for (int i = 0; i < food.Count; i++)
            {
                Food f = food[i];
                if (player2.CheckCollisionWithFood(f.Position))
                {
                    if (f.Type is FoodType)
                    {
                        player2.IncreaseLength();
                        player2.Score += ((FoodType)f.Type).Score;
                        scoreLabel2.Text = "Игрок 2: " + player2.Score;
                    }
                    else if (f.Type is FoodType)
                    {
                        player2.IncreaseLength();
                        player2.Score += ((FoodType)f.Type).Score;
                        scoreLabel2.Text = "Игрок 2: " + player2.Score;
                    }

                    food.RemoveAt(i);
                    i--;
                    break;
                }
            }

            // Проверка столкновения игроков друг с другом
            if (player1.CheckCollisionWithPlayer(player2) || player2.CheckCollisionWithPlayer(player1))
            {
                GameOver();
                return;
            }

            // Генерация пищи, если массив food пустой
            if (food.Count == 0)
            {
                GenerateFood();
            }


            Refresh(); // Перерисовка формы
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            player1.Draw(g, true); // Передаем true для isPlayer1
            player2.Draw(g, false); // Передаем false для isPlayer1

            foreach (Food f in food)
            {
                g.DrawImage(f.Type.Image, f.Position.X, f.Position.Y, f.Type.Size, f.Type.Size);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Управление player1
            if (e.KeyCode == Keys.W && player1.Direction != Direction.Down)
                player1.Direction = Direction.Up;
            else if (e.KeyCode == Keys.S && player1.Direction != Direction.Up)
                player1.Direction = Direction.Down;
            else if (e.KeyCode == Keys.A && player1.Direction != Direction.Right)
                player1.Direction = Direction.Left;
            else if (e.KeyCode == Keys.D && player1.Direction != Direction.Left)
                player1.Direction = Direction.Right;

            // Управление player2
            if (e.KeyCode == Keys.Up && player2.Direction != Direction.Down)
                player2.Direction = Direction.Up;
            else if (e.KeyCode == Keys.Down && player2.Direction != Direction.Up)
                player2.Direction = Direction.Down;
            else if (e.KeyCode == Keys.Left && player2.Direction != Direction.Right)
                player2.Direction = Direction.Left;
            else if (e.KeyCode == Keys.Right && player2.Direction != Direction.Left)
                player2.Direction = Direction.Right;
        }
        public void GenerateFood()
        {
            int totalFoodCount = food.Count;

            for (int i = totalFoodCount; i < 20; i++)
            {
                int x = random.Next(0, Width - 10);
                int y = random.Next(0, Height - 10);

                // Использование фабричного метода для создания объектов
                Food newFood = gameObjectFactory.CreateGameObject(new Point(x, y));
                food.Add(newFood);
            }

            // Создание 5 единиц еды третьего типа
            for (int i = 0; i < 5; i++)
            {
                int x = random.Next(0, Width - 20);
                int y = random.Next(0, Height - 20);

                Image image = Image.FromFile("head.png"); // Замените "food3.png" на путь к вашему изображению


            }
        }
        public void GameOver()
        {
            timer.Stop();

            if (player1.Score > player2.Score)
                MessageBox.Show("Игрок 1 победил! Очки: Игрок 1 - " + player1.Score + ", Игрок 2 - " + player2.Score, "Игра Окончена");
            else if (player2.Score > player1.Score)
                MessageBox.Show("Игрок 2 победил! Очки: Игрок 1 - " + player1.Score + ", Игрок 2 - " + player2.Score, "Игра Окончена");
            else
                MessageBox.Show("Ничья! Очки: Игрок 1 - " + player1.Score + ", Игрок 2 - " + player2.Score, "Игра Окончена");

            Close();
            // Логика для окончания игры
        }
    }
}