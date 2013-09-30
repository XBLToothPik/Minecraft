using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Methods;
using MC.Save.Structs.UncompressedFile;
namespace MC.Save.Structs.CompressedFile
{
    public class CFFile
    {
        public Int32 CompressedSize { get; set; }
        public Int32 UncompressedSize { get; set; }

        public UFFile DataFile { get; set; }
        public CFFile(Stream xIn)
        {
            CompressedSize = StreamUtils.ReadInt32(xIn, true);
            xIn.Position += 0x4;
            UncompressedSize = StreamUtils.ReadInt32(xIn, true);

            byte[] compressed = StreamUtils.ReadBytes(xIn, CompressedSize);
            byte[] decompressed = new byte[UncompressedSize];
            CompressionUtils.Decompress(compressed, decompressed);

            MemoryStream xMem = new MemoryStream(decompressed);
            this.DataFile = new UFFile(xMem);
        }
        public void Write(Stream xOut)
        {
            long t_off = xOut.Position;
            StreamUtils.WriteInt32(xOut, 0, true);
            StreamUtils.WriteInt32(xOut, 0, true);
            StreamUtils.WriteInt32(xOut, 0, true);
            MemoryStream xMem = new MemoryStream();
            DataFile.SaveToStream(xMem);

            StreamUtils.WriteBytes(xOut, CompressionUtils.Compress(xMem.ToArray()));

            xOut.Position = t_off;
            StreamUtils.WriteInt32(xOut, (int)xOut.Length - 12, true);
            StreamUtils.WriteInt64(xOut, xMem.Length, true);

        }
    }
}