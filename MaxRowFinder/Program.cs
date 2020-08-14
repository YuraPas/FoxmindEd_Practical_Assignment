using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MaxRowFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedTextFile();

            try
            {
                var filePath = GetUserFilePathInput(args);

                ValidateInput(filePath);

                var fileContent = GetFileContent(filePath);

                var maxRowFinder = new MaxRowFinder();

                var maxSumRow = maxRowFinder.GetMaxElementSumRow(fileContent);

                var invalidRows = maxRowFinder.GetInvalidRows();

                Console.WriteLine($"Row with max elements sum: {maxSumRow + 1}");
                Console.WriteLine(new string('-', 40));

                OutputInvalidRowList(invalidRows);
            }
            catch
            {
                Console.WriteLine("Error occured");
            }

            Console.ReadLine();
        }

        #region Helper methods

        private static string GetUserFilePathInput(string[] commandLineArgs)
        {
            int filePathIndex = 0;

            if (commandLineArgs != null && commandLineArgs.Length > 0)
            {
                return commandLineArgs[filePathIndex];
            }

            Console.Write("Please input path to file: ");

            return Console.ReadLine();
        }

        private static void ValidateInput(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || filePath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException("Invalid file path input.");
            }
        }

        private static string[] GetFileContent(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        private static void OutputInvalidRowList(Dictionary<int, string> invalidRows)
        {
            Console.WriteLine("\t \t Invalid row list");
            Console.WriteLine();

            foreach (var row in invalidRows)
            {
                Console.WriteLine($"Row index = {row.Key + 1} \t Row = {row.Value}");
            }
        }

        #endregion

        public static void SeedTextFile()
        {
            var filePath = @"C:\Users\yuriy.paslavskyi\test.txt";

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(content))
                {
                    return;
                }
            }

            var rand = new Random();

            var rowLines = rand.Next(10, 20);

            var stringBuilder = new StringBuilder();
            using (StreamWriter sw = File.AppendText(filePath))
            {
                for (int i = 0; i < rowLines; i++)
                {
                    // generate invalid row if rand numb > 7
                    var isInvalidChance = rand.Next(10) > 7;
                    var rowItems = rand.Next(10);

                    for (int j = 0; j < rowItems; j++)
                    {
                        var multiplier = rand.Next(1, 7);
                        var invalidPosition = rand.Next(rowItems);

                        if (j == invalidPosition)
                        {
                            if (isInvalidChance)
                            {
                                stringBuilder.Append("@");

                            }
                            else
                            {
                                stringBuilder.Append(Math.Round(rand.NextDouble() * multiplier, 2));
                            }
                        }
                        else
                        {
                            stringBuilder.Append((Math.Round(rand.NextDouble() * multiplier, 2)));

                        }

                        if (j != rowItems - 1)
                        {
                            stringBuilder.Append(',');
                        }
                    }

                    stringBuilder.Append("\n");
                    sw.Write(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }
        }

    }
}