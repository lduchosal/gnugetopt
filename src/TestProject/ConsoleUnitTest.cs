// <copyright file="ConsoleUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the ConsoleApplication command-line parsing.
    /// </summary>
    [TestClass]
    public class ConsoleUnitTest
    {
        private static readonly string[] TestMethod1Args = new[] { "10.0.0.0/8" };
        private static readonly string[] TestMethod2Args = new[] { "-Nnmcbflu", "10.0.0.0/8" };
        private static readonly string[] TestMethod3Args = new[] { "-N", "-n", "-m", "-c", "-b", "-f", "-l", "-u", "10.0.0.0/8" };
        private static readonly string[] TestMethod4Args = new[] { "10.0.0.0" };
        private static readonly string[] TestMethod5Args = new[] { "-s", "16", "10.0.0.0/8" };
        private static readonly string[] TestMethod6Args = new[] { "-d", "24", "-x", "-S", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24", "1.1.1.1" };
        private static readonly string[] TestMethod7Args = new[] { "-i", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24", "1.1.1.1" };
        private static readonly string[] TestMethod8Args = new[] { "-d", "24", "-w", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24", "1.1.1.1" };
        private static readonly string[] TestMethod9Args = new[] { ":" };
        private static readonly string[] TestMethod10Args = new[] { "-C", "10.0.0.0/8", "1.1.1.1" };
        private static readonly string[] TestMethod11Args = new[] { "-o", "10.0.0.0/8", "1.1.1.1" };

        /// <summary>Tests parsing a single CIDR network.</summary>
        [TestMethod]
        public void TestMethod1()
        {
            ConsoleApplication.Program.Main(TestMethod1Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests combined short flags with a network.</summary>
        [TestMethod]
        public void TestMethod2()
        {
            ConsoleApplication.Program.Main(TestMethod2Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests individual short flags with a network.</summary>
        [TestMethod]
        public void TestMethod3()
        {
            ConsoleApplication.Program.Main(TestMethod3Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests parsing a bare IP address without CIDR.</summary>
        [TestMethod]
        public void TestMethod4()
        {
            ConsoleApplication.Program.Main(TestMethod4Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests the subnet split action.</summary>
        [TestMethod]
        public void TestMethod5()
        {
            ConsoleApplication.Program.Main(TestMethod5Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests CIDR override with list IP and subtract actions.</summary>
        [TestMethod]
        public void TestMethod6()
        {
            ConsoleApplication.Program.Main(TestMethod6Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests the IP network flag with multiple networks.</summary>
        [TestMethod]
        public void TestMethod7()
        {
            ConsoleApplication.Program.Main(TestMethod7Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests the supernet action with multiple networks.</summary>
        [TestMethod]
        public void TestMethod8()
        {
            ConsoleApplication.Program.Main(TestMethod8Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests parsing an invalid input.</summary>
        [TestMethod]
        public void TestMethod9()
        {
            ConsoleApplication.Program.Main(TestMethod9Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests the contain network action.</summary>
        [TestMethod]
        public void TestMethod10()
        {
            ConsoleApplication.Program.Main(TestMethod10Args);
            Assert.IsTrue(true);
        }

        /// <summary>Tests the overlap network action.</summary>
        [TestMethod]
        public void TestMethod11()
        {
            ConsoleApplication.Program.Main(TestMethod11Args);
            Assert.IsTrue(true);
        }
    }
}
