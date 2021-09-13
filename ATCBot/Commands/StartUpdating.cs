﻿using Discord;
using Discord.WebSocket;

namespace ATCBot.Commands
{
    /// <summary>
    /// Command to start updating the lobby information.
    /// </summary>
    public class StartUpdating : Command
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Name { get; set; } = "startupdating";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override SlashCommandBuilder Builder { get; set; } = new SlashCommandBuilder()
            .WithName("startupdating")
            .WithDescription("Start updating the lobby data. Requires Manage Server permissions.");

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="command"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override string Action(SocketSlashCommand command)
        {
            if (command.User is SocketGuildUser u)
            {
                if (u.GuildPermissions.ManageGuild == true)
                {
                    if (Program.shouldUpdate)
                        return "Already updating!";
                    else
                    {
                        Program.shouldUpdate = true;
                        return "Started updating!";
                    }
                }
                else return "Sorry, you don't have enough permissions for this!";
            }
            else throw new System.Exception("Could not get user permissions!");
        }
    }
}
