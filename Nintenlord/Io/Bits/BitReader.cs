using System.IO;
using System.Text;

namespace Nintenlord.IO.Bits
{
    public class BitReader : BinaryReader
    {
        private byte buffer;
        private int index = 8;

        public BitReader(Stream stream)
            : base(stream) { }

        public BitReader(Stream stream, Encoding encoding)
            : base(stream, encoding) { }

        /// <summary>
        /// Reads one bit from stream.
        /// </summary>
        /// <returns>True of read bit was 1, else false</returns>
        public bool ReadBit()
        {
            if (index > 7)
            {
                buffer = this.ReadByte();
                index %= 8;
            }

            bool result = (buffer & (1 << index)) != 0;
            //bool result = (buffer & (0x80 >> index)) != 0;
            index++;
            return result;
        }

        public bool IsAtEnd => this.BaseStream.Position >= this.BaseStream.Length && index > 7;
    }
}
