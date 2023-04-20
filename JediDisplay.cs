namespace starwarsmod;

public class JediDisplay : ModDisplay
    {

    public override string BaseDisplay => "Anakin";
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Jedi.JediTower(node, "BoomerangMonkey-000", (BloonsTD6Mod)mod);
    }
}