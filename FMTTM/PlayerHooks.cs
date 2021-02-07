using System;
using System.Collections.Generic;
using UnityEngine;

namespace FMTTM
{
    class PlayerHooks
    {
        public static void SetHooks()
        {
            On.Player.Update += Player_Update;
        }

        public static void ClearHooks()
        {
            On.Player.Update -= Player_Update;
        }


        static void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
        {
            orig(self, eu);

            if (!CanSing(self.Consious, self.input[0])) return;

            if (Input.GetKey(KeyCode.C))
            {
                if (FMTTMMod.choir[self] != true)
                {
                    FMTTMMod.choir[self] = true;
                    playerVoices[self] = self.room.PlaySound(EnumExt_SoundID.SlugcatSing, self.firstChunk);
                }
            }
            else
            {
                FMTTMMod.choir[self] = false;
            }
        }

        static bool CanSing(bool consious, Player.InputPackage input)
        {
            /* sluggy should only be able to sing if consious and not moving or eating */
            return consious && !input.pckp && input.x == 0 && input.y == 0 && !input.jmp;
        }

        static Dictionary<Player, ChunkSoundEmitter> playerVoices = new Dictionary<Player, ChunkSoundEmitter>();

    }
}
