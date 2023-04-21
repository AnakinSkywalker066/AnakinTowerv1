using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.Towers.Mods;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

namespace starwarsmod;

public class JediDisplay : ModCustomDisplay

{
    public abstract string GetAssetBundleName();

    public abstract string GetPrefabName();

    public virtual string GetMaterialName()
    {
        
    }

    //
    // Summary:
    //     On a ModCustomDisplay, this property does nothing
    public sealed override string BaseDisplay => "";

        public virtual bool LoadAsync => false;

        //
        // Summary:
        //     Performs alterations to the unity display node when it is created
        //
        // Parameters:
        //   node:
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
        }

        internal override void GetBasePrototype(Factory factory, Action<UnityDisplayNode> onComplete)
        {
            this.GetBasePrototype(mod, onComplete);
        }
    }
