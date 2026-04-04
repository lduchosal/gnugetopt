// <copyright file="ArgParsed.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace ConsoleApplication
{
    /// <summary>
    /// Maps a parsed option character to a callback handler.
    /// </summary>
    public sealed class ArgParsed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgParsed"/> class.
        /// </summary>
        /// <param name="arg">The option character code.</param>
        /// <param name="onArgParsed">The handler to invoke when this option is parsed.</param>
        public ArgParsed(int arg, ArgParsedHandler onArgParsed)
        {
            this.Arg = arg;
            this.OnArgParsed += onArgParsed;
        }

        /// <summary>
        /// Delegate for handling a parsed argument.
        /// </summary>
        /// <param name="ac">The program context.</param>
        /// <param name="arg">The option argument value, if any.</param>
        public delegate void ArgParsedHandler(ProgramContext ac, string? arg);

        private event ArgParsedHandler OnArgParsed;

        /// <summary>Gets or sets the option character code.</summary>
        public int Arg { get; set; }

        /// <summary>
        /// Invokes the registered handler for this option.
        /// </summary>
        /// <param name="ac">The program context.</param>
        /// <param name="arg">The option argument value, if any.</param>
        public void Run(ProgramContext ac, string? arg)
        {
            this.OnArgParsed?.Invoke(ac, arg);
        }
    }
}
