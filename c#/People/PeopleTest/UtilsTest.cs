using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Xunit;
using People.Lib;
namespace PeopleTest
{
    public class UtilsTest
    {

        [Fact]
        public void findRootCommon_HappyPath()
        {
            var found = Utils.findRootCommon();

            Assert.NotNull(found);
            Assert.True(found.Exists);
        }

        [Fact]
        public void findRootCommon_NotFound()
        {
            // Start searching 1 directoy up from root
            var start = new DirectoryInfo(Directory.GetCurrentDirectory()).Root.GetDirectories()[0];

            var found = Utils.findRootCommon(start);

            Assert.Null(found);
         
        }
    }
}
