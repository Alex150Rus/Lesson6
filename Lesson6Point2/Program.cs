using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Lesson6Point2
{
    /// <summary>
    /// Медведев А.Н.
    /// Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата. 
    /// а) Сделать меню с различными функциями и представить пользователю выбор, для какой функции и на каком отрезке
    /// находить минимум.Использовать массив(или список) делегатов, в котором хранятся различные функции.
    /// б) *Переделать функцию Load, чтобы она возвращала массив считанных значений.
    /// Пусть она возвращает минимум через параметр (с использованием модификатора out). 
    /// </summary>

    public delegate double MyDelegate(double x);

    class Program
    {
        /// <summary>
        /// Поле: массив делегатов
        /// </summary>
        public static MyDelegate[] arrayOfFuncs = {Fx2, Fx3, F};

        /// <summary>
        /// f(x^2)
        /// </summary>
        /// <param name="x">Параметр</param>
        /// <returns>f(x^2)</returns>
        public static double Fx2(double x)
        {
            return Math.Pow(x,2);
        }

        /// <summary>
        /// f(x^3)
        /// </summary>
        /// <param name="x">Параметр</param>
        /// <returns>f(x^3)</returns>
        public static double Fx3(double x)
        {
            return Math.Pow(x, 3);
        }

        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }

        /// <summary>
        /// Модифицировал программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
        /// </summary>
        /// <param name="F">Делегат Mydelegate</param>
        /// <param name="fileName"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="h"></param>
        public static void SaveFunc(MyDelegate F,string fileName, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h;// x=x+h;
            }
            bw.Close();
            fs.Close();
        }

        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }

        public static object[] LoadArrayOfValues(string fileName,out double minimum)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            ArrayList list = new ArrayList();
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
                list.Add(d);
            }
            minimum = min;
            bw.Close();
            fs.Close();
            return list.ToArray();
        }

        /// <summary>
        /// Предлагаем пользователю выбрать функцию
        /// </summary>
        /// <returns>Номер функции</returns>
        public static int ChoseFunc() {
            bool flag;
            int result;
            do {
                Console.WriteLine("Ввведите номер функции: 1 - Fx2, 2 - Fx3, 3 - Fx");
                flag = int.TryParse(Console.ReadLine(), out result);
            } while (!flag);
            return result;
        }

        /// <summary>
        /// Получаем от пользователя значения начальной и конечной точки отрезка
        /// </summary>
        /// <returns>Возвращает массив со значением начальной и конечной точки отрезка</returns>
        public static double[] ChooseLineSegment()
        {
            return new double[]{ getDoubleValue("Ввведите значение начала отрезка"), getDoubleValue("Ввведите значение конца отрезка") };
        }

        /// <summary>
        /// Просим пользователя ввести число типа double
        /// </summary>
        /// <param name="question">Просьба к пользователю</param>
        /// <returns>Число типа double</returns>
        public static double getDoubleValue(string question)
        {
            bool flag;
            double result;
            do
            {
                Console.WriteLine(question);
                flag = double.TryParse(Console.ReadLine(), out result);
            } while (!flag);
            return result;
        }

        public static string ToString(object[] array)
        {
            StringBuilder s = new StringBuilder();
            foreach (double elem in array)
            {
                s.Append($"{elem}; ");
            }
            return s.ToString();
        }

        static void Main(string[] args)
        {
            SaveFunc(F,"data.bin", -100, 100, 0.5);
            Console.WriteLine(Load("data.bin"));
            Console.ReadKey();
            Console.WriteLine();

            //а) Сделать меню с различными функциями и представить пользователю выбор, для какой функции и на каком отрезке
            // находить минимум.Использовать массив(или список) делегатов, в котором хранятся различные функции.

            int b = ChoseFunc();
            MyDelegate function = arrayOfFuncs[b-1];
            double[] arr = ChooseLineSegment();
            SaveFunc(function, "data.bin", arr[0], arr[1], 0.5);
            Console.WriteLine(Load("data.bin"));
            Console.ReadKey();
            Console.WriteLine();

            ///// б) *Переделать функцию Load, чтобы она возвращала массив считанных значений.
            /// Пусть она возвращает минимум через параметр (с использованием модификатора out).
            double minimun;
            Console.WriteLine(ToString(LoadArrayOfValues("data.bin", out minimun)));
            Console.WriteLine();
            Console.WriteLine(minimun);


        }
    }
}
