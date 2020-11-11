using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace KrasNonFerMet.Core.Interfaces
{
    public interface IFile
    {
        public string DirectoryPath { get; set; }
        public string Filename { get; set; }

        public string FullPath => String.Concat(new[] { DirectoryPath, Filename });

        public bool IsExist(IFileInfo fileInfo) => fileInfo.Exists;

        public FileInfo GetFileInfo() => new FileInfo(FullPath);
        
    }
}
