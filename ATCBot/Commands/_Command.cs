﻿using Discord;
using Discord.WebSocket;

using System.Collections.Generic;

namespace ATCBot.Commands
{
    /// <summary>
    /// Base class for slash commands to inherit from.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// A list of all commands found through using Reflection.
        /// </summary>
        public static List<Command> AllCommands { get; set; } = new();

        /// <summary>
        /// The name of the command.
        /// </summary>
        /// <remarks>MUST match <cref>Builder.Name</cref>.</remarks>
        public abstract string Name { get; set; }

        /// <summary>
        /// The slash command builder used to create the command. Must be initialized at declaration.
        /// </summary>
        public abstract SlashCommandBuilder Builder { get; set; }

        /// <summary>
        /// The action to take when this command is invoked.
        /// </summary>
        /// <param name="command">The context of the command when invoked.</param>
        /// <returns>A string to output back to the user who invoked the command.</returns>
        public abstract string Action(SocketSlashCommand command);
    }
}
