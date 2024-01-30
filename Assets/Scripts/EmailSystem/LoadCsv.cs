using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class TSVReader
{
    
    public static Dictionary<string, List<string>> ReadTSV(TextAsset emailTextAsset)
    {

        TextAsset rawEmailString;
        var data = new Dictionary<string, List<string>>();
        using (var reader = new StreamReader(GenerateStreamFromString(emailTextAsset.text)))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                return data;

            var headers = SplitCsvLine(headerLine);
            foreach (var header in headers)
            {
                data[header] = new List<string>();
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                // Handle multi-line cells
                while (CountCharacterOccurrences(line, '\"') % 2 != 0)
                {
                    line += "\n" + reader.ReadLine();
                }

                var values = SplitCsvLine(line);

                for (int i = 0; i < values.Length; i++)
                {
                    if (i < headers.Length) // Check to prevent index out of range
                    {
                        data[headers[i]].Add(RemoveSurroundingQuotes(values[i]));
                    }
                }
            }
        }
        return data;
    }

    private static string[] SplitCsvLine(string line)
    {
        // Use Regex to split the line by tabs, considering quoted strings
        return Regex.Split(line, "\t(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
    }

    private static int CountCharacterOccurrences(string input, char character)
    {
        // Count how many times a character appears in the string
        int count = 0;
        foreach (char c in input)
        {
            if (c == character) count++;
        }
        return count;
    }

    private static string RemoveSurroundingQuotes(string input)
    {
        // Remove quotes only if they wrap the entire string
        if (input.StartsWith("\"") && input.EndsWith("\""))
        {
            return input.Substring(1, input.Length - 2).Replace("\"\"", "\"");
        }
        return input;
    }
    
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
