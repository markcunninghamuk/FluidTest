using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidTest.SampleTests.Base
{
    public class StorageFileDetails
    {
        public StorageFileDetails(string fileName, string folderPath)
        {
            FileName = fileName;
            FolderPath = folderPath;
        }
        public string FolderPath { get; internal set; }
        public string FileName { get; internal set; }
    }
}
