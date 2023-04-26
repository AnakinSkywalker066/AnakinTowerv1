using BTD_Mod_Helper;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Towers;
using Il2CppInterop.Runtime;
using Il2CppSystem.Linq.Expressions;
using MelonLoader;
using MelonLoader.Utils;
using StarWarsMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;


[assembly: MelonInfo(typeof(StarWars), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
#nullable enable

namespace StarWarsMod;


public class StarWars : MelonMod
{
    public static GameModel gameModel;
    private static MelonLogger.Instance mllog;
    public static Dictionary<string, StarWarsTower> TowerTypes = new Dictionary<string, StarWarsTower>();
    public static string LoaderPath = MelonEnvironment.ModsDirectory;
    public static void Log(object thingtolog, string type = "msg")
    {
        switch (type)
        {
            case "msg":
                mllog.Msg(thingtolog);
                break;
            case "warn":
                mllog.Warning(thingtolog);
                break;
            case "error":
                mllog.Error(thingtolog);
                break;
        }
    }
    public override void OnEarlyInitializeMelon()
    {
        MelonLogger.Msg("Loading Star Wars");
        List<StarWarsTower> towerList = new();
        foreach (MelonMod mod in RegisteredMelons)
        {
            Assembly assembly = mod.MelonAssembly.Assembly;
            foreach (string bundle in assembly.GetManifestResourceNames())
            {
                try
                {
                    if (!Directory.Exists(LoaderPath))
                    {
                        Stream stream = assembly.GetManifestResourceStream(bundle);
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes);
                        File.WriteAllBytes(LoaderPath + bundle.Split('.')[2], bytes);
                    }
                }
                catch (Exception error) { mllog.Error(error); }
            }
            foreach (Type type in assembly.GetTypes())
            {
                try
                {
                    StarWarsTower tower = (StarWarsTower)Activator.CreateInstance(type);
                    if (tower.Name! == "")
                    {
                        towerList.Add(tower);
                        if (StarWarsTower.HasBundle)
                        {
                            tower.LoadedBundle = UnityEngine.AssetBundle.LoadFromFileAsync(LoaderPath + tower.Name.ToLower()).assetBundle;
                        }
                    }

                }
                catch { }
            }
        }
        if (towerList.Count() > 0)
        {
            towerList = towerList.OrderBy(a => a.Order).ToList();
            foreach (StarWarsTower tower in towerList)
            {
                TowerTypes.Add(tower.Name, tower);
            }
        }
    }
    public static void PrintError(Exception exception, string message = null)
    {
        if (message != null)
        {
            Log(message);
        }
        string error = exception.Message;
        error += "\n" + exception.TargetSite;
        error += "\n" + exception.StackTrace;
        Log(error, "error");
    }
    public static T LoadAsset<T>(string Asset, AssetBundle Bundle) where T : uObject
    {
        try
        {
            return Bundle.LoadAssetAsync(Asset, Il2CppType.Of<T>()).asset.Cast<T>();
        }
        catch
        {
            foreach (KeyValuePair<string, StarWarsTower> tower in TowerTypes)
            {
                tower.Value.LoadedBundle = AssetBundle.LoadFromFileAsync(LoaderPath + tower.Key.ToLower()).assetBundle;
            }
            try
            {
                return Bundle.LoadAssetAsync(Asset, Il2CppType.Of<T>()).asset.Cast<T>();
            }
            catch (Exception error)
            {
                PrintError(error, "Failed to load " + Asset + " from " + Bundle.name);
                try
                {
                    Log("Attempting to get available assets");
                    foreach (string asset in Bundle.GetAllAssetNames())
                    {
                        Log(asset);
                    }
                }
                catch
                {
                    Log("Bundle is null");
                }
                return null;
            }
        }
    }
    public static void PlaySound(string name)
    {
        Game.instance.audioFactory.PlaySoundFromUnity(null, name, "FX", 1, 1);
    }
    public static void PlayAnimation(Animator animator, string anim, float duration = 0.2f)
    {
        animator.CrossFade(anim, duration, 0, 0);
    }
}

    

    
