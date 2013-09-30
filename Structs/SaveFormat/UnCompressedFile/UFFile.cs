using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Methods;
using MC.Save.Other;
namespace MC.Save.Structs.UncompressedFile
{
    public class UFFile
    {
        public UFHeader Header { get; set; }
        public UFBody Body { get; set; }

        public UFFile(Stream xIn)
        {
            this.Header = new UFHeader(xIn); ;
            this.Body = new UFBody(xIn, Header);
        }
        public void SaveToStream(Stream xOut)
        {
            long temp_headerOffset = xOut.Position;
            
            StreamUtils.WriteInt32(xOut, 0, false);
            StreamUtils.WriteInt32(xOut, 0, false); //Header place holders
            StreamUtils.WriteInt32(xOut, 0, false);

            //Write data
            for (int i = 0; i < Body.Files.Count; i++)
            {

                UFBodyEntry fEnt = Body.Files[i];

                if (fEnt.CustomStream == null)
                    Header.MainStream.Position = (long)fEnt.Offset;
                else
                    fEnt.CustomStream.Position = 0;

                Body.Files[i].Offset = (int)xOut.Position;
                StreamUtils.ReadBufferedStream(fEnt.CustomStream == null ? Header.MainStream : fEnt.CustomStream, fEnt.CustomStream == null ? fEnt.Size : (int)fEnt.CustomStream.Length, xOut);

            }

            Header.TOCOffset = (int)xOut.Position;
            Header.FileCount = Body.Files.Count;

            long xPos = xOut.Position;
            xOut.Position = temp_headerOffset;
            Header.Write(xOut);
            xOut.Position = xPos;

            //Write TOC
            for (int i = 0; i < Body.Files.Count; i++)
                Body.Files[i].Write(xOut);
        }
    }
}