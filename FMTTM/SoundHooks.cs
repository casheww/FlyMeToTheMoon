using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CustomWAV;

namespace FMTTM
{
    class SoundHooks
    {
        public static void SetHooks()
        {
            On.VirtualMicrophone.PlaySound_SoundID_PositionedSoundEmitter_bool_float_float_bool += VirtualMicrophone_PlaySound;
            On.SoundLoader.GetAudioClip += SoundLoader_GetAudioClip;
        }

        public static void ClearHooks()
        {
            On.VirtualMicrophone.PlaySound_SoundID_PositionedSoundEmitter_bool_float_float_bool -= VirtualMicrophone_PlaySound;
            On.SoundLoader.GetAudioClip -= SoundLoader_GetAudioClip;
        }


        static void VirtualMicrophone_PlaySound(On.VirtualMicrophone.orig_PlaySound_SoundID_PositionedSoundEmitter_bool_float_float_bool orig,
                VirtualMicrophone self, SoundID soundID, PositionedSoundEmitter controller, bool loop, float vol,
                float pitch, bool randomStartPosition)
        {
            if (soundID == EnumExt_SoundID.SlugcatSing)
            {
                SoundLoader.SoundData soundData = GetSlugcatSingSound(soundID);
                self.soundObjects.Add(new VirtualMicrophone.ObjectSound(self, soundData, loop, controller, vol, pitch, randomStartPosition));
            }
            else
            {
                orig(self, soundID, controller, loop, vol, pitch, randomStartPosition);
            }
        }

        static SoundLoader.SoundData GetSlugcatSingSound(SoundID soundID)
        {
            return new SoundLoader.SoundData(soundID, 41919, 1, 1, 1, 1);
        }

        static AudioClip SoundLoader_GetAudioClip(On.SoundLoader.orig_GetAudioClip orig, SoundLoader self, int i)
        {
            if (fmttmAudioIndexes.Contains(i))
            {
                WAV wav = new WAV(GetWavData(i));
                AudioClip audioClip = AudioClip.Create("testsound", wav.SampleCount, wav.ChannelCount,
                        wav.Frequency, false, false);
                audioClip.SetData(wav.LeftChannel, 0);
                return audioClip;
            }
            else
            {
                return orig(self, i);
            }
        }

        static byte[] GetWavData(int i)
        {
            switch (i)
            {
                default:
                case 41919:
                    UnmanagedMemoryStream stream = SoundResources.test;
                    byte[] bytes = new byte[stream.Length];
                    for (int j = 0; j < stream.Length; j++)
                    {
                        bytes[j] = (byte)stream.ReadByte();
                    }
                    return bytes;
            }
        }

        // hopefully safe placeholder audio values
        public static List<int> fmttmAudioIndexes = new List<int> { 41919, 41920, 41921 };

    }
}
