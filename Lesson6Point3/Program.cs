using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lesson6Point3
{
    /// <summary>
    /// Медведев А.Н.
    /// Переделать программу Пример использования коллекций для решения следующих задач:
    /// а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
    /// б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(*частотный массив);
    /// в) отсортировать список по возрасту студента;
    /// г) * отсортировать список по курсу и возрасту студента;
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int numOfBachelors = 0;
            int numOfMasters = 0;

            int numOf5CourseStuds = 0;
            int numOf6CourseStuds = 0;
            // Создадим необобщенный список
            ArrayList list = new ArrayList();
            List<int> ages = new List<int>();
            List<int> courseAgeList = new List<int>();
            Dictionary <int, int> ageCourse = new Dictionary <int, int>();
            // Запомним время в начале обработки данных
            DateTime dt = DateTime.Now;
            if (File.Exists("students_1.csv"))
            {
                StreamReader sr = new StreamReader("students_1.csv");
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string[] s = sr.ReadLine().Split(';');
                        // Console.WriteLine("{0}", s[0], s[1], s[2], s[3], s[4]);
                        StringBuilder name = new StringBuilder();
                        name.Append($"{s[1]} ");
                        name.Append($"{s[0]}");

                        list.Add(name.ToString());// Добавляем склееные имя и фамилию
                        ages.Add(int.Parse(s[5]));
                        courseAgeList.Add(int.Parse(s[5] + s[6]));

                        setQtyOfStudentsOnCourses(int.Parse(s[5]), int.Parse(s[6]), ref ageCourse);
                        setQtyOf5And6CourseStuds(int.Parse(s[6]), ref numOf5CourseStuds, ref numOf6CourseStuds);

                        if (int.Parse(s[6]) < 5) numOfBachelors++; else numOfMasters++;
                    }
                    catch
                    {
                    }
                }
                sr.Close();
                //в) отсортировать список по возрасту студента;
                //sortListByAgeOrCourseAge(ref list, ages);

                //г) *отсортировать список по курсу и возрасту студента;
                    sortListByAgeOrCourseAge(ref list, courseAgeList);

                foreach (var v in list) Console.WriteLine(v);
                list.Sort();
                Console.WriteLine("Всего студентов:{0}", list.Count);
                Console.WriteLine("Магистров:{0}", numOfMasters);
                Console.WriteLine("Бакалавров:{0}", numOfBachelors);
                // а) Подсчитать количество студентов учащихся на 5 и 6 курсах
                Console.WriteLine("5-и курсников:{0}", numOf5CourseStuds);
                Console.WriteLine("6-и курсников:{0}", numOf6CourseStuds);

                foreach (var v in list) Console.WriteLine(v);
                Console.WriteLine();
                
                //б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(*частотный массив);
                foreach (KeyValuePair<int, int> pair in ageCourse)
                    Console.WriteLine("{0} -> {1}", pair.Key, pair.Value);
                // Вычислим время обработки данных
            }
            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }


        /// <summary>
        /// Заполняет словарь количеством студентов на курсе
        /// </summary>
        /// <param name="age">Возраст</param>
        /// <param name="course">Курс</param>
        /// <param name="ageCourse">Словарь [курс => кол-во студентов]</param>
        public static void setQtyOfStudentsOnCourses(int age, int course, ref Dictionary<int, int> ageCourse)
        {
            if (age > 17 && age < 20)
                if (ageCourse.ContainsKey(course))
                    ageCourse[course]++;
                else
                    ageCourse.Add(course, 1);
        }

        /// <summary>
        ///     Метод вычисляет количестов 5-и и 6-и курсников
        /// </summary>
        /// <param name="course"> Курс </param>
        /// <param name="numOf5CourseStuds"> Количество 5-и курсников </param>
        /// <param name="numOf6CourseStuds">Количество 6-и курсников</param>
        public static void setQtyOf5And6CourseStuds(int course, ref int numOf5CourseStuds, ref int numOf6CourseStuds)
        {   
            if (course == 5)
                numOf5CourseStuds++;
            else if (course == 6)
                numOf6CourseStuds++;
        }

        /// <summary>
        /// метод сортировки списка по возрасту студента или по возрасту и курсу;
        /// </summary>
        /// <param name="list">список студентов</param>
        /// <param name="ageOrAgeCourse">список возрастов или список возрастов, склеенных с номером курса</param>
        public static void sortListByAgeOrCourseAge(ref ArrayList list , List<int> ageOrAgeCourse)
        {
            int length = list.Count;
            int ageOrAgeCourseLength = ageOrAgeCourse[0].ToString().Length;
            for (int i = 0; i < length; i++)
            {
                list[i] = ageOrAgeCourse[i].ToString() + list[i];
            }
            list.Sort();
            for (int i = 0; i < length; i++) list[i] = list[i].ToString().Substring(ageOrAgeCourseLength);
        }
    }
}
