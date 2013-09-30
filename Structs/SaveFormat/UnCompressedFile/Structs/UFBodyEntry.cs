using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Methods;
using MC.Save.Other;
namespace MC.Save.Structs.UncompressedFile
{
    public class UFBodyEntry
    {
        public string Name { get; set; }
        public Int32 Size { get; set; }
        public Int32 Offset { get; set; }
        public Int32 Unk1 { get; set; }
        public Int32 Unk2 { get; set; }
        UFHeader hdr;
        public Stream CustomStream { get; set; }
        public bool isValid { get; set; }

        public UFBodyEntry(Stream xIn, ref int offset, UFHeader hdr)
        {
            xIn.Position = offset;
            this.hdr = hdr;
            int temp = StreamUtils.ReadInt32(xIn, true);
            isValid = temp == 0x00000000 ? false : true;
            xIn.Position = offset;

            Size = 0;
            Offset = 0;
            Unk1 = 0;
            Unk2 = 0;
            Name = "";

            if (temp == 0x00000000)
                offset += 0x90;
            else
            {
                byte[] buf = StreamUtils.ReadBytes(xIn, 0x80);
                Name = Encoding.BigEndianUnicode.GetString(buf);
                Size = StreamUtils.ReadInt32(xIn, true);
                Offset = StreamUtils.ReadInt32(xIn, true);
                Unk1 = StreamUtils.ReadInt32(xIn, true);
                Unk2 = StreamUtils.ReadInt32(xIn, true);
                offset += 0x90;
            }
        }

        public void Write(Stream xOut)
        {
            if (isValid)
                StreamUtils.WriteUnicode(xOut, Name);
            else
                StreamUtils.WriteBytes(xOut, new byte[0x80]);
            StreamUtils.WriteInt32(xOut, CustomStream == null ? Size : (int)CustomStream.Length, true);
            StreamUtils.WriteInt32(xOut, Offset, true);
            StreamUtils.WriteInt32(xOut, Unk1, true);
            StreamUtils.WriteInt32(xOut, Unk2, true);
        }
        public void ExtractToStream(Stream xOut)
        {
            hdr.MainStream.Position = this.Offset;
            byte[] buf = new byte[Size];
            hdr.MainStream.Read(buf, 0, buf.Length);
            xOut.Write(buf, 0, buf.Length);
        }
        public Stream GetStream()
        {
            hdr.MainStream.Position = this.Offset;
            byte[] buf = new byte[Size];
            hdr.MainStream.Read(buf, 0, buf.Length);
            return new MemoryStream(buf);
        }
    }
}