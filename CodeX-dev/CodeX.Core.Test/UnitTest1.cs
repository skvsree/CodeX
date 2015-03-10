using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CodeX.AOP;
using CodeX.Core.Test.Annotations;
using CodeX.Date;
using CodeX.Strings;
using CodeX.Data;
using CodeX.IO;
using CodeX.AOP.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeX.Core.Test
{
    interface IPerson : IAOP
    {
        int Age { get; set; }

        string Name { get; set; }

        void Echo();
    }

    [Serializable]
    public class Person : IPerson
    {
        [ValidateOnSet]
        [Range(5, 50)]
        [NotifyOnPropertyChanged]
        public int Age { get; set; }

        [StringLength(3)]
        public string Name { get; set; }

        public void Echo()
        {
            MessageBox.Show("HelloWorld");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum Colors
    {
        Red,
        Green,
        Blue
    }

    [TestClass]
    public class UnitTest1
    {
        const string Tempdir = "temp";
        private readonly string[] _arrayFileContent = new[]
                {
                    "1,2,3,5,6,7,8,9",
                    "2,1,3,5,7,12,,2",
                    "4,5,7,8,9,10,1,3"
                };

        private readonly string[] _namedFiledContent = new[]
                {
                   "FirstName,LastName,Age",
                   "Anakin,Skywalker,20",
                   "Tony,Stark,30",
                   "Bruce,Banner,29",
                   "Steve,Rogers,45"
                };

        [TestMethod]
        public void TestGenerateUserPasswordWithoutCharacterPreference()
        {
            Assert.IsTrue(Security.GeneratePassword(8).Length == 8);
        }

        [TestMethod]
        public void TestGenerateUserPasswordWithCharacterPreference()
        {
            const string prefChar = "abcdefgh";
            var retVal = Security.GeneratePassword(8, prefChar);
            Assert.IsTrue(retVal.Length == 8);
            if (retVal.ToCharArray().Any(c => !prefChar.Contains(c.ToString(CultureInfo.InvariantCulture))))
            {
                Assert.Fail("Unexpected value character in the return value");
            }
        }

        [TestMethod]
        public void TestToXml()
        {
            var person = new Person
                {
                    Age = 20,
                    Name = "Skywalker"
                };
            Assert.AreEqual(person.ToXml<Person>(), "<Person><Age>20</Age><Name>Skywalker</Name></Person>");
        }

        [TestMethod]
        public void TestToObject()
        {   
            const string xml = "<Person><Age>20</Age><Name>Skywalker</Name></Person>";
            var person = xml.ToObject<Person>();
            Assert.AreEqual(person.Age, 20);
            Assert.AreEqual(person.Name, "Skywalker");
        }

        [TestMethod]
        public void TestRightDataToValidate()
        {
            var person = new Person
            {
                Age = 20,
                Name = "Red"
            };
            var results = person.Validate();
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void TestWrongDataToValidate()
        {
            var person = new Person
            {
                Age = 2,
                Name = "Skywalker"
            };
            var results = person.Validate();
            Assert.IsFalse(results.Count == 0);
        }

        [TestMethod]
        public void TestFill()
        {
            const string testString = "Hello {0}";
            Assert.AreEqual(testString.Fill("World"), "Hello World");
        }

        [TestMethod]
        public void TestGetFiles()
        {
            var dir = GetTempDirectory();
            File.WriteAllText("{0}\\File.txt".Fill(dir.FullName), "Test");
            File.WriteAllText("{0}\\File.log".Fill(dir.FullName), "Test");
            Assert.IsTrue(dir.GetFiles("File.txt|File.log".Split('|').AsEnumerable(), SearchOption.AllDirectories).Count() == 2);
        }

        [TestMethod]
        public void TestEncryptDecrypt()
        {
            const string testString = "Testing Encryption";
            Assert.AreEqual(testString.Encrypt("password").Decrypt("password"), testString);
        }

        [TestMethod]
        public void TestMatchList()
        {
            var array1 = new[] { 'a', 'b', 'b', 'c', 'd', 'e' };
            var array2 = new[] { 'a', 'b', 'c', 'c' };
            var retDict = array1.Match(array2);
            Assert.IsTrue(retDict['a'] == 1 &&
                            retDict['b'] == 1 &&
                            retDict['c'] == 2 &&
                            retDict['d'] == 0 &&
                            retDict['e'] == 0);
        }

        [TestMethod]
        public void TestIn()
        {
            var array1 = new[] { 'a', 'b', 'b', 'c', 'd', 'e' };
            Assert.IsTrue('a'.In(array1) && !'z'.In(array1));
        }

        [TestMethod]
        public void TestSortDescendingByColumnIndex()
        {
            var dir = GetTempDirectory();
            _arrayFileContent.WriteToFile("{0}\\FileToBeSorted.txt".Fill(dir.FullName));
            var temp = "{0}\\FileToBeSorted.txt".Fill(dir.FullName).Sort(3, ListSortDirection.Descending);
            Assert.IsTrue(temp.First().Equals("4,5,7,8,9,10,1,3"));
        }

        [TestMethod]
        public void TestSortDescendingByColumnName()
        {

            var dir = GetTempDirectory();
            _namedFiledContent.WriteToFile("{0}\\FileToBeSorted.txt".Fill(dir.FullName));
            var temp = "{0}\\FileToBeSorted.txt".Fill(dir.FullName).Sort("FirstName", ListSortDirection.Descending);
            Assert.IsTrue(temp.Skip(1).First().Equals("Tony,Stark,30"));
        }


        [TestMethod]
        public void TestSortAscendingByColumnIndex()
        {
            var dir = GetTempDirectory();
            _arrayFileContent.WriteToFile("{0}\\FileToBeSorted.txt".Fill(dir.FullName));
            var temp = "{0}\\FileToBeSorted.txt".Fill(dir.FullName).Sort(3, ListSortDirection.Ascending);
            Assert.IsTrue(temp.First().Equals("1,2,3,5,6,7,8,9"));
        }

        [TestMethod]
        public void TestSortAscendingByColumnName()
        {

            var dir = GetTempDirectory();
            _namedFiledContent.WriteToFile("{0}\\FileToBeSorted.txt".Fill(dir.FullName));
            var temp = "{0}\\FileToBeSorted.txt".Fill(dir.FullName).Sort("FirstName", ListSortDirection.Ascending);
            Assert.IsTrue(temp.Skip(1).First().Equals("Anakin,Skywalker,20"));
        }

        [TestMethod]
        public void TestToEnumWithCase()
        {
            Assert.IsTrue("Blue".ToEnum<Colors>() == Colors.Blue);
        }

        [TestMethod]
        public void TestToEnumWithoutCase()
        {
            Assert.IsTrue("blue".ToEnum<Colors>() == Colors.Blue);
        }

        [TestMethod]
        public void TestMonthBasedDateFormat()
        {
            Assert.IsTrue(1.Jan(2010).Equals(new DateTime(2010, 1, 1)));
            Assert.IsTrue(1.Feb(2010).Equals(new DateTime(2010, 2, 1)));
            Assert.IsTrue(1.Mar(2010).Equals(new DateTime(2010, 3, 1)));
            Assert.IsTrue(1.Apr(2010).Equals(new DateTime(2010, 4, 1)));
            Assert.IsTrue(1.May(2010).Equals(new DateTime(2010, 5, 1)));
            Assert.IsTrue(1.Jun(2010).Equals(new DateTime(2010, 6, 1)));
            Assert.IsTrue(1.Jul(2010).Equals(new DateTime(2010, 7, 1)));
            Assert.IsTrue(1.Aug(2010).Equals(new DateTime(2010, 8, 1)));
            Assert.IsTrue(1.Sep(2010).Equals(new DateTime(2010, 9, 1)));
            Assert.IsTrue(1.Oct(2010).Equals(new DateTime(2010, 10, 1)));
            Assert.IsTrue(1.Nov(2010).Equals(new DateTime(2010, 11, 1)));
            Assert.IsTrue(1.Dec(2010).Equals(new DateTime(2010, 12, 1)));
        }

        [TestMethod]
        public void TestFindNextIndexOf()
        {
            var testArray = new[] { 1, 2, 3, 1, 3, 4, 5, 7 };
            const int n = 1;
            var expectedValue = testArray.Count(x => x.Equals(n));
            var countOfn = testArray.FindNextIndex(1).Count(item => testArray[item].Equals(n));
            Assert.AreEqual(expectedValue, countOfn);
        }

        [TestMethod]
        public void TestIsLeapYear()
        {
            Assert.IsTrue(1996.IsLeapYear());
            Assert.IsTrue(2000.IsLeapYear());
            Assert.IsFalse(1990.IsLeapYear());
            Assert.IsFalse(1997.IsLeapYear());
        }

        [TestMethod]
        public void TestAsFile()
        {
            GetTempDirectory().GetFiles().ToList().ForEach(x => x.Delete());
            var newfile = "{0}.log".Fill(Guid.NewGuid());
            Assert.IsTrue(newfile.AsFile(true).Exists);
        }

        [TestMethod]
        public void TestAsFolder()
        {
            Assert.AreEqual("{0}\\{1}".Fill(Environment.CurrentDirectory, Tempdir).AsFolder().GetType(), typeof(DirectoryInfo));
        }

        [TestMethod]
        public void TestEncoding()
        {
            var ascii = "This string contains the unicode character Pi(\u03c0)".ToUTF8();
            var utf8 = "This string contains the unicode character Pi(π)".ToUTF8();
            var utf16 = "This string contains the unicode character Pi(π)".ToUTF32();
            var utf7 = "This string contains the unicode character Pi(π)".ToUTF7();
            var bigendian = "This string contains the unicode character Pi(π)".ToBigEndianUnicode();
            var unicode = "This string contains the unicode character Pi(π)".ToUnicode();
            Assert.AreEqual(ascii, utf16);
            Assert.AreEqual(ascii, utf7);
            Assert.AreEqual(ascii, utf8);
            Assert.AreEqual(ascii, unicode);
            Assert.AreEqual(ascii, bigendian);
        }

        [TestMethod]
        public void TestIsNullOrEmpty()
        {
            string nullstring = null;
            Assert.IsTrue(string.Empty.IsNullOrEmpty());
            Assert.IsTrue(string.Empty.IsNullOrWhiteSpace());
            Assert.IsTrue(nullstring.IsNullOrEmpty());
            Assert.IsTrue(nullstring.IsNullOrWhiteSpace());
            Assert.IsTrue("   ".IsNullOrWhiteSpace());
            Assert.IsFalse("Iam not empty".IsNullOrWhiteSpace());
        }

        [TestMethod]
        public void TestAddRange()
        {
            var sampleList = new Dictionary<string, IEnumerable<string>>
                {
                    {"Jedi", new List<string>{"Yoda"}},
                    {"Sid", new List<string>{"Siddius"}}
                };
            sampleList.AddRange("Jedi", "Yoda");
            Assert.IsTrue(sampleList["Jedi"].Count().Equals(2));
            sampleList.AddRange("Jedi", "Yoda", false);
            Assert.IsTrue(sampleList["Jedi"].Count().Equals(2));
        }

        [TestMethod]
        public void TestAddRangeForEnumerable()
        {
            var sampleList = new Dictionary<string, IEnumerable<string>>
                {
                    {"Jedi", new List<string>{"Yoda"}},
                    {"Sid", new List<string>{"Siddius"}}
                };
            sampleList.AddRange("Jedi", new List<string> { "Yoda", "Yoda" });
            Assert.IsTrue(sampleList["Jedi"].Count().Equals(3));
            sampleList.AddRange("Jedi", new List<string> { "Yoda", "Yoda" }, false);
            Assert.IsTrue(sampleList["Jedi"].Count().Equals(3));
        }

        [TestMethod]
        public void TestToDateTime()
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            Assert.AreEqual(date, date.ToDateTime().ToString("dd/MM/yyyy"));
        }

        [TestMethod]
        public void TestBinarySerialization()
        {
            var person = new Person
            {
                Age = 20,
                Name = "Skywalker"
            };
            var cloned = person.ToBinary().ToObject<Person>();
            Assert.AreEqual(person.Age, cloned.Age);
            Assert.AreEqual(person.Name, cloned.Name);
        }

        [TestMethod]
        public void TestAddRangeByte()
        {
            var byte1 = "Hello ".GetBytes();
            var byte2 = "World".GetBytes();
            var byte3 = byte1.AddRange(byte2);
            var output = byte3.GetString();
            Assert.AreEqual("Hello World", output);
        }

        [TestMethod]
        public void TestToDataTable()
        {
            GetTempDirectory().GetFiles().ToList().ForEach(x => x.Delete());
            var newfile = "{0}.log".Fill(Guid.NewGuid());
            _arrayFileContent.WriteToFile(newfile);
            var table = newfile.AsFile().ToDataTable();
            Assert.IsTrue(table.Columns.Count == 8);
            Assert.IsTrue(table.Rows.Count == 2);
            table.WriteToFile(newfile, "|");
            table = newfile.AsFile().ToDataTable("|");
            Assert.IsTrue(table.Columns.Count == 8);
            Assert.IsTrue(table.Rows.Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"The field Age must be between 5 and 50.")]
        public void TestDeputy()
        {
            var c = Deputy<IPerson>.Create(new Person { Age = 13, Name = "Scott" });
            c.PropertyChanged += (s, e) => Assert.AreEqual(e.PropertyName, "Age");
            c.Age = 51;
        }

        [TestMethod]
        public void TestPuts()
        {
            "hello world".puts();
        }

        private static DirectoryInfo GetTempDirectory()
        {
            Directory.CreateDirectory("{0}\\{1}".Fill(Environment.CurrentDirectory, Tempdir));
            return new DirectoryInfo("{0}\\{1}".Fill(Environment.CurrentDirectory, Tempdir));
        }


    }
}

