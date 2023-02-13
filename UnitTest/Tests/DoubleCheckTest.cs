using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassFiles;
using ClassFiles.Classes;
using ClassFiles.Services;
using Xunit;
namespace UnitTest
{
    public class DoubleCheckTest
    {
        [Fact]
        public void OutputAdd_CorrectlyUpdatesDuplicateData()
        {
            // Arrange
            Text text = new Text("Console.WriteLine();", 10);
            FilesRead file = new FilesRead(
                new FileInfo(
                    "C:\\Users\\phili\\OneDrive - HTBLuVA Mödling\\Schule\\3.Klasse\\1.Semester\\POS\\Beispiele\\DuplikateWPF-main\\DuplikateWPF-main\\ClassesDuplikate\\Services\\DuplicateCheck.cs"),
                new List<Text> { text });
            Dictionary<string, Output> output = new Dictionary<string, Output>
                                                    {
                                                        {
                                                            "Console.WriteLine();",
                                                            new Output(new List<FileInfo>(), 0, new List<int>())
                                                        }
                                                    };

            // Act
            DuplicateCheck.OutputAdd(output, text, file);

            // Assert
            Output expectedOutput = new Output(
                new List<FileInfo> { file.FileInfo },
                1,
                new List<int> { text.LineNumber });
            string fileNameCheck = expectedOutput.FileName[0].DirectoryName;
            string fil = output["Console.WriteLine();"].FileName[0].DirectoryName;
            Assert.Equal(fileNameCheck, fil);

            int DuplicatCheck = expectedOutput.Duplicatenumber;
            int DuplicateberC = output["Console.WriteLine();"].Duplicatenumber;
            Assert.Equal(DuplicatCheck, DuplicateberC);

            int linenumberCheck = expectedOutput.LineNumber.Count;
            int linenumberC = output["Console.WriteLine();"].LineNumber.Count;
            Assert.Equal(linenumberCheck, linenumberC);
        }

        [Fact]
        public void OutputAdd_CorrectlyUpdatesDuplicateError()
        {
            // Arrange
            Text text = new Text(null, 0);
            FilesRead file = new FilesRead(
                new FileInfo(
                    "C:\\Users\\phili\\OneDrive - HTBLuVA Mödling\\Schule\\3.Klasse\\1.Semester\\POS\\Beispiele\\DuplikateWPF-main\\DuplikateWPF-main\\ClassesDuplikate\\Services\\DuplicateCheck.cs"),
                new List<Text> { text });
            Dictionary<string, Output> output = new Dictionary<string, Output>
                                                    {
                                                        {
                                                            "Console.WriteLine();",
                                                            new Output(new List<FileInfo>(), 0, new List<int>())
                                                        }
                                                    };

            // Act


            Assert.Throws<ArgumentNullException>(() => DuplicateCheck.OutputAdd(output, text, file));
            // Assert

        }
    }
}
