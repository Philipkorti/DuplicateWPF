using System;
using System.Collections.Generic;
using System.IO;
using ClassFiles.Classes;
using Xunit;

namespace UnitTest
{
    public class FileListTest
    {
        [Fact]
        public void AddTest_GetFiles()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentprpath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileending = "*.cs";
            List<string> fileList = new List<string>();
            GetFileList.GetFileNames(currentprpath, fileending, out fileList);

            Assert.NotEmpty(fileList);
        }
        
        [Fact]
        public void CheckCurrentpath()
        {
            string currentprpath = "";
            string fileending = "*.cs";
            List<string> fileList = new List<string>();

            Assert.Throws<ArgumentException>(() =>
                Directory.GetFiles(currentprpath, fileending, SearchOption.AllDirectories));
        }
        [Fact]

        public void Checkfileending0()
        {

            string currentprpath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..";
            string fileending = "";
            List<string> fileList = new List<string>();

            foreach (string file in Directory.GetFiles(currentprpath, fileending, SearchOption.AllDirectories))
            {

                fileList.Add(file);

            }

            GetFileList.GetFileNames(currentprpath, fileending, out fileList);
            Assert.Empty(fileList);
        }
    }
}