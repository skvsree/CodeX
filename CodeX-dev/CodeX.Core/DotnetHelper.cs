using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

// ReSharper disable CheckNamespace
namespace CodeX
// ReSharper restore CheckNamespace
{

    /// <summary>
    /// Extension Base Dot net classes and datatypes
    /// </summary>
    public static class DotnetHelper
    {


        /// <summary>
        /// To the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        /// <returns>System.String.</returns>
        //public static string ToJson<T>(this object o)
        //{
        //    //System.Web.Script.Serialization
        //    //return json = new Jnew JavaScriptSerializer().Serialize(o);
        //    return null;
        //}




        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        private static string SerializeToString<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>``0.</returns>
        private static T DeserializeFromString<T>(string data)
        {
            var b = Convert.FromBase64String(data);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }


        /// <summary>
        /// Combines the bite arrays.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <param name="third">The third.</param>
        /// <returns>System.Byte[][].</returns>
        public static byte[] CombineBiteArrays(byte[] first, byte[] second, byte[] third)
        {
            var ret = new byte[first.Length + second.Length + third.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            Buffer.BlockCopy(third, 0, ret, first.Length + second.Length,
                             third.Length);
            return ret;
        }










        /// <summary>
        /// Upserts the key and value to specified dictionary.  This means if the key already exists it is updated if not it will be inserted
        /// </summary>
        /// <typeparam name="TKey">Generic type of the key.</typeparam>
        /// <typeparam name="TValue">Generic type of the value.</typeparam>
        /// <param name="dictionary"> dictionary.</param>
        /// <param name="key">key.</param>
        /// <param name="value">value.</param>
        public static void Upsert<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.Keys.Contains(key))
            {
                dictionary[key] = value;
                return;
            }
            dictionary.Add(key, value);
        }



    }

}
