using System;
using System.Drawing;
using System.Windows.Forms;

namespace kp
{
    public partial class MenuForm : Form
    {
        private Button startButton;
        private Button exitButton;
        private Button infoButton;

        public MenuForm()
        {
            Width = 800;
            Height = 600;
            Text = "Меню игры";
            BackColor = Color.LightBlue; // Задаем цвет фона формы

            startButton = new Button();
            startButton.Text = "Начать игру";
            startButton.Font = new Font("Arial", 14, FontStyle.Bold); // Задаем стиль шрифта
            startButton.BackColor = Color.Green; // Задаем цвет фона кнопки
            startButton.ForeColor = Color.White; // Задаем цвет текста кнопки
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);

            exitButton = new Button();
            exitButton.Text = "Выход";
            exitButton.Font = new Font("Arial", 14, FontStyle.Bold);
            exitButton.BackColor = Color.Red;
            exitButton.ForeColor = Color.White;
            exitButton.Click += ExitButton_Click;
            Controls.Add(exitButton);

            infoButton = new Button();
            infoButton.Text = "Сведения";
            infoButton.Font = new Font("Arial", 14, FontStyle.Bold);
            infoButton.BackColor = Color.Orange;
            infoButton.ForeColor = Color.White;
            infoButton.Click += InfoButton_Click;
            Controls.Add(infoButton);

            UpdateButtonLayout();
            SizeChanged += MenuForm_SizeChanged;
        }

        private void UpdateButtonLayout()
        {
            // Calculate the button sizes and positions based on the form size
            int buttonWidth = Width / 4;
            int buttonHeight = Height / 10;
            int buttonSpacing = Height / 20;
            int buttonLeft = (Width - buttonWidth) / 2;

            startButton.Size = new Size(buttonWidth, buttonHeight);
            startButton.Location = new Point(buttonLeft, Height / 3);

            exitButton.Size = new Size(buttonWidth, buttonHeight);
            exitButton.Location = new Point(buttonLeft, startButton.Bottom + buttonSpacing);

            infoButton.Size = new Size(buttonWidth, buttonHeight);
            infoButton.Location = new Point(buttonLeft, exitButton.Bottom + buttonSpacing);
        }

        private void MenuForm_SizeChanged(object sender, EventArgs e)
        {
            UpdateButtonLayout();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Hide(); // Скрываем форму меню
            GameForm gameForm = new GameForm();
            gameForm.Closed += (s, args) => Close(); // Закрываем приложение, когда игровая форма закрывается
            gameForm.Show(); // Показываем игровую форму
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Очень жаль, что вы так и не опробовали нашу игру");
            Close(); // Закрываем приложение при нажатии на кнопку "Выход"
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Это окно меню игры.\n\nНажмите 'Начать игру', чтобы начать игру.\n\nНажмите 'Выход', чтобы закрыть приложение.", "Сведения");
        }
    }
}