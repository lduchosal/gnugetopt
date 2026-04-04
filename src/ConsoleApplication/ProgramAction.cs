// <copyright file="ProgramAction.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace ConsoleApplication
{
    /// <summary>
    /// Available actions the program can perform.
    /// </summary>
    public enum ProgramAction
    {
        /// <summary>Print usage information.</summary>
        Usage,

        /// <summary>Print network details.</summary>
        PrintNetworks,

        /// <summary>Split a network into subnets.</summary>
        Subnet,

        /// <summary>Supernet networks into smallest possible subnets.</summary>
        Supernet,

        /// <summary>Supernet networks into one single subnet.</summary>
        WideSupernet,

        /// <summary>List all IP addresses in networks.</summary>
        ListIPAddress,

        /// <summary>Check if a network contains other networks.</summary>
        ContainNetwork,

        /// <summary>Check if a network overlaps other networks.</summary>
        OverlapNetwork,

        /// <summary>Subtract a network from a subnet.</summary>
        SubtractNetwork,
    }
}
