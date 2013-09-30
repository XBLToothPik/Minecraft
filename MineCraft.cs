using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MC.Save.Structs;
using MC.Save.Structs.CompressedFile;
using MC.Save.Structs.UncompressedFile;
using Methods;

namespace MC.Save
{
    public class MCSave
    {
        public CFFile File { get; set; }
        public MCSave(Stream xIn)
        {
            this.File = new CFFile(xIn);
        }
        public void Save(Stream xOut)
        {
            File.Write(xOut);
        }
    }

}