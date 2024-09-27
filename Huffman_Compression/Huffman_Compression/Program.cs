using System;
using System.Collections.Generic;
using System.IO;
using Huffman_Compression;
using Newtonsoft.Json;

namespace Huffman_Compression
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demander à l'utilisateur de choisir une action (Compression ou Décompression)
            Console.WriteLine("Choisissez une option:");
            Console.WriteLine("1: Compresser 'Compress.txt'");
            Console.WriteLine("2: Décompresser 'texte_compressé.txt'");
            string choix = Console.ReadLine();

            if (choix == "1")
            {
                Console.WriteLine("Compression du fichier 'Compress.txt'...");
                string inputText = File.ReadAllText("Compress.txt");

                var huffmanTable = Huffman.BuildHuffmanTable(inputText);

                Huffman.ExportToJson(huffmanTable, "huffman_table.json");

                string compressedText = Huffman.CompressText(inputText, huffmanTable);
                File.WriteAllText("texte_compressé.txt", compressedText);

                Console.WriteLine("Le fichier 'Compress.txt' a été compressé avec succès en 'texte_compressé.txt'.");
            }
            else if (choix == "2")
            {
                Console.WriteLine("Décompression de 'texte_compressé.txt'...");
                Dictionary<string, char> huffmanTable = ImportFromJson("huffman_table.json");

                string compressedText = File.ReadAllText("texte_compressé.txt");

                string decompressedText = DecompressText(compressedText, huffmanTable);

                File.WriteAllText("texte_decompressé.txt", decompressedText);

                Console.WriteLine("Le fichier texte a été décompressé avec succès en 'texte_decompressé.txt'.");
            }
            else
            {
                Console.WriteLine("Option invalide. Veuillez choisir 1 ou 2.");
            }
        }

        static Dictionary<string, char> ImportFromJson(string fileName)
        {
            try
            {
                string json = File.ReadAllText(fileName);
                Dictionary<char, string> originalTable = JsonConvert.DeserializeObject<Dictionary<char, string>>(json);

                Dictionary<string, char> invertedTable = new Dictionary<string, char>();
                foreach (var entry in originalTable)
                {
                    invertedTable[entry.Value] = entry.Key;
                }
                return invertedTable;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'importation JSON : " + e.Message);
                return null;
            }
        }

        static string DecompressText(string compressedText, Dictionary<string, char> huffmanTable)
        {
            string currentCode = "";
            string decompressedText = "";

            foreach (char bit in compressedText)
            {
                currentCode += bit;

                if (huffmanTable.ContainsKey(currentCode))
                {
                    decompressedText += huffmanTable[currentCode];
                    currentCode = ""; 
                }
            }

            return decompressedText;
        }
    }
}


