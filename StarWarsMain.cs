using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppSystem;
using UnityEngine;
using starwarsmod;
using System.Reflection;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using uObject = UnityEngine.Object;

[assembly: MelonInfo(typeof(starwarsmod.StarWarsMain), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace starwarsmod;
public class StarWarsMain : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {

        foreach (var asset in MelonAssembly.Assembly.GetManifestResourceNames())
            MelonLogger.Msg(asset);
        //previous two lines are for debugging/finding names of assets

        assetBundle = AssetBundle.LoadFromMemory(ExtractResource("anakin.bundle"));// if using unityexplorer, there is an error, but everything still works
        ModHelper.Msg<StarWarsMain>("The Jedi Master loaded!");
    }

    public static AssetBundle assetBundle;


    private byte[] ExtractResource(String filename)
    {
        Assembly a = MelonAssembly.Assembly; // get the assembly
        return a.GetEmbeddedResource(filename).GetByteArray(); // get the embedded bundle as a raw file that unity can read
    }
}

public class AnakinSkywalker : ModTower
{
    
    public override string BaseTower => TowerType.DartMonkey;
    public override string DisplayName => "Anakin Skywalker";
    public override int Cost => 500;
    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;
    public override string Portrait => "2DAnakin";
    public override string Icon => "2DAnakin";
    public override bool DontAddToShop => false;
    public override string Description => "Cuphead is here";

    public override TowerSet TowerSet => TowerSet.Primary;

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.display = new() { guidRef = "Anakin-Prefab" }; //required for custom displays to be recognized
        towerModel.GetBehavior<DisplayModel>().display = new() { guidRef = "Anakin-Prefab" }; //required for custom displays to be recognized
        towerModel.GetBehavior<DisplayModel>().scale = towerModel.GetBehavior<DisplayModel>().scale * 0.6f;
        
        

    }
}

[HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
static class FactoryCreateAsyncPatch
{
    [HarmonyPrefix]
    public static bool Prefix(ref Factory.__c__DisplayClass21_0 __instance, ref UnityDisplayNode prototype)
    {
        GameObject gObj;

        switch (__instance.objectId.guidRef) // makes sure to support loading more than one custom display
        {
            case "Anakin-Prefab":
                gObj = uObject.Instantiate(StarWarsMain.assetBundle.LoadAsset("anakin").Cast<GameObject>(), __instance.__4__this.DisplayRoot); //load the asset from the asset bundle and instantiates/creates it
                break;
            default:
                return true; //if the display is not custom, let the game create the base display
        }

        gObj.name = __instance.objectId.guidRef; //should be optional in theory, but i left it because its good for debugging/organization
        gObj.transform.position = new Vector3(Factory.kOffscreenPosition.x, 0, 0); //move the object offscreen so the game doesn't try to render it when its not needed 

        gObj.AddComponent<UnityDisplayNode>(); //adds a UnityDisplayNode component to the object, this is needed for the game to recognize it as a display
        prototype = gObj.GetComponent<UnityDisplayNode>(); //gets the UnityDisplayNode component from the object
        __instance.__4__this.active.Add(prototype); //adds the object to the active list, this is needed for the game to show the display
        __instance.onComplete.Invoke(prototype); //calls the onComplete delegate thats automatically created by the game, this is needed for the game to use it as a display

        return false; //prevents the game from creating the base display once a custom display is created
    }
}
