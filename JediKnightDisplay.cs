using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppInterop.Runtime;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace starwarsmod
{

    public abstract class JediKnightDisplay : BTD_Mod_Helper.Api.Display.ModTowerCustomDisplay
     
    { 
        public static UnityEngine.Object LoadAsset<T>(string Asset, AssetBundle Bundle)
        {
            try
            {
                return Bundle.LoadAssetAsync(Asset, Il2CppType.Of<T>()).asset;
            }
            catch (Exception error)
            {
                MelonLogger.Msg("Failed to load " + Asset + " from " + Bundle.name);
                try
                {
                    MelonLogger.Msg("Attempting to get available assets");
                    foreach (string asset in Bundle.GetAllAssetNames())
                    {
                        MelonLogger.Msg(asset);
                    }
                }
                catch
                {
                    MelonLogger.Msg("Bundle is null");
                }
                string message = error.Message;
                message += "@\n" + error.StackTrace;
                MelonLogger.Msg(message, "error");
                return null;
            }
        }
        public override string AssetBundleName { get; }
        public override bool LoadAsync { get; }
        public override string MaterialName { get; }
        public override string PrefabName { get; }
        public static BTD_Mod_Helper.Api.Towers.ModTower tower { get; }
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            
            var bundle = GetBundle(mod, "JediAnakin");
            var prefab = LoadAsset<GameObject>(PrefabName, bundle);
            UnityEngine.Object.Instantiate(prefab, node.transform.GetChild(0).transform);
            node.transform.GetChild(0).transform.localScale *= 6;
            node.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
            node.transform.GetChild(0).transform.localPosition = Vector3.zero;
            node.transform.GetChild(0).transform.localPosition -= new Vector3(0, 0, 0);
            node.gameObject.SetActive(true);
        }
    }
}
