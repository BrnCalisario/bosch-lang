using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

var commandSheet = new Dictionary<byte[], byte>()
{
    { "✍".ExtractBytes(), 0 },
    { "➕".ExtractBytes(), 1 },
    { "🤔".ExtractBytes(), 2 },
};

List<string> commands = new();
List<byte[]> data = new();

string filePath = args[0];

using (var sr = new StreamReader(filePath))
{
    string linha;

    while ((linha = sr.ReadLine()) != null)
    {

        if(linha.Length == 0)
            continue;

        var splited = linha.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var command = splited[0].ExtractBytes();

        if (!commandSheet.ContainsByteKey(command, out byte? code))
        {
            throw new Exception("Erro no comando");
        }

        var cmdArgs = splited.Skip(1).ToArray();

        switch (code)
        {
            case 0:
                HandlePrint(cmdArgs);
                break;
            case 1:
                HandleSum(cmdArgs);
                break;
            default:
                throw new Exception();
        }
    }
}

void HandlePrint(string[] args)
{
    var temp = string.Join(' ', args);

    var byteArr = Encoding.ASCII.GetBytes(temp);
    
    string cmd = $"{0}x{data.Count}";
    data.Add(byteArr);


    commands.Add(cmd);
}

void HandleSum(string[] args)
{
    string cmd = $"{1}x";

    for (int i = 0; i < args.Length; i++)
    {
        if (!int.TryParse(args[i], out _))
            throw new Exception("Parâmetro não é número");

        cmd += data.Count + "e";
        data.Add(Encoding.ASCII.GetBytes(args[i]));
    }
    // for (int i = 0; i < args.Length; i++)
    // {
    //     cmd += (data.Count - (1 - i)) + "e";
    // }
    cmd = cmd.Remove(cmd.Length - 1);

    commands.Add(cmd);
}

using (var sw = new StreamWriter("../compiled.emojo"))
{
    foreach (var c in commands)
    {
        sw.Write(c + " ");
    }

    sw.WriteLine();

    foreach (var d in data)
    {
        foreach (var b in d)
        {
            sw.Write(b + " ");
        }

        sw.Write("- ");
    }
}
