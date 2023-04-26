﻿namespace StarWarsMod
{
    public class StarWarsTower
    {
        public virtual string Name => "";
        public virtual byte[] TowerBundle => null;
        public virtual ShopTowerDetailsModel ShopDetails() => null;
        public virtual HeroDetailsModel GenerateHeroDetails() => null;
        public HeroDetailsModel HeroDetails = null;
        public virtual TowerModel[] GenerateTowerModels() => null;
        public TowerModel[] TowerModels = null;
        public virtual int MaxTier => 0;
        public virtual string Description => "";
        public virtual UpgradeModel[] GenerateUpgradeModels() => null;
        public UpgradeModel[] UpgradeModels = null;
        public AssetBundle LoadedBundle = null;
        public virtual void Attack(Weapon weapon) { }
        public virtual void Upgrade(int tier, Tower tower) { }
        public virtual bool Ability(string ability, Tower tower)
        {
            return true;
        }

       

        public virtual void Create(Tower tower) { }
        public virtual SkinData HeroSkin() => null;
        public virtual void Select(Tower tower) { }
        public virtual void Sell(Tower tower) { }
        public virtual void RoundStart() { }
        public virtual void RoundEnd() { }
        public virtual Dictionary<string, Il2CppSystem.Type> Components => new();
        public virtual bool AddToShop => true;
        public virtual bool Upgradable => true;
        public virtual string UpgradeScreenSound => null;
        public virtual bool ShowUpgradeMenu => true;
        public enum Faction
        {
            Jedi,
            Sith,
            NotSet
        }
        public virtual Faction TowerFaction => Faction.NotSet;
        public virtual int Order => 0;

        public static bool HasBundle = true;
    }
}
    
