using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using BTD_Mod_Helper;
using Il2CppInterop.Runtime;
using uObject = UnityEngine.GameObject;
using MelonLoader;
using System;
using Il2CppInterop.Runtime.Injection;

namespace starwarsmod;

public class Jedi : ModTower
{
    public static int GetTowerIndex(List<TowerDetailsModel> towerSet)
    {
        return towerSet.First(model => model.towerId == TowerType.GlueGunner).towerIndex + 1;
    }

    public override TowerSet TowerSet => TowerSet.Military;
    public override string BaseTower => TowerType.DartMonkey;
    public override string DisplayName => "Anakin Skywalker";
    public override int Cost => 500;
    public override int TopPathUpgrades => 5;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;
    public override string Portrait => "2DAnakin";
    public override string Icon => "2DAnakin";
    public override string Description => "Anakin Uses the Force to Destory Bloons!";
    public override string Name => "Jedi Knight";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {

        towerModel.ApplyDisplay<JediKnightDisplay>();
        towerModel.GetAttackModel().weapons[0].projectile = Game.instance.model.GetTower(TowerType.BoomerangMonkey).GetAttackModel().weapons[0].projectile.Duplicate();
        towerModel.GetAttackModel().weapons[0].projectile.ApplyDisplay<Lightsaber>();
        towerModel.GetAttackModel().weapons[0].projectile.pierce = 100;

    }




    public class JediKnightDisplay : ModTowerCustomDisplay<Jedi>
    {
        public override string AssetBundleName => "AssetBundle/JediAnakin";


        public static Jedi Jedi;
        public override ModTower Tower => Jedi;


        public override string PrefabName => "Anakin-Prefab";



        public static void LoadNode(UnityDisplayNode node, string prefabName, BloonsMod mod)
        {
            node.GetRenderer<SpriteRenderer>().sprite = null;
            var bundle = GetBundle(mod, "JediAnakin");
            var prefab = bundle.LoadAssetAsync(prefabName, Il2CppType.Of<uObject>()).asset;
            var anakinGameObject = GameObject.Instantiate(prefab, node.transform.GetChild(0).transform);
            node.transform.GetChild(0).transform.localScale *= 15;
            node.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
            node.transform.GetChild(0).transform.localPosition = Vector3.zero;
            node.transform.GetChild(0).transform.localPosition -= new Vector3(0, 0, 0);
            
        }

        public override bool UseForTower(int[] tiers)
        {
            return true;
        }
    }
}

