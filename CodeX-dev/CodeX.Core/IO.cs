using System.Threading.Tasks;
// ReSharper disable CheckNamespace
using System.Drawing.Imaging;

namespace CodeX.IO
// ReSharper restore CheckNamespace
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Strings;

    /// <summary>
    /// Explains on IO functions in C#
    /// </summary>
    public static class IO
    {
        /// <summary>
        /// Serializes as string.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="o">The object.</param>
        /// <param name="indented">if set to <c>true</c> [xml will be indented].</param>
        /// <returns>
        /// Object as XML
        /// </returns>
        /// <example><code>
        /// Person.ToXml(typeof(Person)) returns '&lt;Person&gt;&lt;Age&gt;12&lt;/Age&gt;&lt;Name&gt;Skywalker&lt;/Name&gt;&lt;/Person&gt;'
        /// </code> </example>
        public static string ToXml<T>(this object o, bool indented = false)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add(String.Empty, String.Empty);

            using (XmlWriter xmlWriter = new XmlTextWriterFormattedNoDeclaration(stringWriter, indented))
            {
                xmlSerializer.Serialize(xmlWriter, o, ns);
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T">Generic Parameter T</typeparam>
        /// <param name="xml">The XML string that contains the properties on the object.</param>
        /// <returns>
        /// Deserialized Object
        /// </returns>
        /// <example><code>
        /// &lt;Person&gt;&lt;Age&gt;12&lt;/Age&gt;&lt;Name&gt;Skywalker&lt;/Name&gt;&lt;/Person&gt; returns new Person{Age=12,Name="Skywalker"}
        /// </code></example>
        public static T ToObject<T>(this string xml)
        {
            if (typeof (T) == typeof (Dictionary<string, string>))
            {
                var deserializer = new DataContractSerializer(typeof(Dictionary<string, string>));
                var obj = deserializer.ReadObject(new XmlTextReader(new StringReader(xml)));
                return (T)obj;
            }
            var xmlDeserializer = new XmlSerializer(typeof(T));
            var o = Convert.ChangeType(xmlDeserializer.Deserialize(new NamespaceIgnorantXmlTextReader(new StringReader(xml))), typeof(T));
            return (T)o;
        }

        /// <summary>
        /// Serialize To Binary
        /// </summary>
        /// <param name="o">Object to be serialized.</param>
        /// <returns>
        /// Memory Stream
        /// </returns>
        public static MemoryStream ToBinary(this object o)
        {
            var stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }

        /// <summary>
        /// To the object.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="stream">Stream to be converted.</param>
        /// <returns>Object of type T</returns>
        public static T ToObject<T>(this Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }

        /// <summary>
        /// Combines the byte arrays.
        /// </summary>
        /// <param name="first">First Byte</param>
        /// <param name="second">Byte to appended</param>
        /// <returns>
        /// Byte Array
        /// </returns>
        public static byte[] AddRange(this byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        /// <summary>
        /// Gets the files from directory for multiple search patterns.
        /// </summary>
        /// <param name="directoryInfo">The directory info.</param>
        /// <param name="patterns">Search patterns</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>
        /// Array of files from pattern
        /// </returns>
        public static FileInfo[] GetFiles(this DirectoryInfo directoryInfo, IEnumerable<string> patterns, SearchOption searchOption)
        {
            var retFileList = new List<FileInfo>();

            foreach (var pattern in patterns)
            {
                retFileList.AddRange(directoryInfo.GetFiles(pattern));
            }

            return retFileList.ToArray();
        }

        /// <summary>
        /// Gets the files from directory for multiple search patterns.
        /// </summary>
        /// <param name="directoryInfo">The directory info.</param>
        /// <param name="patterns">The patterns.</param>
        /// <returns>
        /// Array of files from pattern
        /// </returns>
        public static FileInfo[] GetFiles(this DirectoryInfo directoryInfo, IEnumerable<string> patterns)
        {
            return directoryInfo.GetFiles(patterns, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumerates the files in Directory for multiple search pattern.
        /// </summary>
        /// <param name="directoryInfo">The directory info.</param>
        /// <param name="searchPatterns">List of search patterns.</param>
        /// <param name="searchOption">search option.</param>
        /// <returns>Enumerable list of FileInfo</returns>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo, IEnumerable<string> searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.ToList().SelectMany(x => directoryInfo.EnumerateFiles(x, searchOption));
        }

        /// <summary>
        /// Enumerates the files in Directory for multiple search pattern.
        /// </summary>
        /// <param name="directoryInfo">The directory info.</param>
        /// <param name="searchPatterns">List of search patterns.</param>
        /// <returns>Enumerable list of FileInfo</returns>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo, IEnumerable<string> searchPatterns)
        {
            return directoryInfo.EnumerateFiles(searchPatterns, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Gets the lines.
        /// </summary>
        /// <param name="path">The input file path.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Lines as Enumerable</returns>
        public static IEnumerable<string> Lines(this string path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding).AsEnumerable();
        }

        /// <summary>
        /// Gets the lines.
        /// </summary>
        /// <param name="path">The input file path.</param>
        /// <returns>
        /// Lines as Enumerable
        /// </returns>
        public static IEnumerable<string> Lines(this string path)
        {
            return File.ReadAllLines(path, Encoding.Default).AsEnumerable();
        }

        /// <summary>
        /// Words in specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="seperator">The separator default=','.</param>
        /// <returns>Words in the line using the separator</returns>
        public static IEnumerable<string> Words(this string line, string seperator = ",")
        {
            return line.Split(new[] { seperator }, StringSplitOptions.None).AsEnumerable();
        }

        /// <summary>
        /// Sorts the file in specified path.
        /// </summary>
        /// <param name="path">Input file path.</param>
        /// <param name="orderByColumnIndex">Index of the order by column.</param>
        /// <param name="direction">The direction ascending or descending.</param>
        /// <param name="seperator">The separator.</param>
        /// <returns>Sorted Entries in file</returns>
        public static IEnumerable<string> Sort(this string path, int orderByColumnIndex, ListSortDirection direction, string seperator = ",")
        {
            var tempDict = new Dictionary<string, List<string>>();

            foreach (var line in path.Lines())
            {
                var columnKey = line.Words(seperator).ElementAt(orderByColumnIndex);
                if (tempDict.Keys.Contains(columnKey))
                {
                    tempDict[columnKey].Add(line);
                }
                else
                {
                    tempDict.Add(
                        columnKey,
                        new List<string>
                            {
                            line
                        });
                }
            }

            if (direction == ListSortDirection.Ascending)
            {
                return tempDict.OrderBy(x => x.Key).SelectMany(y => y.Value);
            }

            return tempDict.OrderByDescending(x => x.Key).SelectMany(y => y.Value);
        }

        /// <summary>
        /// Sorts the file in specified path and considers the first line of the File as String.
        /// </summary>
        /// <param name="path">Input file path.</param>
        /// <param name="orderByColumnName">Name of the order by column.</param>
        /// <param name="direction">The direction ascending or descending.</param>
        /// <param name="seperator">The separator.</param>
        /// <returns>
        /// Sorted Entries in file
        /// </returns>
        public static IEnumerable<string> Sort(this string path, string orderByColumnName, ListSortDirection direction, string seperator = ",")
        {
            var lines = path.Lines().ToList();
            if (lines.Any())
            {
                var retList = new List<string>();
                var firstLine = lines.First();
                var columnIndex = firstLine.Words(seperator).ToArray().FindNextIndex(orderByColumnName).First();
                var remainingLines = lines.Skip(1);
                var tempDict = remainingLines.ToDictionary(line => line.Words(seperator).ElementAt(columnIndex));
                retList.Add(firstLine);
                if (direction == ListSortDirection.Ascending)
                {
                    retList.AddRange((from entry in tempDict orderby entry.Value ascending select entry.Value).ToList());
                    return retList;
                }

                retList.AddRange((from entry in tempDict orderby entry.Value descending select entry.Value).ToList());
                return retList;
            }

            return null;
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="enumerable">The enumerable set of values.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="encoding">Encoding to be used.</param>
        /// /// <example>{1,2,3,4,5}.WriteToFile("numbers.txt",Encoding.ASCII) writes the numbers one in each line in ASCII format</example>
        public static void WriteToFile(this IEnumerable<string> enumerable, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, String.Join(Environment.NewLine, enumerable), encoding);
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="enumerable">The enumerable set of values.</param>
        /// <param name="filePath">Output file path.</param>
        /// <example>{1,2,3,4,5}.WriteToFile("numbers.txt") writes the numbers one in each line</example>
        public static void WriteToFile(this IEnumerable<string> enumerable, string filePath)
        {
            enumerable.WriteToFile(filePath, Encoding.Default);
        }

        /// <summary>
        /// Converts file as image
        /// </summary>
        /// <param name="filepath">Path of the file</param>
        /// <returns>Image object</returns>
        public static Image AsImage(this string filepath)
        {
            return Image.FromFile(filepath);
        }

        /// <summary>
        /// Converts file path as FileInfo
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="shouldCreate">if set to <c>true</c> [Create file].</param>
        /// <returns>
        /// File Information
        /// </returns>
        public static FileInfo AsFile(this string filePath, bool shouldCreate = false)
        {
            if (shouldCreate)
            {
                if (!File.Exists(filePath))
                {
                    //Closing the stream after file creation
                    //So as to facilitate the caller to use the same. 
                    //Else this will block the caller from using that.
                    File.Create(filePath).Close();
                }
            }

            return new FileInfo(filePath);
        }

        /// <summary>
        /// Converts path to Directory Info
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        /// <param name="shouldCreate">if set to <c>true</c> [create folder].</param>
        /// <returns>
        /// Directory Information
        /// </returns>
        public static DirectoryInfo AsFolder(this string dirPath, bool shouldCreate = false)
        {
            if (shouldCreate)
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }

            return new DirectoryInfo(dirPath);
        }

        /// <summary>
        /// url to URI
        /// </summary>
        /// <param name="url">Url as string</param>
        /// <returns>Uri <see cref="System.Uri"/></returns>
        public static Uri AsUri(this string url)
        {
            return new Uri(url);
        }

        /// <summary>
        /// Converts uri to Image Object
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns><see cref="System.Drawing.Image"/></returns>
        public static Image AsImage(this Uri uri)
        {
            var webClient = new WebClient();
            return Image.FromStream(new MemoryStream(webClient.DownloadData(uri)));
        }

        /// <summary>
        /// To the stream.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>Stream object</returns>
        public static Stream ToStream(this Image image, ImageFormat imageFormat)
        {
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, imageFormat);
            return memoryStream;
        }

        /// <summary>
        /// Reads file content as text.
        /// </summary>
        /// <param name="fileInfo">The file info object.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>File content as string</returns>
        public static string ReadAsText(this FileInfo fileInfo, Encoding encoding)
        {
            return File.ReadAllText(fileInfo.FullName, encoding);
        }

        /// <summary>
        /// Reads file content as text.
        /// </summary>
        /// <param name="fileInfo">The file info.</param>
        /// <returns>File content as string</returns>
        public static string ReadAsText(this FileInfo fileInfo)
        {
            return File.ReadAllText(fileInfo.FullName);
        }

        /// <summary>
        /// Writes the text to file.
        /// </summary>
        /// <param name="fileInfo">file info.</param>
        /// <param name="content">file content.</param>
        /// <param name="encoding">string encoding.</param>
        public static void WriteText(this FileInfo fileInfo, string content, Encoding encoding)
        {
            File.WriteAllText(fileInfo.FullName, content, encoding);
        }

        /// <summary>
        /// Writes the text.
        /// </summary>
        /// <param name="fileInfo">file info.</param>
        /// <param name="content">file content.</param>
        public static void WriteText(this FileInfo fileInfo, string content)
        {
            File.WriteAllText(fileInfo.FullName, content);
        }
        
        /// <summary>
        /// To the stream.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns><see cref="System.IO.Stream"/></returns>
        public static Stream ToStream(this Image image)
        {
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream;
        }

        /// <summary>
        /// To the base64 string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="options">The options.</param>
        /// <returns>String in Base64</returns>
        public static string ToBase64String(this byte[] bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(bytes, options);
        }

        /// <summary>
        /// Gets the lines from FlatFile
        /// </summary>
        /// <param name="fileInfo">FileInfo Object</param>
        /// <param name="encoding">Encoding of the File</param>
        /// <returns>Enumerable string collection</returns>
        public static IEnumerable<string> Lines(this FileInfo fileInfo, Encoding encoding)
        {
            return fileInfo.FullName.Lines(encoding);
        }

        /// <summary>
        /// Gets the lines from FlatFile
        /// </summary>
        /// <param name="fileInfo">FileInfo Object</param>
        /// <returns>Enumerable string collection</returns>
        public static IEnumerable<string> Lines(this FileInfo fileInfo)
        {
            return fileInfo.FullName.Lines(Encoding.Default);
        }

        /// <summary>
        /// To the base64 string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="options">The options.</param>
        /// <returns>string as Base64</returns>
        public static string ToBase64String(this byte[] bytes, int offset, int length, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(bytes, offset, length, options);
        }

        /// <summary>
        /// Converts delimited file into DataTable
        /// </summary>
        /// <param name="fileInfo">FileInfo object</param>
        /// <param name="delimiter">delimiter value. default value is ","</param>
        /// <param name="hasHeader">if set to <c>true</c> [has header]. by default it is true</param>
        /// <param name="tableName">Name of the table. default value is "FlatFileDataTable"</param>
        /// <returns>Loaded Database</returns>
        public static DataTable ToDataTable(this FileInfo fileInfo, string delimiter = ",", bool hasHeader = true, string tableName = "FlatFileDataTable")
        {
            var retDataTable = new DataTable(tableName);
            var lines = fileInfo.Lines().ToList();
            if (hasHeader)
            {
                foreach (var word in lines.First().Words(delimiter))
                {
                    retDataTable.Columns.Add(word);
                }
            }
            else
            {
                for (var i = 0; i < lines.First().Words(delimiter).Count(); i++)
                {
                    retDataTable.Columns.Add("Column{0}".Fill(i));
                }
            }

            var restOfLines = (hasHeader ? lines.Skip(1) : lines).ToList();
            for (var i = 0; i < restOfLines.Count(); i++)
            {
                var row = retDataTable.NewRow();
                var words = restOfLines[i].Words(delimiter).ToList();
                for (var j = 0; j < words.Count(); j++)
                {
                    row[j] = words[j];
                }

                retDataTable.Rows.Add(row);
            }

            return retDataTable;
        }

        /// <summary>
        /// Writes <c>datatable</c> to file.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="outputFilePath">The output file path.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        public static void WriteToFile(this DataTable dataTable, string outputFilePath, Encoding encoding, string delimiter = ",")
        {
            var lines = new List<string>
                {
                    String.Join(delimiter, from DataColumn column in dataTable.Columns select column.ColumnName)
                };
            foreach (DataRow row in dataTable.Rows)
            {
                var line = new List<string>();
                for (var i = 0; i < dataTable.Columns.Count; i++)
                {
                    line.Add(row[i].ToString());
                }

                lines.Add(String.Join(delimiter, line));
            }

            lines.WriteToFile(outputFilePath, encoding);
        }

        /// <summary>
        /// To the XML.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="indented">if set to <c>true</c> [indented].</param>
        /// <returns>Serialized Dictionary</returns>
        public static string ToXml(this Dictionary<string,string> dict, bool indented = false)
        {
            var serializer = new DataContractSerializer(dict.GetType());
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    // add formatting so the XML is easy to read in the log
                    if (indented)
                    {
                        writer.Formatting = Formatting.Indented;
                    }
                    serializer.WriteObject(writer, dict);
                    writer.Flush();
                    return sw.ToString();
                }
            }
        }

        /// <summary>
        /// To the dictionary.
        /// </summary>
        /// <param name="s">The Dictionary xml.</param>
        /// <returns>Dictionary object</returns>
        public static Dictionary<string, string> ToDictionary(this string s)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(typeof(Dictionary<string, string>));
                return (Dictionary<string, string>)deserializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Writes <c>datatable</c> to file.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="outputFilePath">The output file path.</param>
        /// <param name="delimiter">The delimiter.</param>
        public static void WriteToFile(this DataTable dataTable, string outputFilePath, string delimiter = ",")
        {
            dataTable.WriteToFile(outputFilePath, Encoding.Default, delimiter);
        }

        /// <summary>
        /// Prints the specified automatic.
        /// </summary>
        /// <param name="o">The automatic.</param>
        /// <param name="args">The arguments.</param>
        public static void Print(this object o, params object[] args)
        {
            Console.Out.Write(o.ToString().Fill(args));
        }

        /// <summary>
        /// Prints the line.
        /// </summary>
        /// <param name="o">The automatic.</param>
        /// <param name="args">The arguments.</param>
        public static void PrintLine(this object o, params object[] args)
        {
            Console.Out.WriteLine(o.ToString().Fill(args));
        }

        /// <summary>
        /// Prints the error.
        /// </summary>
        /// <param name="o">input object</param>
        /// <param name="args">The arguments.</param>
        public static void PrintError(this object o, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.Write(o.ToString().Fill(args));
        }

        /// <summary>
        /// Prints the error line.
        /// </summary>
        /// <param name="o">input object</param>
        /// <param name="args">The arguments.</param>
        public static void PrintErrorLine(this object o, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(o.ToString().Fill(args));
        }


        /// <summary>
        /// Prints the specified string.
        /// </summary>
        /// <param name="o">Input string</param>
        /// <param name="args">The arguments.</param>
        // ReSharper disable InconsistentNaming
        public static void puts(this string o, params object[] args)
        // ReSharper restore InconsistentNaming
        {
            PrintLine(o, args);
        }

        /// <summary>
        /// Prints the specified string as Error.
        /// </summary>
        /// <param name="o">Input string</param>
        /// <param name="args">The arguments.</param>
        // ReSharper disable InconsistentNaming
        public static void puterr(this object o, params object[] args)
        // ReSharper restore InconsistentNaming
        {
            PrintError(o, args);
        }
#if NETFX4_5_1
        /// <summary>
        /// Writes the text asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static async Task WriteTextAsync(this string filePath, string text)
        {
            var encodedText = Encoding.Unicode.GetBytes(text);

            using (var sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }

        /// <summary>
        /// Writes the text asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="text">The text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static async Task WriteTextAsync(this string filePath, string text, Encoding encoding)
        {
            var encodedText = encoding.GetBytes(text);

            using (var sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }
#endif
    }
    /// <summary>
    /// XmlTextWriterFormattedNoDeclaration class
    /// </summary>
    internal class XmlTextWriterFormattedNoDeclaration : XmlTextWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTextWriterFormattedNoDeclaration" /> class.
        /// </summary>
        /// <param name="w">Text writer</param>
        /// <param name="indented">if set to <c>true</c> [indented].</param>
        public XmlTextWriterFormattedNoDeclaration(TextWriter w, bool indented = false)
            : base(w)
        {
            Formatting = indented ? Formatting.Indented : Formatting.None;
        }

        /// <summary>
        /// Writes the XML declaration with the version "1.0".
        /// </summary>
        public override void WriteStartDocument()
        {
            // removes the namespace
        }
    }

    /// <summary>
    /// helper class to ignore namespaces when de-serializing
    /// </summary>
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        /// <summary>
        /// Constructor which takes Textreader as argument
        /// </summary>
        /// <param name="reader"></param>
        public NamespaceIgnorantXmlTextReader(TextReader reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }
}

// ReSharper disable CheckNamespace
namespace CodeX.Core
// ReSharper restore CheckNamespace
{
    using System;
    using System.Linq;

    /// <summary>
    /// Static class containing core Extensions
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Combines the bite arrays.
        /// </summary>
        /// <param name="arrays">The arrays.</param>
        /// <returns>Byte Array.</returns>
        public static byte[] CombineByteArrays(params byte[][] arrays)
        {
            var ret = new byte[arrays.Sum(x => x.Length)];
            var offset = 0;
            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }

            return ret;
        }

        /// <summary>
        /// Converts string to <c>Enum</c>
        /// </summary>
        /// <typeparam name="T">Generic type of <c>Enum</c></typeparam>
        /// <param name="strval">String value</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignores case].</param>
        /// <returns>string type of <c>Enum</c></returns>
        public static T ToEnum<T>(this string strval, bool ignoreCase = true)
        {
            var genericType = typeof(T);
            return (T)Enum.Parse(genericType, strval, ignoreCase);
        }

        /// <summary>
        /// Ases the string.
        /// </summary>
        /// <param name="enum">The enum.</param>
        /// <returns>String as Enum</returns>
        public static string AsString(this Enum @enum)
        {
            return Enum.GetName(@enum.GetType(), @enum);
        }

        /// <summary>
        /// Checks if the key is in the list
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">key value</param>
        /// <param name="list">list ok values</param>
        /// <returns>
        /// true if the value is found else returns false
        /// </returns>
        /// <example>1.In(1,6,9,11) returns true</example>
        public static bool In<T>(this T key, params T[] list)
        {
            return list.Contains(key);
        }
    }

}