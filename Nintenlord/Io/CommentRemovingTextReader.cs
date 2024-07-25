using System.IO;
using System.Text;

namespace Nintenlord.IO
{

    public sealed class CommentRemovingTextReader : TextReader
    {
        private readonly TextReader mainReader;
        private readonly StringBuilder currentLine = new(64);
        private int index = 0;
        private int blockCommentDepth = 0;

        public CommentRemovingTextReader(TextReader mainReader)
        {
            this.mainReader = mainReader;
        }

        public override int Read()
        {
            while (index >= currentLine.Length)
            {
                ReadUncommentedLine();
            }

            if (index == -1)
            {
                return -1;
            }
            else
            {
                return currentLine[index++];
            }
        }

        public override int Peek()
        {
            while (index >= currentLine.Length)
            {
                ReadUncommentedLine();
            }

            return index == -1 ? -1 : currentLine[index];
        }

        private void ReadUncommentedLine()
        {
            var line = mainReader.ReadLine();
            if (line == null)
            {
                index = -1;
                return;
            }
            currentLine.Clear();
            currentLine.Append(line);

            Utility.Strings.Parser.RemoveComments(currentLine, ref blockCommentDepth);
            if (blockCommentDepth == 0)
            {
                currentLine.AppendLine();
            }
            index = 0;
        }
    }
}
