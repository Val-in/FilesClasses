using System;
using System.IO;
using System.Reflection;

namespace FilesClasses
{
    class FileWriter
    {
        public static void Main()
        {
            string filePath = @"D:\C Language\FilesLesson\Students.txt"; // Укажем путь 
            if (!File.Exists(filePath)) // Проверим, существует ли файл по данному пути
            {
                //   Если не существует - создаём и записываем в строку
                using (StreamWriter sw = File.CreateText(filePath))  // Конструкция Using (будет рассмотрена в последующих юнитах)
                {
                    sw.WriteLine("Олег");
                    sw.WriteLine("Дмитрий");
                    sw.WriteLine("Иван");
                } //Почему тут записывавется текст в файлы, как это работает? Просто вызываем метод WriteLine? 
            }
            // Откроем файл и прочитаем его содержимое
            using (StreamReader sr = File.OpenText(filePath))
            {
                string str = "";
                while ((str = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной и выводим в консоль
                {
                    Console.WriteLine(str);
                }
            }

            Deletion(filePath);
            ProgramCode programCode = new ProgramCode();
            programCode.TakeTheCode();
            UsingFileInfo usingFileInfo = new UsingFileInfo();
            usingFileInfo.CreateAndManageFile();
        }

        static void Deletion(string filePath)
        {
            File.Delete(filePath);
        } 

        public class ProgramCode()
        {
            FileInfo filePath = new FileInfo(@"D:\C Language\FilesLesson\FilesLesson-8\Program.cs"); // Укажем путь
            public void TakeTheCode()
            {
                // Откроем файл и прочитаем его содержимое
                using (StreamReader sr = File.OpenText(filePath.FullName))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                        Console.WriteLine(str);
                }

                using (StreamWriter sw = filePath.AppendText())
                {
                    sw.WriteLine($"// Время запуска: {DateTime.Now}");
                }
            }
        }

    }
    public class UsingFileInfo()
    {
        public void CreateAndManageFile()

        {
            string tempFilePath = @"D:\C Language\FilesLesson\tempfile.txt"; // используем генерацию имени файла.
            var fileInfo = new FileInfo(tempFilePath); // Создаем объект класса FileInfo.

            using (StreamWriter sw = fileInfo.CreateText())
            {
                sw.WriteLine("Игорь");
                sw.WriteLine("Андрей");
                sw.WriteLine("Сергей");
            }

            //Открываем файл и читаем из него.
            using (StreamReader sr = fileInfo.OpenText())
            {
                string str = "";
                while ((str = sr.ReadLine()) != null)
                {
                    Console.WriteLine(str);
                }
            }

            try
            {
                string tempFile2Path = @"D:\C Language\FilesLesson\tempfile2.txt";
                var fileInfo2 = new FileInfo(tempFile2Path);

                // Убедимся, что файл назначения точно отсутствует
                fileInfo2.Delete();

                // Копируем информацию
                fileInfo.CopyTo(tempFile2Path);
                Console.WriteLine($"{tempFilePath} скопирован в файл {tempFile2Path}.");
                //Удаляем ранее созданный файл.
                fileInfo.Delete();
                Console.WriteLine($"{tempFilePath} удален.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e}");
            }
        }
    }     
}