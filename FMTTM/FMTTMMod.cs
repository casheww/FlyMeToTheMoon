using System.Collections.Generic;
using BepInEx;

namespace FMTTM
{
    [BepInPlugin("casheww.fmttm", "FlyMeToTheMoon", "0.1.0")]
    public class FMTTMMod : BaseUnityPlugin
    {
        void OnEnable()
        {
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
