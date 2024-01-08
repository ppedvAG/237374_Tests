namespace Calculator.Tests
{
    public class FileReaderTests
    {
        [Fact]
        public void GetLinesFromFile_can_read_one_Line()
        {
            var testFile = "test.txt";
            File.WriteAllText(testFile, "Wurst\nKäse\n");

            var reader = new FileReader();
            var result = reader.GetLinesFromFile(testFile);

            Assert.Contains("Käse", result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GetLinesFromFile_emtpy_file_para_throws(string file)
        {
            var reader = new FileReader();

            if (file == null)
                Assert.Throws<ArgumentNullException>(() => reader.GetLinesFromFile(file));
            else
                Assert.Throws<ArgumentException>(() => reader.GetLinesFromFile(file));
        }


    }
}
