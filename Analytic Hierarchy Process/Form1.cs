using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytic_Hierarchy_Process
{
    public partial class Form1 : Form
    {
        List<TextBox> criteriaTextBoxes = new List<TextBox>();
        List<Label> criteriaLabels = new List<Label>();
        List<List<TextBox>> factorsTextBoxes = new List<List<TextBox>>();
        List<TextBox> candidatesTextBoxes = new List<TextBox>();
        List<List<Label>> candidatesLabels = new List<List<Label>>();
        List<List<List<TextBox>>> experiencesTextBoxes = new List<List<List<TextBox>>>();
        public Form1()
        {
            InitializeComponent();
        }
        // Фунція для додавання критерію, та нової таблиці для альтернатив.
        // У поля, що додаються функцією, користувач вписує піб назву критерію
        // та дані задачі
        private void AddCriteria()
        {
            int criteria = criteriaTextBoxes.Where(x => x.Visible).Count();
            int candidates = candidatesTextBoxes.Where(x => x.Visible).Count();
            if (criteria >= 5)
            {
                return;
            }

            criteriaTextBoxes[criteria].Visible = true;
            criteriaLabels[criteria].Visible = true;
            for (int i = 0; i < criteria + 1; i++)
            {
                factorsTextBoxes[criteria][i].Visible = true;
                factorsTextBoxes[i][criteria].Visible = true;
            }

            for (int j = 0; j < candidates; j++)
            {
                if (criteria != 0)
                {
                    candidatesLabels[criteria - 1][j].Visible = true;
                }
                for (int k = 0; k < candidates; k++)
                {
                    experiencesTextBoxes[criteria][j][k].Visible = true;
                }
            }
        }
        // Фунція для видалення критерію, та однієї таблиці для альтернатив.

        private void RemoveCriteria()
        {
            int criteria = criteriaTextBoxes.Where(x => x.Visible).Count();
            int candidates = candidatesTextBoxes.Where(x => x.Visible).Count();
            if (criteria <= 2)
            {
                return;
            }

            criteriaTextBoxes[criteria - 1].Visible = false;
            criteriaTextBoxes[criteria - 1].Text = $"Критерій {criteria}";
            criteriaLabels[criteria - 1].Visible = false;
            for (int i = 0; i < criteria; i++)
            {
                factorsTextBoxes[criteria - 1][i].Visible = false;
                if (factorsTextBoxes[criteria - 1][i].Enabled)
                {
                    factorsTextBoxes[criteria - 1][i].Text = string.Empty;
                    factorsTextBoxes[criteria - 1][i].BackColor = Color.White;
                }

                factorsTextBoxes[i][criteria - 1].Visible = false;
                if (factorsTextBoxes[i][criteria - 1].Enabled)
                {
                    factorsTextBoxes[i][criteria - 1].Text = string.Empty;
                    factorsTextBoxes[i][criteria - 1].BackColor = Color.White;
                }
            }

            for (int j = 0; j < candidates; j++)
            {
                if (criteria != 0)
                {
                    candidatesLabels[criteria - 2][j].Visible = false;
                }
                for (int k = 0; k < candidates; k++)
                {
                    experiencesTextBoxes[criteria - 1][j][k].Visible = false;
                    if (experiencesTextBoxes[criteria - 1][j][k].Enabled)
                    {
                        experiencesTextBoxes[criteria - 1][j][k].Text = string.Empty;
                        experiencesTextBoxes[criteria - 1][j][k].BackColor = Color.White;
                    }
                }

            }

        }
        // Фунція для додавання кандидата до програмного інтерфейсу.
        // У поля, що додаються функцією, користувач вписує дані задачі,
        // та ПІБ виконавця.
        private void AddCandidate()
        {
            int candidates = candidatesTextBoxes.Where(x => x.Visible).Count();
            int criteria = criteriaTextBoxes.Where(x => x.Visible).Count();
            if (candidates >= 5)
            {
                return;
            }

            candidatesTextBoxes[candidates].Visible = true;
            for (int i = 0; i < criteria; i++)
            {
                if (i != 0)
                {
                    candidatesLabels[i - 1][candidates].Visible = true;
                }
                for (int j = 0; j < candidates + 1; j++)
                {
                    experiencesTextBoxes[i][j][candidates].Visible = true;
                    experiencesTextBoxes[i][candidates][j].Visible = true;
                }
            }
        }
        // Функція для видалення кандидата з 
        // програмного інтерфейсу
        private void RemoveCandidate()
        {
            int candidates = candidatesTextBoxes.Where(x => x.Visible).Count();
            int criteria = criteriaTextBoxes.Where(x => x.Visible).Count();
            if (candidates <= 2)
            {
                return;
            }

            candidatesTextBoxes[candidates - 1].Visible = false;
            candidatesTextBoxes[candidates - 1].Text = $"Кандидат {candidates}";
            for (int i = 0; i < criteria; i++)
            {
                if (i != 0)
                {
                    candidatesLabels[i - 1][candidates - 1].Visible = false;
                }
                for (int j = 0; j < candidates; j++)
                {
                    experiencesTextBoxes[i][j][candidates - 1].Visible = false;
                    if (experiencesTextBoxes[i][j][candidates - 1].Enabled)
                    {
                        experiencesTextBoxes[i][j][candidates - 1].Text = string.Empty;
                        experiencesTextBoxes[i][j][candidates - 1].BackColor = Color.White;
                    }

                    experiencesTextBoxes[i][candidates - 1][j].Visible = false;
                    if (experiencesTextBoxes[i][candidates - 1][j].Enabled)
                    {
                        experiencesTextBoxes[i][candidates - 1][j].Text = string.Empty;
                        experiencesTextBoxes[i][candidates - 1][j].BackColor = Color.White;
                    }
                }
            }

        }
        // Функція що додає функціональні кнопки, використовуючі які,
        // користувач може додавати кандидатів, критерії.
        private Button CreateGridControls(int top, int left, string text, EventHandler AddButton_Click, EventHandler RemoveButton_Click)
        {
            Label label = new Label() { Top = top, Left = left, Text = text };
            label.AutoSize = true;
            Controls.Add(label);

            Button addButton = new Button() { Top = label.Top + label.Height + 5, Left = label.Left, Text = "+", Width = 25 };
            addButton.Click += AddButton_Click;
            Controls.Add(addButton);

            Button removeButton = new Button() { Top = addButton.Top, Left = addButton.Left + addButton.Width + 5, Text = "-", Width = addButton.Width };
            removeButton.Click += RemoveButton_Click;
            Controls.Add(removeButton);

            return removeButton;
        }
        // Функція, яка оновлює текст у рядках
        // після того, як користувач вписав нову
        // назву для критерію
        private void UpdateCriterionLabels(TextBox sender)
        {
            int index = criteriaTextBoxes.IndexOf(sender);
            criteriaLabels[index].Text = sender.Text;
        }
        // Функція, яка оновлює текст у рядках
        // після того, як користувач вписав нову
        // назву для кандидата
        private void UpdateLabels(TextBox sender)
        {
            int index = candidatesTextBoxes.IndexOf(sender);
            for (int i = 0; i < candidatesLabels.Count; i++)
            {
                candidatesLabels[i][index].Text = sender.Text;
            }
        }
        // Функція, що перевіряє правильність, введених у поля, даних.
        // Рядки для чисел приймають лише цифри(0-9), кому, крапку, знак дробу.
        // Рядки не можуть бути порожніми.
        // Якщо поле не відповідає критеріям воно виділяється червоним
        // кольором, та подальша робота програми не відбувається
        private bool ValidateFields()
        {
            bool valid = true;

            int candidates = candidatesTextBoxes.Where(x => x.Visible).Count();
            int criteria = criteriaTextBoxes.Where(x => x.Visible).Count();

            for (int i = 0; i < criteria; i++)
            {
                if (criteriaTextBoxes[i].Text == "")
                {
                    criteriaTextBoxes[i].BackColor = Color.Red;
                    valid = false;
                }
                else
                {
                    criteriaTextBoxes[i].BackColor = Color.White;
                }
            }

            for (int i = 0; i < criteria; i++)
            {
                for (int j = i + 1; j < criteria; j++)
                {
                    if (factorsTextBoxes[i][j].Text == "")
                    {
                        factorsTextBoxes[i][j].BackColor = Color.Red;
                        valid = false;
                    }
                    else
                    {
                        factorsTextBoxes[i][j].BackColor = Color.White;
                    }
                }

            }

            for (int i = 0; i < candidates; i++)
            {
                if (candidatesTextBoxes[i].Text == "")
                {
                    candidatesTextBoxes[i].BackColor = Color.Red;
                    valid = false;
                }
                else
                {
                    candidatesTextBoxes[i].BackColor = Color.White;
                }
            }

            for (int i = 0; i < criteria; i++)
            {
                for (int j = 0; j < candidates; j++)
                {
                    for (int k = j + 1; k < candidates; k++)
                    {
                        if (experiencesTextBoxes[i][j][k].Text == "")
                        {
                            experiencesTextBoxes[i][j][k].BackColor = Color.Red;
                            valid = false;
                        }
                        else
                        {
                            experiencesTextBoxes[i][j][k].BackColor = Color.White;
                        }

                    }
                }
            }

            return valid;
        }
        // Функція, що за наданими користувачем даними, вирішує задачу
        // методом аналізу ієрархії.
        private void HierarchyAnanlysis()
        {
            int criteriaCount = criteriaTextBoxes.Where(x => x.Visible).Count();
            int candidatesCount = candidatesTextBoxes.Where(x => x.Visible).Count();
            // Переведення даних, введених користувачем,
            // з тексту у числа
            List<List<float>> factors = new List<List<float>>();
            for (int i = 0; i < criteriaCount; i++)
            {
                factors.Add(new List<float>());
                for (int j = 0; j < criteriaCount; j++)
                {
                    factors[i].Add(-1.0f);
                }
            }

            for (int i = 0; i < criteriaCount; i++)
            {
                for (int j = i; j < criteriaCount; j++)
                {
                    if (i == j)
                    {
                        factors[i][j] = 1.0f;
                    }
                    else
                    {
                        string temp = factorsTextBoxes[i][j].Text;
                        if (temp.Contains('/'))
                        {
                            var splitted = temp.Split('/');
                            int a = int.Parse(splitted[0]);
                            int b = int.Parse(splitted[1]);
                            factors[i][j] = (float)a / b;
                            factors[j][i] = 1 / factors[i][j];
                        }
                        else
                        {
                            temp = temp.Replace('.', ',');
                            factors[i][j] = float.Parse(temp);
                            factors[j][i] = 1 / factors[i][j];
                        }
                    }
                }
            }

            List<List<List<float>>> experiences = new List<List<List<float>>>();
            for (int i = 0; i < criteriaCount; i++)
            {
                experiences.Add(new List<List<float>>());
                for (int j = 0; j < candidatesCount; j++)
                {
                    experiences[i].Add(new List<float>());
                    for (int k = 0; k < candidatesCount; k++)
                    {
                        experiences[i][j].Add(-1.0f);
                    }
                }
            }

            for (int i = 0; i < criteriaCount; i++)
            {
                for (int j = 0; j < candidatesCount; j++)
                {
                    for (int k = j; k < candidatesCount; k++)
                    {
                        if (k == j)
                        {
                            experiences[i][j][k] = 1.0f;
                        }
                        else
                        {
                            string temp = experiencesTextBoxes[i][j][k].Text;
                            if (temp.Contains('/'))
                            {
                                var splitted = temp.Split('/');
                                int a = int.Parse(splitted[0]);
                                int b = int.Parse(splitted[1]);
                                experiences[i][j][k] = (float)a / b;
                                experiences[i][k][j] = 1 / experiences[i][j][k];
                            }
                            else
                            {
                                temp = temp.Replace('.', ',');
                                experiences[i][j][k] = float.Parse(temp);
                                experiences[i][k][j] = 1 / experiences[i][j][k];
                            }
                        }

                    }
                }
            }

            List<List<float>> factorsNormalized = new List<List<float>>();
            for (int i = 0; i < factors.Count; i++)
            {
                factorsNormalized.Add(new List<float>());
                for (int j = 0; j < factors[0].Count; j++)
                {
                    factorsNormalized[i].Add(-1.0f);
                }
            }
            // Нормалізація матриці попарних порівнянь вагових
            // коефіцієнтів критеріїв.
            for (int j = 0; j < factors[0].Count; j++)
            {
                float sum = 0.0f;
                for (int i = 0; i < factors.Count; i++)
                {
                    sum += factors[i][j];
                }

                for (int i = 0; i < factors[0].Count; i++)
                {
                    factorsNormalized[i][j] = factors[i][j] / sum;
                }
            }
            // Визначення відносних усереднених вагових
            // коефіцієнтів часткових критеріїв.
            List<float> wChast = new List<float>();
            for (int i = 0; i < factorsNormalized.Count; i++)
            {
                float sum = factorsNormalized[i].Sum();
                int count = factorsNormalized.Count;
                wChast.Add(sum / count);
            }

            List<List<List<float>>> experiencesNormalized = new List<List<List<float>>>();
            for (int i = 0; i < criteriaCount; i++)
            {
                experiencesNormalized.Add(new List<List<float>>());
                for (int j = 0; j < candidatesCount; j++)
                {
                    experiencesNormalized[i].Add(new List<float>());
                    for (int k = 0; k < candidatesCount; k++)
                    {
                        experiencesNormalized[i][j].Add(-1.0f);
                    }
                }
            }
            // Нормалізація матриць парних порівнянь вагових
            // коефіцієнтів альтернативних рішень.
            for (int i = 0; i < criteriaCount; i++)
            {
                for (int j = 0; j < experiences[0][0].Count; j++)
                {
                    float sum = 0.0f;
                    for (int k = 0; k < experiences[0].Count; k++)
                    {
                        sum += experiences[i][k][j];
                    }

                    for (int k = 0; k < experiences[0].Count; k++)
                    {
                        experiencesNormalized[i][k][j] = experiences[i][k][j] / sum;
                    }
                }
            }

            List<List<float>> wAlts = new List<List<float>>();
            for (int i = 0; i < criteriaCount; i++)
            {
                wAlts.Add(new List<float>());
                for (int j = 0; j < candidatesCount; j++)
                {
                    wAlts[i].Add(-1.0f);
                }
            }
            // Визначення відносних вагових коефіцієнтів
            // альтернатив сприйманих рішень.
            for (int i = 0; i < criteriaCount; i++)
            {
                for (int j = 0; j < candidatesCount; j++)
                {
                    float sum = experiencesNormalized[i][j].Sum();
                    int count = experiences[i][j].Count;
                    wAlts[i][j] = sum / count;
                }
            }
            // Визначення комбінованих вагових коефіцієнтів
            // альтернатив рішень, що приймаються.
            List<float> wCombi = new List<float>();
            for (int i = 0; i < wAlts[0].Count; i++)
            {
                float sum = 0.0f;
                for (int j = 0; j < wAlts.Count; j++)
                {
                    sum += wChast[j] * wAlts[j][i];
                }
                wCombi.Add(sum);
            }
            // З поміж отриманих результатів обрати найкращий
            int maxIndex = -1;
            float max = float.MinValue;
            for (int i = 0; i < wCombi.Count; i++)
            {
                if (wCombi[i] > max)
                {
                    max = wCombi[i];
                    maxIndex = i;
                }
            }
            // Вивести користувачу сповіщення про 
            // знайдений результат
            string caption = "Рішення знайдено";
            string messasge = $"Кандидата {candidatesTextBoxes[maxIndex].Text} слід прийняти на роботу.";
            MessageBox.Show(messasge, caption);

        }
        // Запуск програмного додатку, та формування графічного інтерфейсу додатку
        // заповнення додатку початковими даними, рядками, стовпцями, кнопками.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Створення полів для введення даних
            // по критеріям
            for (int i = 0; i < 5; i++)
            {
                TextBox criterionTextBox = new TextBox() { Left = 10, Top = 40 + 30 * i, Width = 70, Text = $"Критерій {i + 1}", Visible = false };
                criterionTextBox.Leave += CriterionTextBox_Leave;
                criteriaTextBoxes.Add(criterionTextBox);
                Controls.Add(criterionTextBox);

                factorsTextBoxes.Add(new List<TextBox>());
                for (int j = 0; j < 5; j++)
                {
                    TextBox factorTextBox = new TextBox() { Left = 90 + 50 * j, Top = 40 + 30 * i, Width = 40, Visible = false };
                    if (j <= i)
                    {
                        factorTextBox.Enabled = false;
                    }
                    factorsTextBoxes[i].Add(factorTextBox);
                    Controls.Add(factorTextBox);
                }
            }
            // Створення полів для введення даних 
            // по кандидатам
            for (int matrix = 0; matrix < 2; matrix++)
            {
                Label criterionLabel = new Label() { Left = 10 + 370 * (matrix + 1) + 160 - 35, Top = 10, Width = 70, MaximumSize = new Size(70, 20), Text = criteriaTextBoxes[matrix].Text, Visible = false };
                criteriaLabels.Add(criterionLabel);
                Controls.Add(criterionLabel);

                if (matrix != 0)
                {
                    candidatesLabels.Add(new List<Label>());
                }
                experiencesTextBoxes.Add(new List<List<TextBox>>());
                for (int i = 0; i < 5; i++)
                {
                    if (matrix == 0)
                    {
                        TextBox candidateTextBox = new TextBox() { Left = 10 + 370 * (matrix + 1), Top = 40 + 30 * i, Width = 70, Text = $"Кандидат {i + 1}", Visible = false };
                        candidateTextBox.Leave += CandidateTextBox_Leave;
                        candidatesTextBoxes.Add(candidateTextBox);
                        Controls.Add(candidateTextBox);
                    }
                    else
                    {
                        Label candidateLabel = new Label() { Left = 10 + 370 * (matrix + 1), Top = 45 + 30 * i, Width = 70, MaximumSize = new Size(70, 20), Text = candidatesTextBoxes[i].Text, Visible = false };
                        candidatesLabels[matrix - 1].Add(candidateLabel);
                        Controls.Add(candidateLabel);
                    }

                    experiencesTextBoxes[matrix].Add(new List<TextBox>());
                    for (int j = 0; j < 5; j++)
                    {
                        TextBox experienceTextBox = new TextBox() { Left = 90 + 370 * (matrix + 1) + 50 * j, Top = 40 + 30 * i, Width = 40, Visible = false };
                        if (j <= i)
                        {
                            experienceTextBox.Enabled = false;
                        }
                        experiencesTextBoxes[matrix][i].Add(experienceTextBox);
                        Controls.Add(experienceTextBox);
                    }
                }
            }

            for (int matrix = 2; matrix < 5; matrix++)
            {
                Label criterionLabel = new Label() { Left = 10 + 370 * (matrix - 3 + 1) + 160 - 35, Top = 195, Width = 70, MaximumSize = new Size(70, 20), Text = criteriaTextBoxes[matrix].Text, Visible = false };
                criteriaLabels.Add(criterionLabel);
                Controls.Add(criterionLabel);

                candidatesLabels.Add(new List<Label>());
                experiencesTextBoxes.Add(new List<List<TextBox>>());
                for (int i = 0; i < 5; i++)
                {
                    Label candidateTextBox = new Label() { Left = 10 + 370 * (matrix - 3 + 1), Top = 225 + 30 * i, Width = 70, MaximumSize = new Size(70, 20), Text = candidatesTextBoxes[i].Text, Visible = false };
                    candidatesLabels[matrix - 1].Add(candidateTextBox);
                    Controls.Add(candidateTextBox);

                    experiencesTextBoxes[matrix].Add(new List<TextBox>());
                    for (int j = 0; j < 5; j++)
                    {
                        TextBox experienceTextBox = new TextBox() { Left = 90 + 370 * (matrix - 3 + 1) + 50 * j, Top = 220 + 30 * i, Width = 40, Visible = false };
                        if (j <= i)
                        {
                            experienceTextBox.Enabled = false;
                        }
                        experiencesTextBoxes[matrix][i].Add(experienceTextBox);
                        Controls.Add(experienceTextBox);
                    }
                }
            }
            // Створення функціональних кнопок
            var lastButton = CreateGridControls(400, 10, "Критерії", AddCriteriaButton_Click, RemoveCriteriaButton_Click);
            lastButton = CreateGridControls(lastButton.Top + lastButton.Height + 5, 10, "Кандидати", AddCandidateButton_Click, RemoveCandidateButton_Click);

            Button submitButton = new Button() { Top = 440, Text = "Розрахувати", AutoSize = true };
            submitButton.Left = this.Width / 2 - submitButton.Width / 2;
            submitButton.Click += SubmitButton_Click;
            Controls.Add(submitButton);

            for (int i = 0; i < 2; i++)
            {
                AddCandidate();
                AddCriteria();
            }
        }
        // Додавання критерію при натисканні кнопки додавання критерію
        private void AddCriteriaButton_Click(object sender, EventArgs e)
        {
            AddCriteria();
        }
        // Видалення критерію при натисканні кнопки видалення критерію
        private void RemoveCriteriaButton_Click(object sender, EventArgs e)
        {
            RemoveCriteria();
        }
        // Додавання кандидата при натисканні кнопки додавання кандидата
        private void AddCandidateButton_Click(object sender, EventArgs e)
        {
            AddCandidate();
        }
        // Видалення кандидата при натисканні кнопки видалення кандидата
        private void RemoveCandidateButton_Click(object sender, EventArgs e)
        {
            RemoveCandidate();
        }
        // Оновлення тексту у рядках після того,
        // як користувач вписав нову назву для критерію
        private void CriterionTextBox_Leave(object sender, EventArgs e)
        {
            UpdateCriterionLabels(sender as TextBox);
        }
        // Оновлення тексту у рядках після того,
        // як користувач вписав нову назву для критерію
        private void CandidateTextBox_Leave(object sender, EventArgs e)
        {
            UpdateLabels(sender as TextBox);
        }
        // Перевірка введених у поле користувачем даних
        private void EfficiencyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ',') && (e.KeyChar != '/'))
            {
                e.Handled = true;
            }
        }
        private void EfficiencyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, "[^0-9.,/]"))
            {
                (sender as TextBox).Text = "";
            }
        }
        // Перевірка введених користувачем даних
        // якщо дані корректні, виконання розрахунку
        // за методу аналізу ієрархії
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            HierarchyAnanlysis();
        }
    }
}
