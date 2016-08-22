using System;

namespace DotaSharp
{
    [Flags]
    public enum ConVarFlags
    {
        // ConVar Systems
        FcvarUnregistered = 1 << 0, // If this is set, don't add to linked list,

        FcvarDevelopmentonly =
            1 << 1, // Hidden in released products. Flag is removed automatically if ALLOW_DEVELOPMENT_CVARS is defined.
        FcvarGamedll = 1 << 2, // defined by the game DLL
        FcvarClientdll = 1 << 3, // defined by the client DLL

        FcvarHidden =
            1 << 4, // Hidden. Doesn't appear in find or autocomplete. Like DEVELOPMENTONLY, but can't be compiled out.

        // ConVar only
        FcvarProtected =
            1 << 5, // It's a server cvar, but we don't send the data since it's a password, etc.  Sends 1 if it's not bland/zero, 0 otherwise as value
        FcvarSponly = 1 << 6, // This cvar cannot be changed by clients connected to a multiplayer server.
        FcvarArchive = 1 << 7, // set to cause it to be saved to vars.rc
        FcvarNotify = 1 << 8, // notifies players when changed
        FcvarUserinfo = 1 << 9, // changes the client's info string
        FcvarCheat = 1 << 14, // Only useable in singleplayer / debug / multiplayer & sv_cheats

        FcvarPrintableonly =
            1 << 10, // This cvar's string cannot contain unprintable characters ( e.g., used for player name etc ).

        FcvarUnlogged =
            1 << 11, // If this is a FCVAR_SERVER, don't log changes to the log file / console if we are creating a log
        FcvarNeverAsString = 1 << 12, // never try to print that cvar
        FcvarReplicated = 1 << 13, // server setting enforced on clients, TODO rename to FCAR_SERVER at some time
        FcvarDemo = 1 << 16, // record this cvar when starting a demo file
        FcvarDontrecord = 1 << 17, // don't record these command in demofiles
        FcvarNotConnected = 1 << 22, // cvar cannot be changed by a client that is connected to a server
        FcvarArchiveXbox = 1 << 24, // cvar written to config.cfg on the Xbox

        FcvarServerCanExecute =
            1 << 28, // the server is allowed to execute this command on clients via ClientCommand/NET_StringCmd/CBaseClientState::ProcessStringCmd.

        FcvarServerCannotQuery =
            1 << 29, // If this is set, then the server is not allowed to query this cvar's value (via IServerPluginHelpers::StartQueryCvarValue).
        FcvarClientcmdCanExecute = 1 << 30 // IVEngineClient::ClientCmd is allowed to execute this command
    }
}