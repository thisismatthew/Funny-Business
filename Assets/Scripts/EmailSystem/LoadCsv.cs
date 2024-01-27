using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class TSVReader
{
    public static Dictionary<string, List<string>> ReadTSV(string filePath)
    {
        var data = new Dictionary<string, List<string>>();
        using (var reader = new StreamReader(filePath))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                return data;

            var headers = headerLine.Split('\t'); // Change delimiter to tab
            foreach (var header in headers)
            {
                data[header] = new List<string>();
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t'); // Change delimiter to tab
                Debug.Log("Length: " + values.Length.ToString());

                for (int i = 0; i < values.Length; i++)
                {
                    // Debug.Log("Row: " + i.ToString());
                    if (i < headers.Length) // Check to prevent index out of range
                    {
                        data[headers[i]].Add(values[i]);
                    }
                }
            }
        }
        return data;
    }
}
