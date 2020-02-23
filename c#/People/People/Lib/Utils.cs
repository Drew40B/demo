using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace People.Lib
{
    public class Utils
    {

        /// <summary>
        /// Utility function to find RootCommon directory
        /// </summary>
        /// <param name="start">Ooptional directory to start at. If not specified will use the current application directory</param>
        /// <returns>Directory object that points to the rootCommon folder or null if root common cannot be found</returns>
        public static DirectoryInfo findRootCommon(DirectoryInfo start = null)
        {
            var currentDir = start?? new DirectoryInfo(Directory.GetCurrentDirectory());

            var found = currentDir.GetDirectories().FirstOrDefault(d => d.Name == "rootCommon");
               
            while (currentDir.Parent != null && found == null)
            {
                currentDir = currentDir.Parent;
                found = currentDir.GetDirectories().FirstOrDefault(d => d.Name == "rootCommon");

            }

            return found;

        }
    }
}
