using System;
using System.IO;
using System.Linq;

namespace MyColor.Infra.Data.Repositories
{
    public class CsvFileReader : IDisposable
    {
        private readonly bool _containsHeader;
        private int counter;
        private string[] lines;
        private string[] currentFields;
        private string[] headers;
        private readonly string _delimiter;

        public string[] Lines { get; private set; }

        public CsvFileReader(string filePath, string delimiter, bool containsHeader)
        {
            _delimiter = delimiter;
            _containsHeader = containsHeader;

            Load(filePath);
        }

        private void Load(string filePath)
        {
            lines = File.ReadAllLines(filePath);
            if (_containsHeader)
            {
                headers = lines[0].Split(_delimiter);
                counter++;
            }
        }

        public bool Read()
        {
            if (counter >= lines.Length)
            {
                return false;
            }

            var line = lines[counter];
            counter++;
            currentFields = line.Split(_delimiter);

            return true;
        }

        public string GetField(string name)
        {
            if (!_containsHeader)
                return string.Empty;

            if (!headers.Contains(name))
                return string.Empty;

            var indexOfField = Array.IndexOf(headers, name);
            return GetIndex(indexOfField);
        }

        public string GetIndex(int index)
        {
            return currentFields[index];
        }

        public void Dispose()
        {
            lines = null;
            GC.SuppressFinalize(this);
        }
    }
}