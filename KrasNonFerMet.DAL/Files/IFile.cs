using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace KrasNonFerMet.DAL.Files
{
    public interface IFile
    {
        public string DirectoryName { get; set; }
        public string Filename { get; set; }
        public string Fullname => String.Concat(new[] {DirectoryName, Filename});
        
        public FileInfo GetFileInfo() //TODO: Make Property
        {
            var fileInfo = new FileInfo(Fullname);

            DirectoryName ??= fileInfo.DirectoryName;
            Filename ??= fileInfo.Name;

            return fileInfo;
        }

    }
}