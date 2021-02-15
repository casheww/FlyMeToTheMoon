using System.IO;
using System.Linq;
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
            if (NewSounds.SoundNames.Contains(soundID.ToString()))
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
            int audioClip = NewSounds.FmttmAudio.Keys.ToArray()[random.Next(NewSounds.FmttmAudio.Count)];
            return new SoundLoader.SoundData(soundID, audioClip, 0.4f, 1, 1, 1);
        }

        static AudioClip SoundLoader_GetAudioClip(On.SoundLoader.orig_GetAudioClip orig, SoundLoader self, int i)
        {
            if (NewSounds.FmttmAudio.ContainsKey(i))
            {
                WAV wav = new WAV(GetWavData(i));
                AudioClip audioClip = AudioClip.Create(NewSounds.FmttmAudio[i].ToString(), wav.SampleCount, wav.ChannelCount,
                        wav.Frequency, false, false);
                audioClip.SetData(wav.LeftChannel, 0);
                return audioClip;
            }
            else
            {
                return orig(self, i);
            }
        }

        /// <summary>
        /// Gets the embedded WAV data corresponding to the audioClip number given 
        /// based on NewSounds.FmttmAudio
        /// </summary>
        /// <param name="i">audio clip number</param>
        /// <returns>byte array representing the WAV data</returns>
        static byte[] GetWavData(int i)
        {
            Debug.Log(i);
            string wavName = NewSounds.FmttmAudio[i].ToString();

            UnmanagedMemoryStream stream = SoundResources.ResourceManager.GetStream(wavName);

            byte[] bytes = new byte[stream.Length];
            for (int j = 0; j < stream.Length; j++)
            {
                bytes[j] = (byte)stream.ReadByte();
            }
            return bytes;
        }

        static readonly System.Random random = new System.Random();
    }
}
