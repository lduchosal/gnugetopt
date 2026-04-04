// <copyright file="ProgramContext.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace ConsoleApplication
{
    using System;

    /// <summary>
    /// Holds the parsed command-line options and state for the program.
    /// </summary>
    public sealed class ProgramContext
    {
        /// <summary>Gets or sets a value indicating whether to print the IP network.</summary>
        public bool IPNetwork { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the network address.</summary>
        public bool Network { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the netmask.</summary>
        public bool Netmask { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the CIDR notation.</summary>
        public bool Cidr { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the broadcast address.</summary>
        public bool Broadcast { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the first usable address.</summary>
        public bool FirstUsable { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the last usable address.</summary>
        public bool LastUsable { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the number of usable addresses.</summary>
        public bool Usable { get; set; }

        /// <summary>Gets or sets a value indicating whether to print the total number of addresses.</summary>
        public bool Total { get; set; }

        /// <summary>Gets or sets the CIDR parsing mode.</summary>
        public CidrParseMode CidrParse { get; set; } = CidrParseMode.Value;

        /// <summary>Gets or sets the parsed CIDR value.</summary>
        public byte CidrParsed { get; set; } = 32;

        /// <summary>Gets or sets the network to check containment against.</summary>
        public string? ContainNetwork { get; set; }

        /// <summary>Gets or sets the network to check overlap against.</summary>
        public string? OverlapNetwork { get; set; }

        /// <summary>Gets or sets the network to subtract.</summary>
        public string? SubtractNetwork { get; set; }

        /// <summary>Gets or sets the action to perform.</summary>
        public ProgramAction Action { get; set; } = ProgramAction.PrintNetworks;

        /// <summary>Gets or sets the CIDR for subnet splitting.</summary>
        public byte SubnetCidr { get; set; }

        /// <summary>Gets or sets the raw network strings from the command line.</summary>
        public string[] NetworksString { get; set; } = Array.Empty<string>();

        /// <summary>Gets or sets the parsed network strings.</summary>
        public string[] Networks { get; set; } = Array.Empty<string>();
    }
}
