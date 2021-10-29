using ATCBot.Structs;
using Discord;
using Discord.WebSocket;
using System;
using System.Linq;


namespace ATCBot.Commands
{
    class LobbyMods : Command
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Name { get; set; } = "lobbymods";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override SlashCommandBuilder Builder { get; set; } = new SlashCommandBuilder()
            .WithName("lobbymods")
            .WithDescription("Lists the mods currently loaded in the lobby")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("lobbyindex")
                .WithDescription("The position of the lobby in the list")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(true)
            );

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="command"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override string Action(SocketSlashCommand command)
        {
            if (Program.lobbyHandler.vtolLobbies.Count - LobbyHandler.PasswordedLobbies > 0)
            {
                int lobbyToFind = Convert.ToInt32(command.Data.Options.ElementAt(0).Value);

                //Needs to loop through incase the lobby has an invalid state
                int lobbyIndex = 1;
                foreach (VTOLLobby lobby in Program.lobbyHandler.vtolLobbies.Where(l => !l.PasswordProtected()))
                {
                    if (lobby.OwnerName == string.Empty || lobby.LobbyName == string.Empty || lobby.ScenarioName == string.Empty)
                    {
                        Log.LogWarning("Invalid lobby state!", "VTOL Embed Builder", true);
                        continue;
                    }

                    if (lobbyIndex == lobbyToFind)
                    {
                        return $"Lobby has {lobby.ModCount} loaded";
                    }
                    lobbyIndex++;
                }

                
            }

            return "Sorry, coudn't find that lobby";
        }

    }
}
