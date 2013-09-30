using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Methods;
namespace MC.Save.Structs.UncompressedFile
{
    public class UFHeader
    {
        public Int32 TOCOffset { get; set; }
        public Int32 FileCount { get; set; }
        public Int32 Unk { get; set; }

        public Stream MainStream { get; set; }
        public UFHeader(Stream xIn)
        {
            TOCOffset = StreamUtils.ReadInt32(xIn, true);
            FileCount = StreamUtils.ReadInt32(xIn, true);
            Unk = StreamUtils.ReadInt32(xIn, true);
            this.MainStream = xIn;
        }
        public void Write(Stream xOut)
        {
            StreamUtils.WriteInt32(xOut, TOCOffset, true);
            StreamUtils.WriteInt32(xOut, FileCount, true);
            StreamUtils.WriteInt32(xOut, Unk, true);
        }
    }
}
