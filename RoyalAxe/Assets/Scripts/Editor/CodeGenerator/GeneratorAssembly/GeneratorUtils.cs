using System;
using System.IO;
using UnityEditor;

namespace CodeGenerator
{
    /// <summary>
    ///     Вспомогательные методы для кодогенератора юнити
    /// </summary>
    public static class GeneratorUtils
    {
        /// <summary>
        ///     Найти путь к файлу по имени типа ВАЖНО. Имя и название файла должно соответсвовать типу
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string FindFileByType<T>()
        {
            return FindFileByType(typeof(T));
        }

        /// <summary>
        ///     Найти путь к файлу по имени типа ВАЖНО. Имя и название файла должно соответсвовать типу
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string FindFileByType(Type type)
        {
            return FindFile(type.Name);
        }

        private static string FindFile(string nameFile)
        {
            foreach (var guids in AssetDatabase.FindAssets(nameFile))
            {
                var filePath = AssetDatabase.GUIDToAssetPath(guids);
                var fileName = Path.GetFileName(filePath);
                if (fileName == $"{nameFile}.cs")
                {
                    return filePath;
                }
            }

            throw new ArgumentException($"Couldn't find file for type {nameFile}");
        }

        /// <summary>
        ///     Записать код в файл скрипта(класса, енума и т.п.) по его типу
        /// </summary>
        /// <param name="gType"></param>
        /// <param name="generator"></param>
        public static void WriteCode(string FileName, ICodeGenerator generator)
        {
            var filePath = FindFile(FileName);
            File.WriteAllText(filePath, generator.Generate());
            AssetDatabase.Refresh();
        }

        /// <summary>
        ///     Записать код в файл скрипта(класса, енума и т.п.) по его типу
        /// </summary>
        /// <param name="gType"></param>
        /// <param name="generator"></param>
        public static void WriteCode<T>(ICodeGenerator generator)
        {
            var filePath = FindFileByType<T>();
            File.WriteAllText(filePath, generator.Generate());
            AssetDatabase.Refresh();
        }
    }
}