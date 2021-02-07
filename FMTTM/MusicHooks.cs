using System;

namespace FMTTM
{
    class MusicHooks
    {
        public static void SetHooks()
        {
            On.Music.MusicPlayer.Update += MusicPlayer_Update;
        }

        public static void ClearHooks()
        {
            On.Music.MusicPlayer.Update -= MusicPlayer_Update;
        }

        static void MusicPlayer_Update(On.Music.MusicPlayer.orig_Update orig, Music.MusicPlayer self)
        {
            orig(self);

            if (FMTTMMod.CheckAnyPlayerSinging())
            {
                self.song.FadeOut(20);
            }
            else
            {
                self.song.fadeInTime = 1;
            }
        }

    }
}
