namespace starwarsmod
{
    public static class Helper
    {
       
        public static UnityEngine.Object LoadAsset<StarWars>(string Asset, AssetBundle Bundle)
        {
            try
            {
                return Bundle.LoadAssetAsync(Asset, Il2CppType.Of<StarWars>()).asset;
            }
            catch (Exception error)
            {
                MelonLogger.Msg("Failed to load " + Asset + " from " + Bundle.name);
                try
                {
                    MelonLogger.Msg("Attempting to get available assets");
                    foreach (string asset in Bundle.GetAllAssetNames())
                    {
                        MelonLogger.Msg(asset);
                    }
                }
                catch
                {
                    MelonLogger.Msg("Bundle is null");
                }
                string message = error.Message;
                message += "@\n" + error.StackTrace;
                MelonLogger.Msg(message, "error");
                return (UnityEngine.Object)false;
            }
        }

        public static void Node(UnityDisplayNode node, string PrefabName, BloonsTD6Mod mod)
        {
            if (mod is null)
            {
                throw new ArgumentNullException(nameof(mod));
            }
            
            node.GetRenderer<SpriteRenderer>().sprite = null;
            var prefab = LoadAsset<StarWars>(PrefabName, (AssetBundle)"anakin");
            UnityEngine.Object.Instantiate(prefab, node.transform.GetChild(0).transform);
            node.transform.GetChild(0).transform.localScale *= 10;
            node.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
            node.transform.GetChild(0).transform.localPosition = Vector3.zero;
            node.transform.GetChild(0).transform.localPosition -= new Vector3(0, 0, 0);
        }

    }
}