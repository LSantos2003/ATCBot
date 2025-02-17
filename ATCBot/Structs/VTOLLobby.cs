using SteamKit2;

using System;
using System.Collections.Generic;

namespace ATCBot.Structs
{
    /// <summary>
    /// Represents a single VTOL VR lobby.
    /// </summary>
    public struct VTOLLobby
    {
        //Vanilla lobby info
        private string lobbyName;
        private string ownerName;
        private string ownerId;
        private string scenarioName;
        private string scenarioId;
        private string maxPlayers;
        private string feature;
        private string envIdx;
        private string gameVersion;
        private string briefingRoom;
        private string passwordHash;
        private int playerCount;

        //Modloader lobby info
        private string loadedMods;
        private string modCount;
        
        /// <summary>
        /// Represents the version status of a lobby.
        /// </summary>
        public enum FeatureType
        {
            /// <summary>Main branch</summary>
            f,
            /// <summary>Public testing branch</summary>
            p,
            /// <summary>Modded version</summary>
            m
        }

        /// <summary>
        /// Represents the time of day of a lobby.
        /// </summary>
        public enum EnvType
        {
            /// <summary />
            Day,
            /// <summary />
            Morning,
            /// <summary />
            Night
        }



        /// <summary>
        /// The name of the lobby.
        /// </summary>
        public string LobbyName { get => lobbyName; private set => lobbyName = value; }

        /// <summary>
        /// The name of the owner of the lobby.
        /// </summary>
        public string OwnerName { get => ownerName; private set => ownerName = value; }

        /// <summary>
        /// The Steam ID of the owner of the lobby.
        /// </summary>
        public string OwnerId { get => ownerId; private set => ownerId = value; }

        /// <summary>
        /// The name of the scenario of the lobby.
        /// </summary>
        public string ScenarioName { get => scenarioName; private set => scenarioName = value; }

        /// <summary>
        /// The ID of the scenario of the lobby.
        /// </summary>
        public string ScenarioId { get => scenarioId; private set => scenarioId = value; }

        /// <summary>
        /// The maximum number of players in the lobby.
        /// </summary>
        public int MaxPlayers { get => int.Parse(maxPlayers); private set => maxPlayers = value.ToString(); }

        /// <summary>
        /// The type of lobby - main branch, PTB, or modded.
        /// </summary>
        public FeatureType Feature { get => Enum.Parse<FeatureType>(feature); private set => feature = value.ToString(); }

        /// <summary>
        /// Whether it is day, morning, or night.
        /// </summary>
        public EnvType EnvIdx { get => (EnvType) int.Parse(envIdx); private set => envIdx = value.ToString(); }

        /// <summary>
        /// The version of the game the lobby is running.
        /// </summary>
        public string GameVersion { get => gameVersion; private set => gameVersion = value; }

        /// <summary>
        /// The type of briefing room being used.
        /// </summary>
        // TODO: enumerate types
        public int BriefingRoom { get => int.Parse(briefingRoom); private set => briefingRoom = value.ToString(); }

        /// <summary>
        /// The password hash of the lobby. 0 means it is public.
        /// </summary>
        public int PasswordHash { get => int.Parse(passwordHash); private set => passwordHash = value.ToString(); }

        /// <summary>
        /// The current amount of players in the lobby.
        /// </summary>
        public int PlayerCount { get => playerCount; private set => playerCount = value; }

        /// <summary>
        /// Whether or not this lobby is password protected.
        /// </summary>
        public bool PasswordProtected() => PasswordHash != 0;

        /// <summary>
        /// How many mods are loaded in the lobby.
        /// </summary>
        public string ModCount { get => $"{modCount} mod{(int.Parse(modCount) != 1 ? "s" : "")}"; private set => modCount = value; }


        /// <summary>
        /// Which mods are loaded in the lobby.
        /// </summary>
        public string LoadedMods { get => loadedMods; private set => loadedMods = value; }

        /// <summary>Create a lobby from a SteamKit2 lobby.</summary>
        public VTOLLobby(SteamMatchmaking.Lobby lobby)
        {
            if (lobby.Metadata.ContainsKey("name"))
            {
                Log.LogVerbose("Skipping modded lobby...", "VTOL VR Lobby Constructor");
                this = default;
                return;
            }
            if (!lobby.Metadata.ContainsKey("scn"))
            {
                Log.LogVerbose("Skipping incomplete lobby...", "VTOL VR Lobby Constructor");
                this = default;
                return;
            }

            List<string> badKeys = new();

            playerCount = lobby.NumMembers;


            if (!lobby.Metadata.TryGetValue("lName", out lobbyName))
                badKeys.Add("lName");

            if (!lobby.Metadata.TryGetValue("oName", out ownerName))
                badKeys.Add("oName");

            if (!lobby.Metadata.TryGetValue("oId", out ownerId))
                badKeys.Add("oId");

            if (!lobby.Metadata.TryGetValue("scn", out scenarioName))
                badKeys.Add("scn");

            if (!lobby.Metadata.TryGetValue("scID", out scenarioId))
                badKeys.Add("scID");

            if (!lobby.Metadata.TryGetValue("maxP", out maxPlayers))
                badKeys.Add("maxP");

            if (!lobby.Metadata.TryGetValue("feature", out feature))
                badKeys.Add("feature");

            if (!lobby.Metadata.TryGetValue("envIdx", out envIdx))
                badKeys.Add("envIdx");

            if (!lobby.Metadata.TryGetValue("ver", out gameVersion))
                badKeys.Add("ver");

            if (!lobby.Metadata.TryGetValue("brtype", out briefingRoom))
                badKeys.Add("brtype");

            if (!lobby.Metadata.TryGetValue("pwh", out passwordHash))
                badKeys.Add("pwh");

            if (!lobby.Metadata.TryGetValue("lModCount", out modCount) && Enum.Parse<FeatureType>(feature) == FeatureType.m)
                badKeys.Add("lModCount");

            if (!lobby.Metadata.TryGetValue("lMods", out loadedMods) && Enum.Parse<FeatureType>(feature) == FeatureType.m)
                badKeys.Add("lMods");





            if (badKeys.Count > 0)
            {
                Log.LogWarning($"One or more keys could not be set correctly! \"{string.Join(", ", badKeys.ToArray())}\"", "VTOL VR Lobby Constructor", true);
                this = default;
            }
            Log.LogDebug($"Found VTOL Lobby | Name: {LobbyName} , Owner: {OwnerName} , Scenario: {ScenarioName} , Players: {PlayerCount}/{MaxPlayers} , PP: {PasswordProtected()}",
                "VTOL VR Lobby Constructor");
        }
    }
}