using MelonLoader;
using StarWarsMod;

[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
#nullable enable

namespace StarWarsMod;


public class StarWars : MelonMod
{
}
