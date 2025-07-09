using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("На вход программы передаются три пути к файлам: values.json tests.json report.json");
            return;
        }

        var valuesPath = args[0];
        var testsPath = args[1];
        var reportPath = args[2];

        ProcessJsonFiles(valuesPath, testsPath, reportPath);
        Console.WriteLine("Отчет записан по пути: " + reportPath);
    }

    private static void ProcessJsonFiles(string valuesPath, string testsPath, string reportPath)
    {
        var valuesJson = File.ReadAllText(valuesPath);
        var testsJson = File.ReadAllText(testsPath);
        var valuesNode = JsonNode.Parse(valuesJson);
        var testsNode = JsonNode.Parse(testsJson);
        var valueDict = new Dictionary<int, string>();
        foreach (var value in valuesNode["values"].AsArray())
        {
            var id = value["id"].GetValue<int>();
            var val = value["value"].GetValue<string>();
            valueDict[id] = val;
        }

        var reportNode = testsNode.DeepClone();
        FillValues(reportNode["tests"], valueDict);

        var options = new JsonSerializerOptions { WriteIndented = true };
        var reportJson = reportNode.ToJsonString(options);
        File.WriteAllText(reportPath, reportJson);
    }

    private static void FillValues(JsonNode node, Dictionary<int, string> valueDict)
    {
        if (node is JsonObject obj && obj.ContainsKey("id") && obj.ContainsKey("value"))
        {
            var id = obj["id"].GetValue<int>();
            if (valueDict.TryGetValue(id, out string val))
            {
                obj["value"] = val;
            }
        }

        switch (node)
        {
            case JsonObject when node["values"] is JsonArray valuesArray:
            {
                foreach (var nodeItem in valuesArray)
                {
                    FillValues(nodeItem, valueDict);
                }

                break;
            }
            case JsonArray array:
            {
                foreach (var nodeItem in array)
                {
                    FillValues(nodeItem, valueDict);
                }

                break;
            }
        }
    }
}