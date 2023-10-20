using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

var commandSheet = new Dictionary<byte[], byte>()
{
    { "✍".ExtractBytes(), 0 },
};

List<string> commands = new();
List<byte[]> data = new();

string filePath = args[0];

using (var sr = new StreamReader(filePath))
{
    string linha;
    while ((linha = sr.ReadLine()) != null)
    {
        var splited = linha.Split(" ");

        var command = splited[0].ExtractBytes();

        if (!commandSheet.ContainsByteKey(command, out byte? code))
        {
            throw new Exception("Erro no comando");
        }
        
        switch (code)
        {
            case 0:
                HandlePrint(splited.Skip(1).ToArray());
                break;

            default:
                throw new Exception();
        }
    }
}

void HandlePrint(string[] arg)
{
    var temp = string.Join(' ', arg);

    var byteArr = Encoding.ASCII.GetBytes(temp);
    data.Add(byteArr);

    string cmd = $"{0}x{commands.Count}";

    commands.Add(cmd);
}

using (var sw = new StreamWriter("compiled.emojo"))
{
    foreach(var c in commands)
    {
        sw.Write(c + " ");
    }

    sw.WriteLine();

    foreach(var d in data)
    {
        foreach(var b in d)
        {
            sw.Write(b + " ");
        }

        sw.Write("- ");
    }
}
