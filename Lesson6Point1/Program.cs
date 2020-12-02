using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6Point1
{
    /// <summary>
    /// Медведев А.Н.
    /// Изменить программу вывода таблицы функции так, чтобы можно было передавать функции типа double (double, double). 
    /// Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).
    /// </summary>

    public delegate double Fun(double x);
    public delegate double DoubleFun(double x, double y);
    class Program
    {

        public static void Table(Fun F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.00} | {1,8:0.00} |", x, F(x));
                x += 1;
            }
            Console.WriteLine("---------------------");

        }

        /// <summary>
        /// Перегрузка метода Table для принятия нового типа делегата
        /// </summary>
        /// <param name="F"></param>
        /// <param name="x">Первый параметр</param>
        /// <param name="b">Воторой параметр</param>
        public static void Table(DoubleFun F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.00} | {1,8:0.00} |", x, F(b, x));
                x += 1;
            }
            Console.WriteLine("---------------------");

        }

        public static double MyFunc(double x)
        {
            return x * x * x;
        }

        public static double Ax2(double a, double x)
        {
            return a * Math.Pow(x, 2);
        }

        public static double AsinX(double a, double x)
        {
            return a * Math.Sin(x);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Таблица функции MyFunc:");
            Table(new Fun(MyFunc), -2, 2);
            Console.WriteLine("Еще раз та же таблица, но вызов организован по новому");
            Table(MyFunc, -2, 2);
            Console.WriteLine("Таблица функции Sin:");
            Table(Math.Sin, -2, 2);
            Console.WriteLine("Таблица функции x^2:");
            // Упрощение(с C# 2.0). Использование анонимного метода
            Table(delegate (double x) { return x * x; }, 0, 3);

            //Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).
            Console.WriteLine("Продемонстрируем работу на функции с функцией a * x ^ 2");
            Table(Ax2, -2, 2);
            Console.WriteLine("Продемонстрируем работу на функции с функцией a*sin(x)");
            Table(AsinX, -2, 2);

        }
    }
}
