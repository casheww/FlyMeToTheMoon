using OptionalUI;
using UnityEngine;

namespace FMTTM
{
    class Config : OptionInterface
    {
        public Config() : base(plugin: FMTTMMod.Instance) { }

        public override void Initialize()
        {
            // hopefully all of the 'magic numbers' can be forgiven
            base.Initialize();
            Tabs = new OpTab[1];
            Tabs[0] = new OpTab("FMTTM");

            // metadata start
            string modName = FMTTMMod.Instance.Info.Metadata.Name;
            string modVrsn = FMTTMMod.Instance.Info.Metadata.Version.ToString();
            string modAuth = FMTTMMod.Instance.Info.Metadata.GUID.Split('.')[0];

            Tabs[0].AddItems(new UIelement[]
            {
                new OpLabel(new Vector2(50, 550), new Vector2(150, 40), modName, FLabelAlignment.Left, true),
                new OpLabel(new Vector2(228, 552), new Vector2(100, 30), $"by {modAuth}", FLabelAlignment.Left),
                new OpLabel(new Vector2(58, 535), new Vector2(100, 15), $"v{modVrsn}", FLabelAlignment.Left),
                new OpLabel(new Vector2(40, 500), new Vector2(250, 15), "Configure song hotkey for each player below!", FLabelAlignment.Left)
            });
            // metadata end

            // player control config start
            Vector2 rectSize = new Vector2(245, 59);
            int[,] controlConfigBoxCoords = new int[,]
            {
                { 50, 400 }, { 305, 400 },
                { 50, 330 }, { 305, 330 }
            };
            
            for (int i = 0; i < controlConfigBoxCoords.GetLength(0); i++)
            {
                // rect's position coords (bottom-left corner)
                Vector2 bl = new Vector2(controlConfigBoxCoords[i, 0], controlConfigBoxCoords[i, 1]);

                OpRect rect = new OpRect(bl, rectSize);
                OpLabel label = new OpLabel(
                    new Vector2(bl.x + 5, bl.y + 20),
                    new Vector2(100, 15), $"Player {i + 1}");

                string keyCodeString = (i == 0) ? KeyCode.C.ToString() : KeyCode.None.ToString();
                OpKeyBinder keyBinder = new OpKeyBinder(
                    new Vector2(bl.x + 98, bl.y + 7),
                    new Vector2(140, 45), modID: FMTTMMod.Instance.Info.Metadata.GUID, $"player{i + 1}_key",
                    keyCodeString, false, OpKeyBinder.BindController.AnyController);

                Tabs[0].AddItems(new UIelement[] { rect, label, keyBinder });
            }
            // player control config end
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void ConfigOnChange()
        {
            base.ConfigOnChange();
            // TODO: key selection logic
        }

        public static KeyCode[] keyBindings { get; private set; }
    }
}