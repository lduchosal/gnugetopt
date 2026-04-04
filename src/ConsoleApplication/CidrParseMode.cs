// <copyright file="CidrParseMode.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace ConsoleApplication
{
    /// <summary>
    /// CIDR parsing mode for network addresses.
    /// </summary>
    internal enum CidrParseMode
    {
        /// <summary>Use the default CIDR based on class (A/8, B/16, C/24).</summary>
        Default,

        /// <summary>Use a specified CIDR value.</summary>
        Value,
    }
}
