using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Compression
{
    public abstract class Node
    {
        public int Frequency { get; protected set; }

        public abstract void CreateHuffmanTable(Dictionary<char, string> table, string prefix);
    }
}
