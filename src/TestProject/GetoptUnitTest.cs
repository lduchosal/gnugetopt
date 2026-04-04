// <copyright file="GetoptUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    using System.Text;
    using Gnu.Getopt;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the Gnu.Getopt library core functionality.
    /// </summary>
    [TestClass]
    public class GetoptUnitTest
    {
        private static readonly string[] ShortOptionsNoArgsArgs = ["-a", "-b", "-c"];
        private static readonly string[] CombinedShortOptionsArgs = ["-abc"];
        private static readonly string[] ShortOptionRequiredArgArgs = ["-o", "file.txt"];
        private static readonly string[] ShortOptionRequiredArgAttachedArgs = ["-ofile.txt"];
        private static readonly string[] ShortOptionOptionalArgPresentArgs = ["-ofile.txt"];
        private static readonly string[] ShortOptionOptionalArgAbsentArgs = ["-o"];
        private static readonly string[] InvalidOptionArgs = ["-z"];
        private static readonly string[] MissingRequiredArgWithColonPrefixArgs = ["-o"];
        private static readonly string[] DoubleDashStopsScanningArgs = ["-a", "--", "-b"];
        private static readonly string[] OpterrSuppressesErrorsArgs = ["-z"];
        private static readonly string[] OptstringGetterSetterArgs = ["-a"];
        private static readonly string[] LongOptionBasicArgs = ["--help"];
        private static readonly string[] LongOptionRequiredArgEqualsArgs = ["--output=file.txt"];
        private static readonly string[] LongOptionRequiredArgNextElementArgs = ["--output", "file.txt"];
        private static readonly string[] LongOptionWithFlagArgs = ["--verbose"];
        private static readonly string[] LongOptionOptionalArgPresentArgs = ["--level=5"];
        private static readonly string[] LongOptionOptionalArgAbsentArgs = ["--level"];
        private static readonly string[] InvalidLongOptionArgs = ["--invalid"];
        private static readonly string[] LongOnlyModeArgs = ["-help"];
        private static readonly string[] ConstructorWithLongOptsNoShortArgs = ["--verbose"];
        private static readonly string[] MultipleOptionsWithArgsArgs = ["-a", "-b", "val", "-c"];
        private static readonly string[] OptindSetterArgs = ["-a", "-b"];
        private static readonly string[] MissingRequiredArgReturnsQuestionMarkArgs = ["-o"];
        private static readonly string[] AmbiguousLongOptionArgs = ["--ver"];
        private static readonly string[] LongOptionMissingRequiredArgArgs = ["--output"];

        /// <summary>Tests parsing short options without arguments.</summary>
        [TestMethod]
        public void ShortOptionsNoArgs()
        {
            var g = new Getopt("test", ShortOptionsNoArgsArgs, "abc");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual('b', g.getopt());
            Assert.AreEqual('c', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests parsing combined short options.</summary>
        [TestMethod]
        public void CombinedShortOptions()
        {
            var g = new Getopt("test", CombinedShortOptionsArgs, "abc");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual('b', g.getopt());
            Assert.AreEqual('c', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests short option with required argument.</summary>
        [TestMethod]
        public void ShortOptionRequiredArg()
        {
            var g = new Getopt("test", ShortOptionRequiredArgArgs, "o:");
            Assert.AreEqual('o', g.getopt());
            Assert.AreEqual("file.txt", g.Optarg);
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests short option with required argument attached.</summary>
        [TestMethod]
        public void ShortOptionRequiredArgAttached()
        {
            var g = new Getopt("test", ShortOptionRequiredArgAttachedArgs, "o:");
            Assert.AreEqual('o', g.getopt());
            Assert.AreEqual("file.txt", g.Optarg);
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests short option with optional argument present.</summary>
        [TestMethod]
        public void ShortOptionOptionalArgPresent()
        {
            var g = new Getopt("test", ShortOptionOptionalArgPresentArgs, "o::");
            Assert.AreEqual('o', g.getopt());
            Assert.AreEqual("file.txt", g.Optarg);
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests short option with optional argument absent.</summary>
        [TestMethod]
        public void ShortOptionOptionalArgAbsent()
        {
            var g = new Getopt("test", ShortOptionOptionalArgAbsentArgs, "o::");
            Assert.AreEqual('o', g.getopt());
            Assert.IsNull(g.Optarg);
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests invalid option returns '?'.</summary>
        [TestMethod]
        public void InvalidOptionReturnsQuestionMark()
        {
            var g = new Getopt("test", InvalidOptionArgs, "abc") { Opterr = false };
            Assert.AreEqual('?', g.getopt());
            Assert.AreEqual('z', g.Optopt);
        }

        /// <summary>Tests missing required argument with colon prefix.</summary>
        [TestMethod]
        public void MissingRequiredArgWithColonPrefix()
        {
            var g = new Getopt("test", MissingRequiredArgWithColonPrefixArgs, ":o:") { Opterr = false };
            Assert.AreEqual(':', g.getopt());
        }

        /// <summary>Tests double-dash stops scanning.</summary>
        [TestMethod]
        public void DoubleDashStopsScanning()
        {
            var g = new Getopt("test", DoubleDashStopsScanningArgs, "ab");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual(-1, g.getopt());
            Assert.AreEqual(2, g.Optind);
        }

        /// <summary>Tests non-option arguments are at the end.</summary>
        [TestMethod]
        public void NonOptionArgsAtEnd()
        {
            string[] args = { "-a", "foo", "-b" };
            var g = new Getopt("test", args, "ab");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual('b', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests POSIX mode stops at first non-option.</summary>
        [TestMethod]
        public void PosixModeStopsAtFirstNonOption()
        {
            string[] args = { "-a", "foo", "-b" };
            var g = new Getopt("test", args, "+ab");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests return in order mode.</summary>
        [TestMethod]
        public void ReturnInOrderMode()
        {
            string[] args = { "-a", "foo", "-b" };
            var g = new Getopt("test", args, "-ab");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual(1, g.getopt());
            Assert.AreEqual("foo", g.Optarg);
            Assert.AreEqual('b', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests Optind property after parsing.</summary>
        [TestMethod]
        public void OptindAfterParsing()
        {
            string[] args = { "-a", "file1", "file2" };
            var g = new Getopt("test", args, "a");
            while (g.getopt() != -1)
            {
            }

            Assert.AreEqual(1, g.Optind);
        }

        /// <summary>Tests Opterr property suppresses error output.</summary>
        [TestMethod]
        public void OpterrSuppressesErrors()
        {
            var g = new Getopt("test", OpterrSuppressesErrorsArgs, "a");
            Assert.IsTrue(g.Opterr);
            g.Opterr = false;
            Assert.IsFalse(g.Opterr);
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests empty args returns -1.</summary>
        [TestMethod]
        public void EmptyArgs()
        {
            var g = new Getopt("test", System.Array.Empty<string>(), "abc");
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests Optstring property getter and setter.</summary>
        [TestMethod]
        public void OptstringGetterSetter()
        {
            var g = new Getopt("test", OptstringGetterSetterArgs, "a");
            Assert.AreEqual("a", g.Optstring);
            g.Optstring = "ab";
            Assert.AreEqual("ab", g.Optstring);
        }

        /// <summary>Tests Argv property getter and setter.</summary>
        [TestMethod]
        public void ArgvGetterSetter()
        {
            string[] args = { "-a" };
            var g = new Getopt("test", args, "a");
            Assert.AreSame(args, g.Argv);

            string[] newArgs = { "-b" };
            g.Argv = newArgs;
            Assert.AreSame(newArgs, g.Argv);
        }

        /// <summary>Tests long option basic parsing.</summary>
        [TestMethod]
        public void LongOptionBasic()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
                new("output", Argument.Required, null!, 'o'),
            };

            var g = new Getopt("test", LongOptionBasicArgs, "ho:", longopts);
            Assert.AreEqual('h', g.getopt());
            Assert.AreEqual(0, g.Longind);
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests long option with required argument using equals.</summary>
        [TestMethod]
        public void LongOptionRequiredArgEquals()
        {
            var longopts = new LongOpt[]
            {
                new("output", Argument.Required, null!, 'o'),
            };

            var g = new Getopt("test", LongOptionRequiredArgEqualsArgs, "o:", longopts);
            Assert.AreEqual('o', g.getopt());
            Assert.AreEqual("file.txt", g.Optarg);
        }

        /// <summary>Tests long option with required argument in next element.</summary>
        [TestMethod]
        public void LongOptionRequiredArgNextElement()
        {
            var longopts = new LongOpt[]
            {
                new("output", Argument.Required, null!, 'o'),
            };

            var g = new Getopt("test", LongOptionRequiredArgNextElementArgs, "o:", longopts);
            Assert.AreEqual('o', g.getopt());
            Assert.AreEqual("file.txt", g.Optarg);
        }

        /// <summary>Tests long option with flag returns 0.</summary>
        [TestMethod]
        public void LongOptionWithFlag()
        {
            var sb = new StringBuilder();
            var longopts = new LongOpt[]
            {
                new("verbose", Argument.No, sb, 'v'),
            };

            var g = new Getopt("test", LongOptionWithFlagArgs, "", longopts);
            Assert.AreEqual(0, g.getopt());
            Assert.AreEqual("118", sb.ToString());
        }

        /// <summary>Tests long option with optional argument present.</summary>
        [TestMethod]
        public void LongOptionOptionalArgPresent()
        {
            var longopts = new LongOpt[]
            {
                new("level", Argument.Optional, null!, 'l'),
            };

            var g = new Getopt("test", LongOptionOptionalArgPresentArgs, "l::", longopts);
            Assert.AreEqual('l', g.getopt());
            Assert.AreEqual("5", g.Optarg);
        }

        /// <summary>Tests long option with optional argument absent.</summary>
        [TestMethod]
        public void LongOptionOptionalArgAbsent()
        {
            var longopts = new LongOpt[]
            {
                new("level", Argument.Optional, null!, 'l'),
            };

            var g = new Getopt("test", LongOptionOptionalArgAbsentArgs, "l::", longopts);
            Assert.AreEqual('l', g.getopt());
            Assert.IsNull(g.Optarg);
        }

        /// <summary>Tests invalid long option returns '?'.</summary>
        [TestMethod]
        public void InvalidLongOption()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            var g = new Getopt("test", InvalidLongOptionArgs, "h", longopts) { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests long-only mode allows single dash for long options.</summary>
        [TestMethod]
        public void LongOnlyMode()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            var g = new Getopt("test", LongOnlyModeArgs, "h", longopts, true);
            Assert.AreEqual('h', g.getopt());
        }

        /// <summary>Tests LongOpt properties.</summary>
        [TestMethod]
        public void LongOptProperties()
        {
            var sb = new StringBuilder();
            var opt = new LongOpt("test", Argument.Required, sb, 42);
            Assert.AreEqual("test", opt.Name);
            Assert.AreEqual(Argument.Required, opt.HasArg);
            Assert.AreSame(sb, opt.Flag);
            Assert.AreEqual(42, opt.Val);
        }

        /// <summary>Tests LongOpt with null flag.</summary>
        [TestMethod]
        public void LongOptNullFlag()
        {
            var opt = new LongOpt("test", Argument.No, null!, 'x');
            Assert.IsNull(opt.Flag);
        }

        /// <summary>Tests LongOpt invalid argument type throws.</summary>
        [TestMethod]
        public void LongOptInvalidArgument()
        {
            bool threw = false;
            try
            {
                _ = new LongOpt("test", (Argument)99, null!, 'x');
            }
            catch
            {
                threw = true;
            }

            Assert.IsTrue(threw);
        }

        /// <summary>Tests Digest produces option string from long opts.</summary>
        [TestMethod]
        public void DigestLongOpts()
        {
            var longopts = new LongOpt[]
            {
                new("all", Argument.No, null!, 'a'),
                new("output", Argument.Required, null!, 'o'),
                new("level", Argument.Optional, null!, 'l'),
            };

            string result = Getopt.Digest(longopts);
            Assert.AreEqual("ao:l::", result);
        }

        /// <summary>Tests constructor with long options and no short options.</summary>
        [TestMethod]
        public void ConstructorWithLongOptsNoShort()
        {
            var longopts = new LongOpt[]
            {
                new("verbose", Argument.No, null!, 'v'),
            };

            var g = new Getopt("test", ConstructorWithLongOptsNoShortArgs, "", longopts);
            Assert.AreEqual('v', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests multiple short options with arguments.</summary>
        [TestMethod]
        public void MultipleOptionsWithArgs()
        {
            var g = new Getopt("test", MultipleOptionsWithArgsArgs, "ab:c");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual('b', g.getopt());
            Assert.AreEqual("val", g.Optarg);
            Assert.AreEqual('c', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests Optind setter works.</summary>
        [TestMethod]
        public void OptindSetter()
        {
            var g = new Getopt("test", OptindSetterArgs, "ab") { Optind = 1 };
            Assert.AreEqual(1, g.Optind);
        }

        /// <summary>Tests missing required argument returns '?' by default.</summary>
        [TestMethod]
        public void MissingRequiredArgReturnsQuestionMark()
        {
            var g = new Getopt("test", MissingRequiredArgReturnsQuestionMarkArgs, "o:") { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests ambiguous long option.</summary>
        [TestMethod]
        public void AmbiguousLongOption()
        {
            var longopts = new LongOpt[]
            {
                new("verbose", Argument.No, null!, 'v'),
                new("version", Argument.No, null!, 'V'),
            };

            var g = new Getopt("test", AmbiguousLongOptionArgs, "vV", longopts) { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests long option missing required argument.</summary>
        [TestMethod]
        public void LongOptionMissingRequiredArg()
        {
            var longopts = new LongOpt[]
            {
                new("output", Argument.Required, null!, 'o'),
            };

            var g = new Getopt("test", LongOptionMissingRequiredArgArgs, "o:", longopts) { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests invalid option with Opterr true prints error.</summary>
        [TestMethod]
        public void InvalidOptionWithOpterrTrue()
        {
            var g = new Getopt("test", InvalidOptionArgs, "abc");
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests long option with Opterr true prints error on ambiguous.</summary>
        [TestMethod]
        public void AmbiguousLongOptionWithOpterrTrue()
        {
            var longopts = new LongOpt[]
            {
                new("verbose", Argument.No, null!, 'v'),
                new("version", Argument.No, null!, 'V'),
            };

            var g = new Getopt("test", AmbiguousLongOptionArgs, "vV", longopts);
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests long option with unexpected argument using equals.</summary>
        [TestMethod]
        public void LongOptionUnexpectedArgEquals()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "--help=foo" };
            var g = new Getopt("test", args, "h", longopts);
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests permutation with mixed options and non-options.</summary>
        [TestMethod]
        public void PermutationMixedOptionsAndNonOptions()
        {
            string[] args = { "foo", "-a", "bar", "-b", "baz" };
            var g = new Getopt("test", args, "ab");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual('b', g.getopt());
            Assert.AreEqual(-1, g.getopt());
            Assert.IsTrue(g.Optind <= args.Length);
        }

        /// <summary>Tests POSIX W; convenience for long options.</summary>
        [TestMethod]
        public void PosixWConvenienceLongOption()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "-W", "help" };
            var g = new Getopt("test", args, "W;", longopts);
            Assert.AreEqual('h', g.getopt());
        }

        /// <summary>Tests POSIX W; with invalid long option name returns W.</summary>
        [TestMethod]
        public void PosixWInvalidLongOptionReturnsW()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "-W", "invalid" };
            var g = new Getopt("test", args, "W;", longopts) { Opterr = false };
            Assert.AreEqual('W', g.getopt());
        }

        /// <summary>Tests POSIX W; with no argument.</summary>
        [TestMethod]
        public void PosixWMissingArg()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "-W" };
            var g = new Getopt("test", args, "W;", longopts) { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests long option with -- prefix and Opterr true on missing arg.</summary>
        [TestMethod]
        public void LongOptionMissingRequiredArgWithOpterr()
        {
            var longopts = new LongOpt[]
            {
                new("output", Argument.Required, null!, 'o'),
            };

            string[] args = { "--output" };
            var g = new Getopt("test", args, "o:", longopts);
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests long option with single-dash prefix in longOnly mode.</summary>
        [TestMethod]
        public void LongOptionSingleDashLongOnly()
        {
            var longopts = new LongOpt[]
            {
                new("verbose", Argument.No, null!, 'v'),
            };

            string[] args = { "-verbose" };
            var g = new Getopt("test", args, "a", longopts, true);
            Assert.AreEqual('v', g.getopt());
        }

        /// <summary>Tests unrecognized long option with -- prefix and Opterr.</summary>
        [TestMethod]
        public void UnrecognizedLongOptionDoubleDash()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "--unknown" };
            var g = new Getopt("test", args, "h", longopts);
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests unrecognized long option with single dash in longOnly mode.</summary>
        [TestMethod]
        public void UnrecognizedLongOptionSingleDash()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "-unknown" };
            var g = new Getopt("test", args, "h", longopts, true);
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests empty optstring gets padded.</summary>
        [TestMethod]
        public void EmptyOptstring()
        {
            string[] args = { "-a" };
            var g = new Getopt("test", args, "") { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }

        /// <summary>Tests Digest with empty array.</summary>
        [TestMethod]
        public void DigestEmptyArray()
        {
            string result = Getopt.Digest(System.Array.Empty<LongOpt>());
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>Tests constructor with 3 params (no long options).</summary>
        [TestMethod]
        public void ConstructorThreeParams()
        {
            string[] args = { "-a" };
            var g = new Getopt("test", args, "a");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests constructor with 4 params (long options, no longOnly).</summary>
        [TestMethod]
        public void ConstructorFourParams()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "--help" };
            var g = new Getopt("test", args, "h", longopts);
            Assert.AreEqual('h', g.getopt());
        }

        /// <summary>Tests long option abbreviation resolves uniquely.</summary>
        [TestMethod]
        public void LongOptionAbbreviation()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
                new("output", Argument.Required, null!, 'o'),
            };

            string[] args = { "--ou=test.txt" };
            var g = new Getopt("test", args, "ho:", longopts);
            Assert.AreEqual('o', g.getopt());
            Assert.AreEqual("test.txt", g.Optarg);
        }

        /// <summary>Tests double dash after non-options with Exchange.</summary>
        [TestMethod]
        public void DoubleDashAfterNonOptions()
        {
            string[] args = { "foo", "-a", "--", "bar" };
            var g = new Getopt("test", args, "a");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests calling getopt after endparse returns -1.</summary>
        [TestMethod]
        public void GetoptAfterEndparse()
        {
            string[] args = { "-a" };
            var g = new Getopt("test", args, "a");
            Assert.AreEqual('a', g.getopt());
            Assert.AreEqual(-1, g.getopt());
            Assert.AreEqual(-1, g.getopt());
        }

        /// <summary>Tests long option with no-argument receiving unexpected equals arg with Opterr false.</summary>
        [TestMethod]
        public void LongOptionUnexpectedArgWithOpterrFalse()
        {
            var longopts = new LongOpt[]
            {
                new("help", Argument.No, null!, 'h'),
            };

            string[] args = { "--help=foo" };
            var g = new Getopt("test", args, "h", longopts) { Opterr = false };
            Assert.AreEqual('?', g.getopt());
        }
    }
}
