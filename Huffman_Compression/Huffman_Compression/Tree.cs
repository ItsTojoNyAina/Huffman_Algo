using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Compression
{
    public class Tree : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public Tree(Node left, Node right)
        {
            Left = left;
            Right = right;
            Frequency = left.Frequency + right.Frequency;
        }

        public override void CreateHuffmanTable(Dictionary<char, string> table, string prefix)
        {
            Left.CreateHuffmanTable(table, prefix + "0");
            Right.CreateHuffmanTable(table, prefix + "1");
        }
    }
}
