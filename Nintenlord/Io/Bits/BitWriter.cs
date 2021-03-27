using System.IO;
using System.Text;

namespace Nintenlord.IO.Bits
{
    public class BitWriter : BinaryWriter
    {
        private byte buffer = 0;
        private int index = 0;

        public BitWriter(Stream stream)
            : base(stream) { }

        public BitWriter(Stream stream, Encoding encoding)
            : base(stream, encoding) { }

        public void WriteBit(bool val)
        {
            if (index > 7)
            {
                this.Write(buffer);
                index = 0;
                buffer = 0;
            }
            if (val)
            {
                buffer |= (byte)(1 << index);
            }
            index++;
        }

        public override void Flush()
        {
            this.Write(buffer);
            index = 0;
            buffer = 0;
        }

        public bool IsAtEnd => this.BaseStream.Position >= this.BaseStream.Length && index > 7;
    }
}
