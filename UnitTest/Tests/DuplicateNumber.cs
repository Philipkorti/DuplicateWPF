using System;
using System.Collections.Generic;
using ClassFiles.Classes;
using Xunit;

namespace UnitTest
{
    public class DuplicateNumber
    {
        [Fact]
        public void DuplicateNumberAdd()
        {
            Output output = new Output(new List<System.IO.FileInfo> { }, 0, new List<int> { });
            output.GetUPDuplicate();

            Assert.Equal(1, output.Duplicatenumber);
        }
    }
}