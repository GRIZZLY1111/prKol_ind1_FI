using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktikaIND1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Пример инфиксного выражения
            Console.WriteLine("Введите инфиксное выражение");
            string infixExpression = Convert.ToString(Console.ReadLine());

            // Преобразование в префиксную форму
            string prefixExpression = InfixToPrefix(infixExpression);

            // Вывод результата
            Console.WriteLine($"Инфиксное выражение: {infixExpression}");
            Console.WriteLine($"Префиксное выражение: {prefixExpression}");
            Console.ReadKey();
        }

        static string InfixToPrefix(string infix)
        {
            // Удаляем пробелы и переворачиваем строку
            var reversedInfix = new string(infix.Replace(" ", "").Reverse().ToArray());

            // Заменяем скобки местами
            reversedInfix = new string(reversedInfix.Select(ch =>
                ch == '(' ? ')' :
                ch == ')' ? '(' :
                ch).ToArray());

            // Преобразуем перевернутую строку в постфиксную форму
            var postfix = InfixToPostfix(reversedInfix);

            // Переворачиваем результат обратно для получения префиксной формы
            return new string(postfix.Reverse().ToArray());
        }

        static string InfixToPostfix(string infix)
        {
            // Очередь для выходного выражения
            var outputQueue = new Queue<char>();

            // Стек для операторов
            var operatorStack = new Stack<char>();

            // Приоритет операторов
            var precedence = new Dictionary<char, int>
        {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 }
        };

            // Обрабатываем каждый символ
            foreach (var ch in infix)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    // Если символ — операнд, добавляем его в очередь
                    outputQueue.Enqueue(ch);
                }
                else if (ch == '(')
                {
                    // Если символ — открывающая скобка, помещаем её в стек
                    operatorStack.Push(ch);
                }
                else if (ch == ')')
                {
                    // Если символ — закрывающая скобка, выталкиваем операторы из стека в очередь
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    // Удаляем открывающую скобку из стека
                    operatorStack.Pop();
                }
                else
                {
                    // Если символ — оператор, проверяем приоритет
                    while (operatorStack.Count > 0 && precedence.ContainsKey(operatorStack.Peek()) &&
                           precedence[operatorStack.Peek()] >= precedence[ch])
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    // Добавляем текущий оператор в стек
                    operatorStack.Push(ch);
                }
            }
            // Выталкиваем оставшиеся операторы из стека в очередь
            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }
            // Формируем строку из очереди
            return new string(outputQueue.ToArray());
        }
    }
}