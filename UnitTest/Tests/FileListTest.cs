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
            string currentPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileEnding = "*.cs";
            List<string> fileList = new List<string>();
            GetFileList.GetFileNames(currentPath, fileEnding, out fileList);

            Assert.NotEmpty(fileList);
        }
        
        [Fact]
        public void CheckCurrentPath()
        {
            string currentPath = "";
            string fileEnding = "*.cs";
            List<string> fileList = new List<string>();
            GetFileList.GetFileNames(currentPath, fileEnding, out fileList);
            Assert.Empty(fileList);
        }
        [Fact]

        public void CheckfileEnding()
        {

            string workingDirectory = Environment.CurrentDirectory;
            string currentPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileEnding = "";
            List<string> fileList = new List<string>();

            GetFileList.GetFileNames(currentPath, fileEnding, out fileList);
            Assert.Empty(fileList);
        }

        [Fact]
        public void CheckFileendingPath()
        {
            string currentPath = "";
            string fileEnding = "";
            List<string> fileList = new List<string>();
            
            GetFileList.GetFileNames(currentPath, fileEnding, out fileList);
            
            Assert.Empty(fileList);
        }
    }
}