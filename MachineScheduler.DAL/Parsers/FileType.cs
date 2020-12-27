namespace MachineScheduler.DAL.Parsers
{
    internal class FileType
    {
        private string Name { get; set; }
        private string Extensions { get; set; }

        public FileType(string name, string extensions)
        {
            Name = name;
            Extensions = extensions;
        }
        
        public string GetDialogFilter() => $"{Name}|{Extensions}";
        public string GetExtension() => Extensions;
    }
}