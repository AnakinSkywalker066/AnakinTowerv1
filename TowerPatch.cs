using Il2CppAssets.Scripts.Simulation;

namespace starwarsmod
{
    public class TowerPatches
{
    [HarmonyPatch(typeof(Weapon), "SpawnDart")]
    public class WeaponSpawnDart_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Weapon __instance)
        {
            string towerName = __instance.attack.tower.towerModel.baseId;
            if (TowerTypes.ContainsKey(towerName))
            {
                try
                {
                    TowerTypes[towerName].Attack(__instance);
                }
                catch (Exception error)
                {
                    PrintError(error, "Failed to run Attack for " + towerName);
                }
            }
        }
    }
    [HarmonyPatch(typeof(Simulation), "RoundStart")]
    public class SimulationRoundStart_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (StarWarsTower tower in TowerTypes.Values)
            {
                try
                {
                    tower.RoundStart();
                }
                catch (Exception error)
                {
                    PrintError(error, "Failed to run RoundStart for " + tower.Name);
                }
            }
        }
    }
    [HarmonyPatch(typeof(Simulation), "RoundEnd")]
    public class SimulationRoundEnd_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (StarWarsTower tower in TowerTypes.Values)
            {
                try
                {
                    tower.RoundEnd();
                }
                catch (Exception error)
                {
                    PrintError(error, "Failed to run RoundEnd for " + tower.Name);
                }
            }
        }
    }
    [HarmonyPatch(typeof(Tower), "OnPlace")]
    public class TowerOnPlace_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Tower __instance)
        {
            string towerName = __instance.towerModel.baseId;
            if (TowerTypes.ContainsKey(towerName))
            {
                try
                {
                    TowerTypes[towerName].Create(__instance);
                }
                catch (Exception error)
                {
                    PrintError(error, "Failed to run Create for " + towerName);
                }
            }
        }
    }
    [HarmonyPatch(typeof(TowerSelectionMenu), "SelectTower")]
    public class TowerSelectionMenuSelectTower_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(TowerSelectionMenu __instance)
        {
            string towerName = __instance.selectedTower.tower.towerModel.baseId;
                if (TowerTypes.ContainsKey(towerName))
            {
                try
                {
                    TowerTypes[towerName].Select(__instance.selectedTower.tower);
                }
                catch (Exception error)
                {
                    PrintError(error, "Failed to run Select for " + towerName);
                }
            }
        }
    }
}
}
