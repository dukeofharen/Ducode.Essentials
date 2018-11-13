using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.CommonUtils.Tests
{
   [TestClass]
   public class ObjectUtilitiesFacts
   {
      [TestMethod]
      public void ObjectUtilities_Set_HappyFlow()
      {
         // arrange
         var list = new List<string>();

         // act
         list.Set(l => l.Add("test"));

         // assert
         Assert.AreEqual("test", list.Single());
      }
   }
}
