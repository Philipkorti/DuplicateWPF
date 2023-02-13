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

            string currentprpath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..";
            string fileending = ".cs";
            List<string> fileList = new List<string>();


            GetFileList.GetFileNames(currentprpath, fileending, fileList);

            Assert.NotEmpty(fileList);
        }


        [Fact]

        public void CheckCurrentpath0()
        {

            string currentprpath = "";
            string fileending = ".cs";
            List<string> fileList = new List<string>();
            
                foreach (string file in Directory.GetFiles(currentprpath, fileending, SearchOption.AllDirectories))
                {

                    fileList.Add(file);

                }

                GetFileList.GetFileNames(currentprpath, fileending, fileList);
                
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

            GetFileList.GetFileNames(currentprpath, fileending, fileList);
            Assert.Empty(fileList);
        }
    }
}