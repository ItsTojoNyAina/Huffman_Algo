using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Huffman_Compression
{
    public class Huffman
    {
        // Création de la table Huffman à partir du texte donné
        public static Dictionary<char, string> BuildHuffmanTable(string text)
        {
            // Créer la table de fréquences
            Dictionary<char, int> frequencyMap = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (frequencyMap.ContainsKey(c))
                    frequencyMap[c]++;
                else
                    frequencyMap[c] = 1;
            }

            // Construire la file de priorité
            PriorityQueue<Node, int> pq = new PriorityQueue<Node, int>();
            foreach (var entry in frequencyMap)
            {
                pq.Enqueue(new Leaf(entry.Key, entry.Value), entry.Value);
            }

            // Construction de l'arbre Huffman
            while (pq.Count > 1)
            {
                Node left = pq.Dequeue();
                Node right = pq.Dequeue();
                Tree parent = new Tree(left, right);
                pq.Enqueue(parent, parent.Frequency);
            }

            // Création de la table Huffman
            Node root = pq.Dequeue();
            Dictionary<char, string> huffmanTable = new Dictionary<char, string>();
            root.CreateHuffmanTable(huffmanTable, "");

            return huffmanTable;
        }

        // Exporter la table Huffman en JSON
        public static void ExportToJson(Dictionary<char, string> huffmanTable, string Huff)
        {
            try
            {
                string json = JsonConvert.SerializeObject(huffmanTable, Formatting.Indented);
                File.WriteAllText(Huff, json);
                Console.WriteLine("Le fichier JSON a été exporté avec succès : " + Huff);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'exportation JSON : " + e.Message);
            }
        }

        // Compresser un texte en utilisant la table Huffman
        public static string CompressText(string input, Dictionary<char, string> huffmanTable)
        {
            string compressedText = string.Empty;
            foreach (char c in input)
            {
                compressedText += huffmanTable[c];
            }
            return compressedText;
        }
    }
}
