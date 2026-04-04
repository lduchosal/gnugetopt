// <copyright file="Program.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using Gnu.Getopt;

    /// <summary>
    /// IP network manipulation tool demonstrating GNU getopt usage.
    /// </summary>
    public static class Program
    {
        private const string UnableToParseMessage = "Unable to parse ipnetwork {0}";

        private static readonly Dictionary<int, ArgParsed> Args = new();
        private static readonly ArgParsed[] ArgsList = new[]
        {
            new ArgParsed('i', (ac, arg) => ac.IPNetwork = true),
            new ArgParsed('n', (ac, arg) => ac.Network = true),
            new ArgParsed('m', (ac, arg) => ac.Netmask = true),
            new ArgParsed('c', (ac, arg) => ac.Cidr = true),
            new ArgParsed('b', (ac, arg) => ac.Broadcast = true),
            new ArgParsed('f', (ac, arg) => ac.FirstUsable = true),
            new ArgParsed('l', (ac, arg) => ac.LastUsable = true),
            new ArgParsed('u', (ac, arg) => ac.Usable = true),
            new ArgParsed('t', (ac, arg) => ac.Total = true),
            new ArgParsed('w', (ac, arg) => ac.Action = ProgramAction.Supernet),
            new ArgParsed('W', (ac, arg) => ac.Action = ProgramAction.WideSupernet),
            new ArgParsed('h', (ac, arg) => ac.Action = ProgramAction.Usage),
            new ArgParsed('x', (ac, arg) => ac.Action = ProgramAction.ListIPAddress),
            new ArgParsed('?', (ac, arg) => _ = arg),
            new ArgParsed('D', (ac, arg) => ac.CidrParse = CidrParseMode.Default),
            new ArgParsed('d', (ac, arg) =>
            {
                ac.CidrParse = CidrParseMode.Value;
                ac.CidrParsed = (byte)8;
            }),
            new ArgParsed('s', (ac, arg) =>
            {
                ac.Action = ProgramAction.Subnet;
                ac.SubnetCidr = (byte)9;
            }),
            new ArgParsed('C', (ac, arg) =>
            {
                if (!Program.TryParseIPNetwork(ac.CidrParse, out string? ipnetwork))
                {
                    Console.WriteLine(UnableToParseMessage, arg);
                    ac.Action = ProgramAction.Usage;
                    return;
                }

                ac.Action = ProgramAction.ContainNetwork;
                ac.ContainNetwork = ipnetwork;
            }),
            new ArgParsed('o', (ac, arg) =>
            {
                if (!Program.TryParseIPNetwork(ac.CidrParse, out string? ipnetwork))
                {
                    Console.WriteLine(UnableToParseMessage, arg);
                    ac.Action = ProgramAction.Usage;
                    return;
                }

                ac.Action = ProgramAction.OverlapNetwork;
                ac.OverlapNetwork = ipnetwork;
            }),
            new ArgParsed('S', (ac, arg) =>
            {
                if (!Program.TryParseIPNetwork(ac.CidrParse, out string? ipnetwork))
                {
                    Console.WriteLine(UnableToParseMessage, arg);
                    ac.Action = ProgramAction.Usage;
                    return;
                }

                ac.Action = ProgramAction.SubtractNetwork;
                ac.SubtractNetwork = ipnetwork;
            }),
        };

        static Program()
        {
            foreach (ArgParsed ap in Program.ArgsList)
            {
                Program.Args.Add(ap.Arg, ap);
            }
        }

        /// <summary>
        /// Entry point for the console application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            ProgramContext ac = Program.ParseArgs(args);

            if (ac.Action == ProgramAction.Subnet)
            {
                Program.SubnetNetworks(ac);
            }
            else if (ac.Action == ProgramAction.Supernet)
            {
                Program.SupernetNetworks(ac);
            }
            else if (ac.Action == ProgramAction.WideSupernet)
            {
                Program.WideSupernetNetworks(ac);
            }
            else if (ac.Action == ProgramAction.PrintNetworks)
            {
                Program.PrintNetworks(ac);
            }
            else if (ac.Action == ProgramAction.ContainNetwork)
            {
                Program.ContainNetwork(ac);
            }
            else if (ac.Action == ProgramAction.OverlapNetwork)
            {
                Program.OverlapNetwork(ac);
            }
            else if (ac.Action == ProgramAction.ListIPAddress)
            {
                Program.ListIPAddress(ac);
            }
            else
            {
                Program.Usage();
            }
        }

        private static void ListIPAddress(ProgramContext ac)
        {
            Console.WriteLine("ListIPAddress {0}", ac.Networks.ToString());
        }

        private static void ContainNetwork(ProgramContext ac)
        {
            Console.WriteLine("ContainNetwork {0}", ac.Networks.ToString());
        }

        private static void OverlapNetwork(ProgramContext ac)
        {
            Console.WriteLine("OverlapNetwork {0}", ac.Networks.ToString());
        }

        private static void WideSupernetNetworks(ProgramContext ac)
        {
            Console.WriteLine("WideSupernetNetworks {0}", ac.Networks.ToString());
        }

        private static void SupernetNetworks(ProgramContext ac)
        {
            Console.WriteLine("SupernetNetworks {0}", ac.Networks.ToString());
        }

        private static void SubnetNetworks(ProgramContext ac)
        {
            Console.WriteLine("SubnetNetworks {0}", ac.Networks.ToString());
        }

        private static void PrintNetworks(ProgramContext ac)
        {
            Console.WriteLine("PrintNetworks {0}", ac.Networks.ToString());
        }

        private static ProgramContext ParseArgs(string[] args)
        {
            int c;
            Getopt g = new Getopt("ipnetwork", args, "inmcbfltud:Dhs:wWxC:o:S:");
            ProgramContext ac = new ProgramContext();

            while ((c = g.getopt()) != -1)
            {
                string? optArg = g.Optarg;
                Program.Args[c].Run(ac, optArg);
            }

            List<string> ipnetworks = new List<string>();
            for (int i = g.Optind; i < args.Length; i++)
            {
                if (!string.IsNullOrEmpty(args[i]))
                {
                    ipnetworks.Add(args[i]);
                }
            }

            ac.NetworksString = ipnetworks.ToArray();
            Program.ParseIPNetworks(ac);

            if (ac.Networks.Length == 0)
            {
                Console.WriteLine("Provide at least one ipnetwork");
                ac.Action = ProgramAction.Usage;
            }

            if (ac.Action == ProgramAction.Supernet
                && ipnetworks.Count < 2)
            {
                Console.WriteLine("Supernet action required at least two ipnetworks");
                ac.Action = ProgramAction.Usage;
            }

            if (ac.Action == ProgramAction.WideSupernet
                && ipnetworks.Count < 2)
            {
                Console.WriteLine("WideSupernet action required at least two ipnetworks");
                ac.Action = ProgramAction.Usage;
            }

            if (Program.PrintNoValue(ac))
            {
                Program.PrintAll(ac);
            }

            if (g.Optind == 0)
            {
                Program.PrintAll(ac);
            }

            return ac;
        }

        private static void ParseIPNetworks(ProgramContext ac)
        {
            List<string> ipnetworks = new List<string>();
            foreach (string ips in ac.NetworksString)
            {
                if (!Program.TryParseIPNetwork(ac.CidrParse, out string? ipnetwork))
                {
                    Console.WriteLine(UnableToParseMessage, ips);
                    continue;
                }

                ipnetworks.Add(ipnetwork!);
            }

            ac.Networks = ipnetworks.ToArray();
        }

        private static bool TryParseIPNetwork(CidrParseMode cidrParseEnum, out string? ipn)
        {
            if (cidrParseEnum == CidrParseMode.Default)
            {
                ipn = "1.1.1.1/8";
                return true;
            }
            else if (cidrParseEnum == CidrParseMode.Value)
            {
                ipn = "1.1.1.1/9";
                return true;
            }

            ipn = null;
            return true;
        }

        private static bool PrintNoValue(ProgramContext ac)
        {
            ArgumentNullException.ThrowIfNull(ac);

            return !ac.IPNetwork
                  && !ac.Network
                  && !ac.Netmask
                  && !ac.Cidr
                  && !ac.Broadcast
                  && !ac.FirstUsable
                  && !ac.LastUsable
                  && !ac.Total
                  && !ac.Usable;
        }

        private static void PrintAll(ProgramContext ac)
        {
            ArgumentNullException.ThrowIfNull(ac);

            ac.IPNetwork = true;
            ac.Network = true;
            ac.Netmask = true;
            ac.Cidr = true;
            ac.Broadcast = true;
            ac.FirstUsable = true;
            ac.LastUsable = true;
            ac.Usable = true;
            ac.Total = true;
        }

        private static void Usage()
        {
            Assembly assembly = Assembly.GetEntryAssembly()
                ?? Assembly.GetExecutingAssembly()
                ?? Assembly.GetCallingAssembly()
                ;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string? version = fvi.FileVersion;

            Console.WriteLine("Usage: ipnetwork [-inmcbflu] [-d cidr|-D] [-h|-s cidr|-S|-w|-W|-x|-C network|-o network] networks ...");
            Console.WriteLine("Version: {0}", version);
            Console.WriteLine();
            Console.WriteLine("Print options");
            Console.WriteLine("\t-i : network");
            Console.WriteLine("\t-n : network address");
            Console.WriteLine("\t-m : netmask");
            Console.WriteLine("\t-c : cidr");
            Console.WriteLine("\t-b : broadcast");
            Console.WriteLine("\t-f : first usable ip address");
            Console.WriteLine("\t-l : last usable ip address");
            Console.WriteLine("\t-u : number of usable ip addresses");
            Console.WriteLine("\t-t : total number of ip addresses");
            Console.WriteLine();
            Console.WriteLine("Parse options");
            Console.WriteLine("\t-d cidr : use cidr if not provided (default /32)");
            Console.WriteLine("\t-D      : IPv4 only - use default cidr (ClassA/8, ClassB/16, ClassC/24)");
            Console.WriteLine();
            Console.WriteLine("Actions");
            Console.WriteLine("\t-h         : help message");
            Console.WriteLine("\t-s cidr    : split network into cidr subnets");
            Console.WriteLine("\t-w         : supernet networks into smallest possible subnets");
            Console.WriteLine("\t-W         : supernet networks into one single subnet");
            Console.WriteLine("\t-x         : list all ipaddresses in networks");
            Console.WriteLine("\t-C network : network contain networks");
            Console.WriteLine("\t-o network : network overlap networks");
            Console.WriteLine("\t-S network : subtract network from subnet");
            Console.WriteLine(string.Empty);
            Console.WriteLine("networks  : one or more network addresses ");
            Console.WriteLine("            (1.2.3.4 10.0.0.0/8 10.0.0.0/255.0.0.0 2001:db8::/32 2001:db8:1:2:3:4:5:6/128 )");
        }
    }
}
