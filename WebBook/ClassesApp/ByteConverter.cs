using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBook.ClassesApp
{
    public class ByteConverter
    {

        public static byte[] CompressData(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress))
            {
                deflateStream.Write(data, 0, data.Length);
                deflateStream.Close();
                return compressedStream.ToArray();
            }
        }

        public static byte[] DecompressData(byte[] compressedData)
        {
            using (var compressedStream = new MemoryStream(compressedData))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            using (var decompressedStream = new MemoryStream())
            {
                deflateStream.CopyTo(decompressedStream);
                deflateStream.Close();
                return decompressedStream.ToArray();
            }
        }
    }
}
