using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using UnityEngine;
using StarWarsMod;
using System.Reflection;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.TowerSets;
using uObject = UnityEngine.GameObject;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2Cpp;


[assembly: MelonInfo(typeof(StarWarsMod.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace StarWarsMod;
public class Main : BloonsTD6Mod
{
    

    public override void OnApplicationStart()
    {

        foreach (var asset in MelonAssembly.Assembly.GetManifestResourceNames())
            MelonLogger.Msg(asset);
        //previous two lines are for debugging/finding names of assets
        assetbundle = AssetBundle.LoadFromMemory(ExtractResource("sky"));

        ModHelper.Msg<Main>("ANAKIN DOES NOT HAVE ABILITY SEE CAMO BLOONS RIGHT NOW");        ModHelper.Msg<Main>("Star Wars HAS LOADED!!!!!!");
    }

    public static AssetBundle assetbundle;

    private byte[] ExtractResource(string filename)
    {
        Assembly a = MelonAssembly.Assembly; // get the assembly
        return a.GetEmbeddedResource(filename).GetByteArray(); // get the embedded bundle as a raw file that unity can read
    }


    public class AnakinSkywalker : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.BoomerangMonkey;
        public override string DisplayName => "Anakin Skywalker";
        public override int Cost => 1000;
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;
        public override string Portrait => "2DAnakin";
        public override string Icon => "2DAnakin";
        public override bool DontAddToShop => false;
        public override string Description => "Younglins Beware of This Man!";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetDescendant<DamageModel>().immuneBloonProperties = (BloonProperties)0;
            towerModel.display = new() { guidRef = "AnakinSaber-Prefab" }; //required for custom displays to be recognized
            towerModel.GetBehavior<DisplayModel>().display = new() { guidRef = "AnakinSaber-Prefab" }; //required for custom displays to be recognized
            towerModel.GetBehavior<DisplayModel>().scale = towerModel.GetBehavior<DisplayModel>().scale * 1f;
             //required for custom displays to be recognized
            towerModel.displayScale = 30f;
            var proj = towerModel.GetAttackModel().weapons[0].projectile;
            proj.display = new() { guidRef = "lightsaber-Prefab" };
            proj.GetBehavior<DisplayModel>().display = new() { guidRef = "lightsaber-Prefab" };
            
            proj.scale = 50f;
            

            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.animateOnMainAttack = true;
                weaponModel.projectile.pierce = 100000;
                
            }


        }
        public override int GetTowerIndex(List<TowerDetailsModel> towerSet)
        {
            return towerSet.First(model => model.towerId == TowerType.GlueGunner).towerIndex + 1;
        }
        public override bool IsValidCrosspath(int[] tiers) =>
           ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);


        [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
        static class FactoryCreateAsyncPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(ref Factory.__c__DisplayClass21_0 __instance, ref UnityDisplayNode prototype)
            {   
                 //loads the asset bundle from the raw file
                GameObject gObj;

                switch (__instance.objectId.guidRef) // makes sure to support loading more than one custom display
                {
                    case "AnakinSaber-Prefab":
                        gObj = uObject.Instantiate(assetbundle.LoadAsset("AnakinSaber").Cast<uObject>(), __instance.__4__this.DisplayRoot); //load the asset from the asset bundle and instantiates/creates it
                        break;
                    case "lightsaber-Prefab":
                        gObj = uObject.Instantiate(assetbundle.LoadAsset("lightsaber").Cast<uObject>(), __instance.__4__this.DisplayRoot); //load the asset from the asset bundle and instantiates/creates it
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
    public class Top1 : ModUpgrade<AnakinSkywalker>
    {
        public override string Name => "Top1";
        public override string DisplayName => "Youngling";
        public override string Description => "Anakin Can Now Use His LightSaber Better";
        public override int Cost => 750;
        public override int Path => TOP;
        public override int Tier => 1;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.Rate *= 0.75f;
                weaponModel.animateOnMainAttack = true;
                weaponModel.projectile.GetDamageModel().damage += 5;

            }
        }
        public override string Icon => "Top1";
        public override string Portrait => "Top1";
    }

    public class Top2 : ModUpgrade<AnakinSkywalker>
    {
        public override string Name => "Top2";
        public override string DisplayName => "Padawan";
        public override string Description => "Anakin is Now Being Taught The Ways Of Killing Younglings I Mean Force...";
        public override int Cost => 1500;
        public override int Path => TOP;
        public override int Tier => 2;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.Rate *= 0.5f;
                weaponModel.projectile.GetDamageModel().damage += 5;
                weaponModel.animateOnMainAttack = true;
            }
        }
        public override string Icon => "Top2";
        public override string Portrait => "Top2";
    }

    public class Top3 : ModUpgrade<AnakinSkywalker>
    {
        public override string Name => "Top3";
        public override string DisplayName => "Jedi Knight";
        public override string Description => "Now Anakin Will Show His True Jedi Power!";
        public override int Cost => 7500;
        public override int Path => TOP;
        public override int Tier => 3;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 4, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 4, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 4, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 4, false, true));
                weaponModel.Rate = 0.15f;
                weaponModel.animateOnMainAttack = true;
                weaponModel.projectile.GetDamageModel().damage += 5;
            }
        }
        public override string Icon => "Clonewars";
        public override string Portrait => "Clonewars";
    }

    public class Top4 : ModUpgrade<AnakinSkywalker>
    {
        public override string Name => "Top4";
        public override string DisplayName => "Jedi Master";
        public override string Description => "Something Anakin Never Got xD";
        public override int Cost => 15000;
        public override int Path => TOP;
        public override int Tier => 4;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.animateOnMainAttack = true;
                weaponModel.projectile.pierce += 5;
                weaponModel.projectile.GetDamageModel().damage += 5;
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 6, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 6, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 6, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 6, false, true));
                weaponModel.Rate = 0.1f;
            }
        }
        public override string Icon => "top4";
        public override string Portrait => "top4";
    }

    public class Top5 : ModUpgrade<AnakinSkywalker>
    {
        public override string Name => "Top5";
        public override string DisplayName => "Grand Master Jedi";
        public override string Description => "Only The True Power Of The Jedi Can Be Achieved By The Will Of The Force";
        public override int Cost => 75000;
        public override int Path => TOP;
        public override int Tier => 5;
        public override void ApplyUpgrade(TowerModel towerModel)
        {

            
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                
                weaponModel.projectile.pierce += 5;
                weaponModel.projectile.GetDamageModel().damage += 5;
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 55, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 80, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 230, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 190, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bad", "Bad", 1, 390, false, true));
                weaponModel.Rate = 0.05f;
                weaponModel.animateOnMainAttack = true;
            }
        }
        public override string Icon => "Top5";
        public override string Portrait => "Top5";
    }
    

}