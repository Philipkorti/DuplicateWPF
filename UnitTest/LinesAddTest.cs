using System.Collections.Generic;
using ClassFiles.Classes;
using ClassFiles.Services;
using Xunit;
namespace UnitTest
{
    public class LinesAddTest
    {
        [Fact]

        public void inputtest()
        {
            //Arrange
            List<string> text = new List<string>();
            List<Text> line = new List<Text>();
            List<int> filelinecounter = new List<int>();
            
            text.Add("Hallo heute");
            filelinecounter.Add(3);


            //ACT 
            LinesAdd.Lines(filelinecounter, text, out line);

            //Assert
            Assert.NotEmpty(line);
            
        }

        [Fact]
        public void emptyinputtest()
        {
            // Arrange
            var filelinecount = new List<int>();
            var text = new List<string>();
            List<Text> lines;

            // Act
            LinesAdd.Lines(filelinecount, text, out lines);

            // Assert
            Assert.Empty(lines);
        }

        [Fact]
        public void lengthtest()
        {
            // Arrange
            var filelinecount = new List<int> { 1, 2 };
            var text = new List<string> { "line1", "line2", "line3" };
            List<Text> lines;

            // Act and Assert
            LinesAdd.Lines(filelinecount, text, out lines);

            Assert.Equal(filelinecount.Count, lines.Count);
        }

        [Fact]
        public void nullvaluestest()
        {
            // Arrange
            var filelinecounter = new List<int> { 1, 2, 3 };
            var text = new List<string> { "line1", null, "line3" };
            List<Text> line;

            // Act
            LinesAdd.Lines(filelinecounter, text, out line);

            //Assert
            Assert.Null(line[1].FileText);
        }
    }
}