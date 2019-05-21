using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Console.Tests
{
    [TestClass]
    public class ArgsHelperFacts
    {
        [TestMethod]
        public void ArgsHelper_Parse_HappyFlow()
        {
            // arrange
            string argsText = "--arg1 value1 --arg2 value2";
            var args = argsText.Split(' ');

            // act
            var result = args.Parse();

            // assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("value1", result["arg1"]);
            Assert.AreEqual("value2", result["arg2"]);
        }

        [TestMethod]
        public void ArgsHelper_GetValue_ValueNotFound_ShouldReturnNull()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            string result = dictionary.GetValue("arg2");

            // assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ArgsHelper_GetValue_HappyFlow()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            string result = dictionary.GetValue("arg1");

            // assert
            Assert.AreEqual("value1", result);
        }

        [TestMethod]
        public void ArgsHelper_GetValueDefault_ValueNotFound_ShouldReturnDefault()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            string result = dictionary.GetValue("arg2", "default");

            // assert
            Assert.AreEqual("default", result);
        }

        [TestMethod]
        public void ArgsHelper_GetValueDefault_HappyFlow()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            string result = dictionary.GetValue("arg1", "default");

            // assert
            Assert.AreEqual("value1", result);
        }

        [TestMethod]
        public void ArgsHelper_GetAndSetValue_ValueNotFound_ShouldReturnDefault_ShouldAddDefaultValueToDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
             {
                { "arg1", "value1" }
             };

            // act
            string result = dictionary.GetAndSetValue("arg2", "default");

            // assert
            Assert.AreEqual("default", result);
            Assert.AreEqual("default", dictionary["arg2"]);
        }

        [TestMethod]
        public void ArgsHelper_GetAndSetValue_ValueFound_ShouldReturnValue_ShouldNotAddDefaultValueToDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            string result = dictionary.GetAndSetValue("arg1", "default");

            // assert
            Assert.AreEqual("value1", result);
            Assert.AreEqual("value1", dictionary["arg1"]);
        }

        [DataTestMethod]
        [DataRow("arg50", 0, 0)]
        [DataRow("arg1", 0, 0)]
        [DataRow("arg2", 0, 42)]
        public void ArgsHelper_GetValueInt(string key, int defaultValue, int expectedResult)
        {
            // act
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" },
            { "arg2", "42" }
         };

            // act
            int result = dictionary.GetValue(key, defaultValue);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ArgsHelper_GetAndSetValueInt_ValueNotFound_ShouldReturnDefault_ShouldAddDefaultValueToDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            int result = dictionary.GetAndSetValue("arg2", 42);

            // assert
            Assert.AreEqual(42, result);
            Assert.AreEqual("42", dictionary["arg2"]);
        }

        [TestMethod]
        public void ArgsHelper_GetAndSetValueInt_ValueFound_ShouldReturnValue_ShouldNotAddDefaultValueToDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" },
            { "arg2", "33" }
         };

            // act
            int result = dictionary.GetAndSetValue("arg2", 42);

            // assert
            Assert.AreEqual(33, result);
            Assert.AreEqual("33", dictionary["arg2"]);
        }

        [DataTestMethod]
        [DataRow("arg50", false, false)]
        [DataRow("arg1", false, false)]
        [DataRow("arg2", false, true)]
        [DataRow("arg3", false, true)]
        [DataRow("arg4", false, true)]
        [DataRow("arg5", false, false)]
        [DataRow("arg6", false, false)]
        [DataRow("arg7", false, false)]
        public void ArgsHelper_GetValueBool(string key, bool defaultValue, bool expectedResult)
        {
            // act
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" },
            { "arg2", "1" },
            { "arg3", "true" },
            { "arg4", "TRUE" },
            { "arg5", "0" },
            { "arg6", "false" },
            { "arg7", "FALSE" }
         };

            // act
            bool result = dictionary.GetValue(key, defaultValue);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ArgsHelper_GetAndSetValueBool_ValueNotFound_ShouldReturnDefault_ShouldAddDefaultValueToDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" }
         };

            // act
            bool result = dictionary.GetAndSetValue("arg2", true);

            // assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("True", dictionary["arg2"]);
        }

        [TestMethod]
        public void ArgsHelper_GetAndSetValueBool_ValueFound_ShouldReturnValue_ShouldNotAddDefaultValueToDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string>
         {
            { "arg1", "value1" },
            { "arg2", "false" }
         };

            // act
            bool result = dictionary.GetAndSetValue("arg2", true);

            // assert
            Assert.AreEqual(false, result);
            Assert.AreEqual("false", dictionary["arg2"]);
        }
    }
}
