﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


// using(var sr = new StreamReader(filePath, Encoding.UTF8))
// {

//     var line = sr.ReadLine();
//     var bytes = Encoding.UTF8.GetBytes(line);

//     var emoji  = Encoding.UTF8.GetBytes("😀");

//     foreach(var b in bytes)
//         System.Console.Write(b + " ");

//     foreach(var b in emoji)
//         System.Console.Write(b + " ");
// }


var commandSheet = new Dictionary<byte[], byte>()
{
    { "✍".ExtractBytes(), 0 },
};

List<string> commands = new();
List<byte[]> data = new();

int count = 0;


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
                HandlePrint(splited[1]);
                break;

            default:
                throw new Exception();
        }
    }
}

void HandlePrint(string arg)
{
    byte[] d = Encoding.ASCII.GetBytes(arg);
    data.Add(d);

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