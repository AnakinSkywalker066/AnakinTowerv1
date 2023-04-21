using MelonLoader;
using starwarsmod;
using UnityEngine;
using System;
using System.Reflection;
using BTD_Mod_Helper;
using HarmonyLib;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity;
using System.IO;
using Il2CppSystem.Collections.Generic;
using static MelonLoader.MelonLogger;
using Il2CppAssets.Scripts.Unity.Cascade;
using Il2CppSystem.Runtime.Remoting;
using ApprovalUtilities.Reflection;
using Il2CppNewtonsoft.Json.Utilities;

[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace starwarsmod;
public class StarWars : BloonsTD6Mod 
{ 
public override void OnApplicationStart()
{

foreach (var asset in MelonAssembly.Assembly.GetManifestResourceNames())
MelonLogger.Msg(asset);
//previous two lines are for debugging/finding names of assets

assetBundle = AssetBundle.LoadFromMemory(ExtractResource("JediAnakin.bundle"));// if using unityexplorer, there is an error, but everything still works
ModHelper.Msg<StarWars>("Star Wars loaded!");
}

public static AssetBundle assetBundle;


private byte[] ExtractResource(String filename)
{
Assembly a = MelonAssembly.Assembly; // get the assembly
return a.GetEmbeddedResource(filename).GetByteArray(); // get the embedded bundle as a raw file that unity can read
}
}

public class Jedi : ModTower
{
    public static int GetTowerIndex(List<TowerDetailsModel> towerSet)
    {
        return towerSet.First(model => model.towerId == TowerType.GlueGunner).towerIndex + 1;
    }

    public override TowerSet TowerSet => TowerSet.Military;
    public override string BaseTower => TowerType.DartMonkey;
    public override string DisplayName => "AnakinSkywaylker";
    public override int Cost => 500;
    public override int TopPathUpgrades => 5;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;
    public override string Portrait => "2DAnakin";
    public override string Icon => "2DAnakin";
    public override string Description => "Anakin Uses the Force to Destory Bloons!";
    
    

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        
        towerModel.GetAttackModel().ApplyDisplay<JediKnightDisplay>();
        towerModel.GetAttackModel().weapons[0].projectile = Game.instance.model.GetTower(TowerType.BoomerangMonkey).GetAttackModel().weapons[0].projectile.Duplicate();
        towerModel.GetAttackModel().weapons[0].projectile.ApplyDisplay<Lightsaber>();
        towerModel.GetAttackModel().weapons[0].projectile.pierce = 100;
    }
}
