using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KraftWrapper.Extensions.Tests
{
    [TestClass]
    public class SitecoreIDExpensionsTests
    {
        [TestMethod]
        public void CheckConvertionToSitecoreIDString()
        {
            var giud = new Guid("00000000-aaaa-BBBB-cccc-000000001234");

            var result = giud.ToIDString();

            Assert.AreEqual("{00000000-AAAA-BBBB-CCCC-000000001234}", result);
        }
    }
}
