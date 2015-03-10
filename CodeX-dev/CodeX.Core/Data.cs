// ReSharper disable CheckNamespace
namespace CodeX.Data
// ReSharper restore CheckNamespace
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Contains code Extensions for DataBase and Data structure
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Validates the object.
        /// </summary>
        /// <param name="o">Dynamic Object</param>
        /// <returns>
        /// Validation results
        /// </returns>
        /// <example><code> 
        /// public class Person
        /// {
        ///    [Range(5, 50)]
        ///    public int Age { get; set; }
        ///    public string Name { get; set; }
        /// }
        /// Person p = new Person{Age = 30, Name = "Stark"};
        /// p.Validate();
        /// </code></example>
        public static List<ValidationResult> Validate(this object o)
        {
            var ctx = new ValidationContext(o, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(o, ctx, results, true);
            return results;
        }

        /// <summary>
        /// Validates the specified Property of the given object.
        /// </summary>
        /// <param name="o">Object</param>
        /// <param name="property">The property.</param>
        /// <param name="propertyname">The propertyname.</param>
        /// <returns></returns>
        public static List<ValidationResult> Validate(this object o, object property, string propertyname)
        {
            var ctx = new ValidationContext(o, null, null);
            ctx.MemberName = propertyname;
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(property, ctx, results);
            return results;
        }

        /// <summary>
        /// Finds the next.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <param name="start">The start index.</param>
        /// <returns>
        /// Enumerable Index
        /// </returns>
        /// <example><code>
        /// public void TestFindNextIndexOf()
        /// {
        /// var testArray = new[] { 1, 2, 3, 1, 3, 4, 5, 7 };
        /// foreach (var item in testArray.FindNextIndex(1))
        /// {
        ///    //Do Something
        /// }
        /// }
        /// </code></example>
        public static IEnumerable<int> FindNextIndex<T>(this T[] array, T value, int start = 0) where T : IComparable
        {
            var retIndex = start;

            retIndex = Array.IndexOf(array, value, retIndex);
            while (retIndex != -1)
            {
                yield return retIndex;
                retIndex = Array.IndexOf(array, value, retIndex + 1);
            }
        }

        /// <summary>
        /// Matches the given list with other list and returns the key and matched count.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="thislist">Current List </param>
        /// <param name="otherList">List to be Matched with</param>
        /// <returns>The key item and count of item in other list <see>
        ///                                                           <cref>System.Collections.Generic.IDictionary</cref>
        ///                                                       </see>
        /// </returns>
        /// <example><code>
        ///    var array1 = new[] { 'a', 'b', 'b', 'c', 'd', 'e' };
        ///    var array2 = new[] { 'a', 'b', 'c', 'c' };
        ///    var retDict = array1.Match(array2);
        /// </code></example>
        public static IDictionary<T, int> Match<T>(this IEnumerable<T> thislist, IEnumerable<T> otherList) where T : IComparable
        {
            var returnDict = new Dictionary<T, int>();
            var other = otherList.ToList();

            foreach (var key in thislist.Where(key => !returnDict.Keys.Contains(key)))
            {
                returnDict.Add(key, other.Count(x => x.Equals(key)));
            }

            return returnDict;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        /// <param name="addEvenIfDuplicate">if set to <c>true</c> [add even if duplicate].</param>
        /// <example>
        /// <code>
        /// var sampleList = new Dictionary&lt;string, IEnumerable&lt;string&gt;&gt;
        ///        {
        ///            {"Jedi", new List&lt;string&gt;{"Yoda"}},
        ///            {"Sid", new List&lt;string&gt;{"Siddius"}}
        ///        };
        ///    sampleList.AddRange("Jedi", "Yoda");
        /// </code> </example>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, IEnumerable<TValue>> dictionary, TKey key, IEnumerable<TValue> values, bool addEvenIfDuplicate = true)
        {
            if (dictionary.ContainsKey(key))
            {
                var listOfValues = dictionary[key].ToList();
                if (addEvenIfDuplicate)
                {
                    listOfValues.AddRange(values);
                }
                else
                {
                    foreach (var value in values.Where(value => !listOfValues.Contains(value)))
                    {
                        listOfValues.Add(value);
                    }
                }

                dictionary[key] = listOfValues;
            }
            else
            {
                dictionary.Add(key, values);
            }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="addEvenIfDuplicate">if set to <c>true</c> [add even if duplicate].</param>
        /// <example>
        /// <code>
        /// var sampleList = new Dictionary&lt;string, IEnumerable&lt;string&gt;&gt;
        ///        {
        ///            {"Jedi", new List&lt;string&gt;{"Yoda"}},
        ///            {"Sid", new List&lt;string&gt;{"Siddius"}}
        ///        };
        ///    sampleList.AddRange("Jedi", new List&lt;string&gt;{ "Yoda","Yoda"});
        /// </code> </example>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, IEnumerable<TValue>> dictionary, TKey key, TValue value, bool addEvenIfDuplicate = true)
        {
            AddRange(dictionary, key, new List<TValue> { value }, addEvenIfDuplicate);
        }
    }
}