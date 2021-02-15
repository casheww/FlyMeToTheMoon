using System.Collections.Generic;
using System.Reflection;

namespace FMTTM
{
    /// <summary>
    /// For keeping track of sound IDs added via enum extender
    /// </summary>
    class NewSounds
    {
        public static void Setup()
        {
            FieldInfo[] fields = typeof(EnumExt_SoundID).GetFields();
            List<string> fieldNames = new List<string>();
            foreach (FieldInfo fInfo in fields)
            {
                fieldNames.Add(fInfo.Name);
            }
            SoundNames = fieldNames;
        }

        public static List<string> SoundNames { get; private set; }

        public static int TestIndex { get => 41919; }
        public static List<int> SlugcatAudioIndexes { get => new List<int> { 41920, 41921, 41922, 41923 }; }

        public static Dictionary<int, SoundID> FmttmAudio
        {
            get =>
                new Dictionary<int, SoundID>
                {
                    { 41919, EnumExt_SoundID.Test },
                    { 41920, EnumExt_SoundID.SlugcatSingA },
                    { 41921, EnumExt_SoundID.SlugcatSingB },
                    { 41922, EnumExt_SoundID.SlugcatSingC },
                    { 41923, EnumExt_SoundID.SlugcatSingD }
                };
        }
    }
}
