using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Formats.Asn1;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
 
class Program
{
    public static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetTempPath(), "sync-filestream.txt");
 
        string fullText = $"This is the first line{Environment.NewLine}This is the second line";
 
        WriteTextSync(path, fullText);
        ReadTextSync(path);
 
    }
 
    static void WriteTextSync(string filePath, string text)
    {
        byte[] encodedText = Encoding.Unicode.GetBytes(text);
 
        using var sourceStream = new FileStream(
            filePath,
            FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: 4096, useAsync: false);
 
        sourceStream.Write(encodedText, 0, encodedText.Length);
 
    }
 
    static void ReadTextSync(string filePath)
    {
        using var sourceStream = new FileStream(
            filePath,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: false);
 
        byte[] buffer = new byte[sourceStream.Length];
        int bytesRead = sourceStream.Read(buffer, 0, buffer.Length);
 
        string decodedText = Encoding.Unicode.GetString(buffer, 0, bytesRead);
        Console.WriteLine(decodedText);
 
    }
}