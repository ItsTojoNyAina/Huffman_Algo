using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Compression
{
    public class Leaf : Node
    {
        public char Symbol { get; }

        public Leaf(char symbol, int frequency)
        {
            Symbol = symbol;
            Frequency = frequency;
        }

        public override void CreateHuffmanTable(Dictionary<char, string> table, string prefix)
        {
            table[Symbol] = prefix;
        }
    }
}
