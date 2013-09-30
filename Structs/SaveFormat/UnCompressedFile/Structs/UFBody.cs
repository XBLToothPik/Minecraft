using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Methods;

namespace MC.Save.Structs.UncompressedFile
{
    public class UFBody
    {
        public List<UFBodyEntry> Files { get; set; }

        public UFBody(Stream xIn, UFHeader hdr)
        {
            Files = new List<UFBodyEntry>();
            int offset = hdr.TOCOffset;
            for (int i = 0; i < hdr.FileCount; i++)
                Files.Add(new UFBodyEntry(xIn, ref offset, hdr));
        }
    }
}