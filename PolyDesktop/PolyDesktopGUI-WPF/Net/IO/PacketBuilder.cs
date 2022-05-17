using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Net.IO
{
    class PacketBuilder
    {
        MemoryStream _ms;

        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }

        public void WriteMessage(byte opcode, string msg)
        {
            var msgLength = msg.Length;
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] msgByte = unicodeEncoding.GetBytes(msg);
            _ms.Write(msgByte, 0, msgByte.Length);

            int count = 0;
            _ms.WriteByte(opcode);
            while (count < msgByte.Length)
            {
               _ms.WriteByte(msgByte[count++]);
            }
            //_ms.Write(BitConverter.GetBytes(msgLength),0,0);
            //_ms.Write(Encoding.ASCII.GetBytes(msg),0,0);
        }

        public byte[] GetPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
