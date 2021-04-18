﻿// TcNo Account Switcher - A Super fast account switcher
// Copyright (C) 2019-2021 TechNobo (Wesley Pyburn)
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TcNo_Acc_Switcher_Globals;
using TcNo_Acc_Switcher_Server.Data;
using TcNo_Acc_Switcher_Server.Pages.General;

namespace TcNo_Acc_Switcher_Server.Pages.Steam
{
    public partial class Settings : ComponentBase
    {
        [Inject]
        public Data.AppData AppData { get; set; }
        private IJSObjectReference _jsModule;
        protected override async Task OnInitializedAsync()
        {
            AppData.WindowTitle = "TcNo Account Switcher - Steam Settings";
            Globals.DebugWriteLine($@"[Auto:Steam\Settings.razor.cs.OnInitializedAsync]");
        }
        
        #region SETTINGS_GENERAL
        // BUTTON: Pick Steam folder
        public async Task PickSteamFolder()
        {
            Globals.DebugWriteLine($@"[ButtonClicked:Steam\Settings.razor.cs.PickSteamFolder]");
            await JsRuntime.InvokeAsync<string>("ShowModal", "find:Steam:Steam.exe:SteamSettings");
        }

        // BUTTON: Check account VAC status
        public static async void ClearVacStatus()
        {
            Globals.DebugWriteLine($@"[ButtonClicked:Steam\Settings.razor.cs.ClearVacStatus]");
            if (SteamSwitcherFuncs.DeleteVacCacheFile())
                await GeneralInvocableFuncs.ShowToast("success", "VAC status for accounts was cleared", renderTo: "toastarea");
            else
                await GeneralInvocableFuncs.ShowToast("error", "Could not delete 'profilecache/SteamVACCache.json'", "Error", "toastarea");
        }

        // BUTTON: Reset settings
        public static void ClearSettings()
        {
            Globals.DebugWriteLine($@"[ButtonClicked:Steam\Settings.razor.cs.ClearSettings]");
            new Data.Settings.Steam().ResetSettings();
            AppData.ActiveNavMan.NavigateTo("/Steam?toast_type=success&toast_title=Success&toast_message=" + Uri.EscapeUriString("Cleared Steam switcher settings"));
        }

        // BUTTON: Reset images
        public static void ClearImages()
        {
            Globals.DebugWriteLine($@"[ButtonClicked:Steam\Settings.razor.cs.ClearImages]");
            SteamSwitcherFuncs.ClearImages(); // ADD A TOAST TO THIS
        }
        #endregion

        #region SETTINGS_STEAM_TOOLS
        // Restore forgotten accounts

        // BUTTON: Clear forgotten backups

        // BUTTON: Open Steam Folder
        public static void OpenSteamFolder()
        {
            Globals.DebugWriteLine($@"[ButtonClicked:Steam\Settings.razor.cs.OpenSteamFolder]");
            Process.Start("explorer.exe", new Data.Settings.Steam().FolderPath);
        }

        // BUTTON: Advanced Cleaning...
        // Handled on page

        #endregion

    }
}