using System;
using System.Collections.Generic;
using System.IO;
using ClassFiles;
using ClassFiles.Classes;
using ClassFiles.Services;
using Xunit;

namespace UnitTest
{
    public class OutputAddTest
    {
         [Fact]
        public void OutputCheckNull()
        {
            Text text = new Text(null, 10);
            FilesRead file1 = new FilesRead(
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

            FilesRead file2 = new FilesRead(
                new FileInfo(
                    "C:\\Users\\phili\\OneDrive - HTBLuVA Mödling\\Schule\\3.Klasse\\1.Semester\\POS\\Beispiele\\DuplikateWPF-main\\DuplikateWPF-main\\ClassesDuplikate\\Services\\DuplicateCheck.cs"),
                new List<Text> { text });


            Assert.Throws<ArgumentNullException>(() => DuplicateCheck.OutputCheck(output, text, file1, file2));
        }

        [Fact]
        public void OutputCheckNotNull()
        {
            Text text = new Text("Console.WriteLine();", 10);
            FilesRead file1 = new FilesRead(
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

            FilesRead file2 = new FilesRead(
                new FileInfo(
                    "C:\\Users\\phili\\OneDrive - HTBLuVA Mödling\\Schule\\3.Klasse\\1.Semester\\POS\\Beispiele\\DuplikateWPF-main\\DuplikateWPF-main\\ClassesDuplikate\\Services\\DuplicateCheck.cs"),
                new List<Text> { text });

            // Act
            DuplicateCheck.OutputCheck(output, text, file1, file2);

            // Assert
            Output expectedOutput2 = new Output(
                new List<FileInfo> { file2.FileInfo },
                1,
                new List<int> { text.LineNumber });
            string fileNameCheck2 = expectedOutput2.FileName[0].DirectoryName;
            string fil2 = output["Console.WriteLine();"].FileName[0].DirectoryName;
            Assert.Equal(fileNameCheck2, fil2);

            int DuplicatCheck2 = expectedOutput2.Duplicatenumber;
            int DuplicateberC2 = output["Console.WriteLine();"].Duplicatenumber;
            Assert.Equal(DuplicatCheck2, DuplicateberC2);

            int linenumberCheck2 = expectedOutput2.LineNumber.Count;
            int linenumberC2 = output["Console.WriteLine();"].LineNumber.Count;
            Assert.Equal(linenumberCheck2, linenumberC2);

            Output expectedOutput = new Output(
                new List<FileInfo> { file1.FileInfo },
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
    }
}