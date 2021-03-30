using BooleanRegisterUtilityAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityUnitTDD
{
    [TestClass]
    public class BLIFTDD
    {


        [TestMethod]
        public void Yo()
        {

            if (BL.If("1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1").TnE)
                Console.WriteLine("Yo");
        }
        [TestMethod]
        public void Yo2()
        {

            if (BL.If("1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1").TnE)
                Console.WriteLine("Yo2");
        }
        [TestMethod]
        public void Yo3()
        {

            if (BL.If("1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1").TnE)
                Console.WriteLine("Yo2");
        }
        [TestMethod]
        public void Yo4()
        {

            if (BL.If("1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1").TnE)
                Console.WriteLine("Yo2");
        }
    }
}
