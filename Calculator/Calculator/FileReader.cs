using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Calculator.Tests")]

namespace Calculator
{
    internal class FileReader
    {
        public string GetLinesFromFile(string file)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(file);

            return File.ReadAllText(file);
        }
    }
}
