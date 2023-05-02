using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using Il2Cpp;
using Il2CppAssets.Scripts.Data.MapSets;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppTMPro;
using MelonLoader;
using starwarsmod;
using StarWarsMod;
using UnityEngine;

[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace StarWarsMod;
public class StarWars : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<StarWars>("Star Wars Mod Has Fully Loaded");
        ModHelper.Msg<StarWars>("This Mod Has Alot So it May Take A Second To Load.");
    }

    
}

    

