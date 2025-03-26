using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace PraktikaIND1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int? EvaluateFormula(string formula)
        {
            Stack<int> operands = new Stack<int>();
            for (int i = formula.Length - 1; i >= 0; i--)
            {
                char current = formula[i];

                if (char.IsDigit(current))
                {
                    operands.Push(current - '0');
                }
                else if (current == ')')
                {
                    continue;
                }
                else if (current == '(')
                {
                    continue;
                }
                else if (current == 'p' || current == 'm')
                {
                    if (operands.Count < 2)
                    {
                        return null;
                    }
                    int a = operands.Pop();
                    int b = operands.Pop();
                    int result = 0;
                    if (current == 'p')
                    {
                        result = (a + b) % 10;
                    }
                    else if (current == 'm')
                    {
                        result = (a - b + 10) % 10;
                    }
                    operands.Push(result);
                }
            }
            if (operands.Count != 1)
            {
                return null;
            }
            return operands.Pop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            string fileName = textBox1.Text+".txt";
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string formula = File.ReadAllText(fileName).Replace(" ", "");
                int? result = EvaluateFormula(formula);
                if (result == null)
                {
                    MessageBox.Show("Некорректная формула.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox1.Text = $"Результат: {result}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}