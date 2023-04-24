using MelonLoader;
using starwarsmod;
using UnityEngine;
using System.Reflection;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;


[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace starwarsmod;
public class StarWars : BloonsTD6Mod 
{ 
public override void OnApplicationStart()
{

foreach (var asset in MelonAssembly.Assembly.GetManifestResourceNames())
MelonLogger.Msg(asset);


assetBundle = AssetBundle.LoadFromMemory(ExtractResource("JediAnakin.bundle"));// if using unityexplorer, there is an error, but everything still works
ModHelper.Msg<StarWars>("Star Wars loaded!");
}

public static AssetBundle assetBundle;


private byte[] ExtractResource(string filename)
{
Assembly a = MelonAssembly.Assembly; // get the assembly
return a.GetEmbeddedResource(filename).GetByteArray(); // get the embedded bundle as a raw file that unity can read
}
    
}
