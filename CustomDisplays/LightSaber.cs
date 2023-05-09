using BTD_Mod_Helper.Api.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace starwarsmod.CustomDisplays
{
    public class LightSaber : ModCustomDisplay
    {
        public override string AssetBundleName => "sky";
        public override string PrefabName => "lightsaber";
    }
}
