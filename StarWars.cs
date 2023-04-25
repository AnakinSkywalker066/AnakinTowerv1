using MelonLoader;
using BTD_Mod_Helper;
using starwarsmod;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Unity.Display;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Reflection;
using ApprovalUtilities.SimpleLogger.Writers;

[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace starwarsmod
{


    public class StarWars : BloonsTD6Mod
    {
        
        public static AssetBundle assetBundle;

        public override void OnApplicationStart()
        {
            assetBundle = AssetBundle.LoadFromMemory(Models.jediresource);
            ModHelper.Msg<StarWars>("This Is Where The Fun Begins!");
        }
    }



    public class AnakinSkywalker : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.BoomerangMonkey;
        public override string DisplayName => "AnakinSkywalker";
        public override int Cost => 500;
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;
        public override string Portrait => "Icon";
        public override string Icon => "2DAnakin";
        public override bool DontAddToShop => false;
        public override string Description => "The strongest Jedi Ever";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.display = new() { guidRef = "AnakinSkywalker-Prefab" }; //required for custom displays to be recognized
            towerModel.GetBehavior<DisplayModel>().display = new() { guidRef = "AnakinSkywalker-Prefab" }; //required for custom displays to be recognized
            towerModel.GetBehavior<DisplayModel>().scale = towerModel.GetBehavior<DisplayModel>().scale * 0.6f;
            towerModel.GetAttackModel().weapons[0].projectile.ApplyDisplay<Lightsaber>();
        }
    }

    [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
    static class FactoryCreateAsyncPatch
    {
        private const string Name = "AnakinSkywalker";

        [HarmonyPrefix]
        public static bool Prefix(Factory.__c__DisplayClass21_0 __instance, UnityDisplayNode prototype)
        {
            GameObject gObj;
            
            switch (__instance.objectId.guidRef) // makes sure to support loading more than one custom display
            {
                case "AnakinSkywalker-Prefab":
                    gObj = Object.Instantiate(StarWars.assetBundle.LoadAsset(Name).Cast<GameObject>(), __instance.__4__this.DisplayRoot); //load the asset from the asset bundle and instantiates/creates it
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
    
}
