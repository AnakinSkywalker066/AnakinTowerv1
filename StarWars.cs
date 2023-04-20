[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace starwarsmod;

public class StarWars : BloonsTD6Mod
{
    public override void OnApplicationStart() 
    {
    ModHelper.Msg<StarWars>("Star Wars has loaded fully");
    }
}
    
