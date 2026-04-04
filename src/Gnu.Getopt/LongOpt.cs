// <copyright file="LongOpt.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

/**************************************************************************
/* LongOpt.cs -- C#.NET port of Long option object for Getopt
/*
/* Copyright (c) 1998 by Aaron M. Renn (arenn@urbanophile.com)
/* C#.NET Port Copyright (c) 2004 by Klaus Pr�ckl (klaus.prueckl@aon.at)
/*
/* This program is free software; you can redistribute it and/or modify
/* it under the terms of the GNU Library General Public License as published
/* by  the Free Software Foundation; either version 2 of the License or
/* (at your option) any later version.
/*
/* This program is distributed in the hope that it will be useful, but
/* WITHOUT ANY WARRANTY; without even the implied warranty of
/* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/* GNU Library General Public License for more details.
/*
/* You should have received a copy of the GNU Library General Public License
/* along with this program; see the file COPYING.LIB.  If not, write to
/* the Free Software Foundation Inc., 59 Temple Place - Suite 330,
/* Boston, MA  02111-1307 USA
/**************************************************************************/

namespace Gnu.Getopt
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using System.Text;

    /// <summary>
    /// Constant enumeration values used for the LongOpt <c>hasArg</c>
    /// constructor argument.
    /// </summary>
    public enum Argument
    {
        /// <summary>
        /// This value indicates that the option takes no argument.
        /// </summary>
        No = 0,

        /// <summary>
        /// This value indicates that the option takes an argument that is
        /// required.
        /// </summary>
        Required = 1,

        /// <summary>
        /// This value indicates that the option takes an argument that is
        /// optional.
        /// </summary>
        Optional = 2,
    }

    /// <summary>
    /// This object represents the definition of a long option in the C# port
    /// of GNU getopt. An array of LongOpt objects is passed to the
    /// <see cref="Getopt"/> object to define the list of valid long options
    /// for a given parsing session. Refer to the <see cref="Getopt"/>
    /// documentation for details on the format of long options.
    /// </summary>
    /// <seealso cref="Getopt">Getopt.</seealso>
    /// <author>Aaron M. Renn (arenn@urbanophile.com).</author>
    /// <author>Klaus Pr�ckl (klaus.prueckl@aon.at).</author>
    public class LongOpt
    {
        /// <summary>
        /// The localized strings are kept in the resources, which can be
        /// accessed by the <see cref="ResourceManager"/> class.
        /// </summary>
        private readonly ResourceManager resManager = new(
            "Gnu.Getopt.MessagesBundle", typeof(LongOpt).GetTypeInfo().Assembly);

        /// <summary>
        /// The current UI culture (set to en-US when posixly correctness is
        /// enabled).
        /// </summary>
        private readonly CultureInfo cultureInfo = CultureInfo.CurrentUICulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="LongOpt"/> class.
        /// Create a new LongOpt object with the given parameter values. If the
        /// value passed as <paramref name="hasArg"/> is not valid, then an
        /// <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <param name="name">
        /// The long option string.
        /// </param>
        /// <param name="hasArg">
        /// Indicates whether the option has no argument
        /// (<see cref="Argument.No"/>), a required argument
        /// (<see cref="Argument.Required"/>) or an optional argument
        /// (<see cref="Argument.Optional"/>).
        /// </param>
        /// <param name="flag">
        /// If non-null, this is a location to store the value of
        /// <paramref name="val"/> when this option is encountered, otherwise
        /// <paramref name="val"/> is treated as the equivalent short option
        /// character.
        /// </param>
        /// <param name="val">
        /// The value to return for this long option, or the equivalent single
        /// letter option to emulate if flag is null.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Is thrown if the <paramref name="hasArg"/> param is not one of
        /// <see cref="Argument.No"/>, <see cref="Argument.Required"/> or
        /// <see cref="Argument.Optional"/>.
        /// </exception>
        public LongOpt(
            string name,
            Argument hasArg,
            StringBuilder flag,
            int val)
        {
            // Check for application setting "Gnu.PosixlyCorrect" to determine
            // whether to strictly follow the POSIX standard. This replaces the
            // "POSIXLY_CORRECT" environment variable in the C version
            try
            {
                if (true)
                {
                    this.cultureInfo = new CultureInfo("en-US");
                }
            }
            catch (Exception)
            {
                // Culture initialization is best-effort; fall through to validation below.
            }

            // Validate hasArg
            if (hasArg is not Argument.No and not Argument.Required and
                not Argument.Optional)
            {
                object[] msgArgs = new object[] { hasArg };
                throw new System.ArgumentException(string.Format(
                    CultureInfo.InvariantCulture,
                    this.resManager.GetString(
                        "getopt.invalidValue",
                        this.cultureInfo), msgArgs));
            }

            // Store off values
            this.Name = name;
            this.HasArg = hasArg;
            this.Flag = flag;
            this.Val = val;
        }

        /// <summary>
        /// Gets the name of this LongOpt as a string.
        /// </summary>
        /// <returns>
        /// The name of the long option.
        /// </returns>
        public string Name { get; }

        /// <summary>
        /// Gets the value set for the <c>hasArg</c> field for this long
        /// option.
        /// </summary>
        /// <returns>
        /// The value of <c>hasArg</c>.
        /// </returns>
        public Argument HasArg { get; }

        /// <summary>
        /// Gets the value of the <c>flag</c> field for this long option.
        /// </summary>
        /// <returns>
        /// The value of <c>flag</c>.
        /// </returns>
        public StringBuilder Flag { get; }

        /// <summary>
        /// Gets the value of the <c>val</c> field for this long option.
        /// </summary>
        /// <returns>
        /// The value of <c>val</c>.
        /// </returns>
        public int Val { get; }
    } // Class LongOpt
}
