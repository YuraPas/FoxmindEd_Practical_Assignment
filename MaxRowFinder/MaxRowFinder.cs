using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MaxRowFinder
{
    public class MaxRowFinder
    {
        public Dictionary<int, string> InvalidRowList { get; set; }

        public MaxRowFinder()
        {
            InvalidRowList = new Dictionary<int, string>();
        }

        private const char FileSeparator = ',';

        public int GetMaxElementSumRow(string[] fileLines)
        {
            double maxRowSum = 0;
            int rowIndex = 0;
            int maxRowIndex = 0;

            foreach (var line in fileLines)
            {
                if (IsNumericRow(line))
                {
                    var elementSum = GetRowSum(line);

                    SetMaxRowSum(elementSum, rowIndex, ref maxRowSum, ref maxRowIndex);
                }
                else
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        InvalidRowList.Add(rowIndex, line);
                    }
                }

                rowIndex++;
            }

            return maxRowIndex;
        }

        public Dictionary<int, string> GetInvalidRows()
        {
            return InvalidRowList;
        }

        public void SetMaxRowSum(double elementSum, int rowIndex, ref double maxRowSum, ref int maxRowIndex)
        {
            if (elementSum > maxRowSum)
            {
                maxRowSum = elementSum;
                maxRowIndex = rowIndex;
            }
        }

        public double GetRowSum(string line)
        {
            var rowItems = SplitFileLine(line);

            double elementSum = 0;

            foreach (var rowsItem in rowItems)
            {
                elementSum += double.Parse(rowsItem, CultureInfo.InvariantCulture);
            }

            return elementSum;
        }

        public bool IsNumericRow(string line)
        {
            var rowItems = SplitFileLine(line);

            foreach (var item in rowItems)
            {
                if (!double.TryParse(item, out double result))
                {
                    return false;
                }
            }

            return true;
        }

        private string[] SplitFileLine(string line)
        {
            return line.Split(FileSeparator);
        }
    }
}
