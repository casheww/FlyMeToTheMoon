using System.Collections.Generic;
using BepInEx;

namespace FMTTM
{
    [BepInPlugin("casheww.fmttm", "FlyMeToTheMoon", "0.1.0")]
    public class FMTTMMod : BaseUnityPlugin
    {
        public static FMTTMMod Instance { get; private set; }

        void OnEnable()
        {
            Instance = this;
            NewSounds.Setup();

            PlayerHooks.SetHooks();
            MusicHooks.SetHooks();
            SoundHooks.SetHooks();
        }

        void OnDisable()
        {
            PlayerHooks.ClearHooks();
            MusicHooks.ClearHooks();
            SoundHooks.ClearHooks();
        }

        public static OptionalUI.OptionInterface LoadOI() => new Config();

        // dict of players to whether they are singing or not
        public static Dictionary<Player, bool> choir = new Dictionary<Player, bool>();

        public static bool CheckAnyPlayerSinging()
        {
            foreach (bool singing in choir.Values)
            {
                if (singing) return true;
            }
            return false;
        }

    }
}
