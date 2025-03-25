using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PraktikaIND1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private int EvaluateFormula(string formula)
        {
            var tokens = formula.Replace(" ", "").Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Stack<int> stack = new Stack<int>();

            foreach (var token in tokens.Reverse())
            {
                if (int.TryParse(token, out int number))
                {
                    stack.Push(number);
                }
                else
                {
                    throw new InvalidOperationException("Неверный формат формулы");
                }
            }
            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Формула составлена некорректно");
            }

            return stack.Pop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string formula = File.ReadAllText(openFileDialog.FileName);
                        int result = EvaluateFormula(formula);
                        textBox1.Text = $"Результат: {result}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
