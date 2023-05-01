using BTD_Mod_Helper;
using MelonLoader;
using StarWarsMod;

[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace StarWarsMod;
public class StarWars : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<StarWars>("Star Wars Mod Has Fully Loaded");
    }
    

    
}
    

