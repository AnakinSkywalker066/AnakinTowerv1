using BTD_Mod_Helper.Extensions;

public class AnakinSkywalker : StarWarsTower
{
    public override string Name => "Anakin Skywalker";
    public override string Description => "Anakin is a Jedi Knight";
    public override Faction TowerFaction => Faction.Jedi;
    public override int MaxTier => 0;
    public override Dictionary<string, Il2CppSystem.Type> Components => new() { { "AnakinSkywalker-Prefab", Il2CppType.Of<Jedi>() } };
    [RegisterTypeInIl2Cpp]
    public class Jedi : MonoBehaviour
    {
        public Jedi(IntPtr ptr) : base(ptr) { }
        public GameObject activeObj = null;
        public GameObject Anakin = null;
        float timer = 0;
        int selectSound = 0;
        int upgradeSound = 0;
        void Start()
        {
            Anakin = transform.GetChild(0).gameObject;
            Anakin.transform.localPosition = new(0, 0, 0);
            activeObj = Anakin;
        }
        void Update()
        {
            timer += Time.fixedDeltaTime;
            if (timer > 10)
            {
                if (activeObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("standing-idle"))
                {
                    switch (new System.Random().Next(1, 101))
                    {
                        case < 16:
                            if (gameObject.name.Contains("Anakin"))
                            {
                                PlayAnimation(activeObj.GetComponent<Animator>(), "standing-idle0" + new System.Random().Next(1, 3), 0.4f);
                            }
                            break;
                    }
                }
                timer = 0;
            }
        }
        public void PlaySelectSound()
        {
            if (selectSound > 5)
            {
                selectSound = 0;
            }
            selectSound += 1;
            if (activeObj.name.Contains("Force"))
            {
                PlaySound("Force" + selectSound);
            }
        }
        public void PlayUpgradeSound()
        {
            upgradeSound += 1;
            selectSound = 0;
            PlaySound("Force" + upgradeSound);
        }
    }
    [HarmonyPatch(typeof(FilterInBaseTowerId), "FilterTowerModel")]
    public class FilterInBaseTowerIdFilterTower_Patch
    {
        [HarmonyPrefix]
        //__instance.entity.blahblahblah is the tower with the filter, towerModel is the tower thats being filtered out or in
        public static bool Prefix(FilterInBaseTowerId __instance, ref bool __result, TowerModel towerModel)
        {
            if (__instance.entity.dependants[0].Cast<RangeSupport>().tower.towerModel.baseId != "Anakin")
            {
                if (__instance.filterInBaseTowerIdModel.baseIds.Contains(towerModel.baseId))
                {
                    __result = true;
                }
                else
                {
                    __result = false;
                }
            }
            else
            {
                if (towerModel.baseId == "AnakinSkywalker")
                {
                    __result = false;
                }
                else
                {
                    __result = true;
                }
            }
            return false;
        }
    }

    public override ShopTowerDetailsModel ShopDetails()
    {
        ShopTowerDetailsModel details = gameModel.towerSet[0].Clone<ShopTowerDetailsModel>();
        details.towerId = Name;
        details.name = Name;
        details.towerIndex = 14;
        details.pathOneMax = 0;
        details.pathTwoMax = 0;
        details.pathThreeMax = 0;
        details.popsRequired = 0;
        return details;
    }
    public override TowerModel[] GenerateTowerModels()
    {
        return new TowerModel[]{
                Base()

            };
    }
    public TowerModel Base()
    {
        TowerModel JediAnakin = gameModel.GetTowerFromId("BoomerrangMonkey").Clone<TowerModel>();
        JediAnakin.name = Name;
        JediAnakin.baseId = JediAnakin.name;
        JediAnakin.towerSet = TowerSet.Military;
        JediAnakin.cost = 800;
        JediAnakin.tier = 0;
        JediAnakin.tiers = new[] { 0, 0, 0 };
        JediAnakin.range = 45;
        JediAnakin.radius = 8;
        JediAnakin.display = new() { guidRef = "AnakinSkywalker-Prefab" };
        JediAnakin.icon = new() { guidRef = "2DAnakin" };
        JediAnakin.instaIcon = JediAnakin.icon;
        JediAnakin.portrait = new() { guidRef = "2DAnakin" };
        List<Model> ravenBehav = JediAnakin.behaviors.ToList();
        DisplayModel display = ravenBehav.GetModel<DisplayModel>();
        display.display = JediAnakin.display;
        display.positionOffset = new(0, 0, 200);
        ravenBehav.Remove(ravenBehav.First(a => a.GetIl2CppType().Name == "AttackModel"));
        return JediAnakin;
    }
    public override void Select(Tower tower)
    {
        tower.Node.graphic.gameObject.GetComponent<Jedi>().PlaySelectSound();
    }
}
        
        
    
