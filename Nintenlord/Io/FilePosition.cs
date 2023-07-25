namespace Nintenlord.IO
{
    public readonly struct FilePosition
    {
        private readonly string file;
        private readonly int line;
        private readonly int column;

        public int Line => line;
        public int Column => column;
        public string File => file;

        public FilePosition(string file, int line, int column)
        {
            this.file = file;
            this.line = line;
            this.column = column;
        }

        public static FilePosition BeginningPosition(string file)
        {
            return new FilePosition(file, 1, 1);
        }

        public override string ToString()
        {
            return string.Format("File {0}, Line {1}, Column {2}", System.IO.Path.GetFileName(file), line, column);
        }
    }
}
