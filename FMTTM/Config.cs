using System;
using OptionalUI;
using UnityEngine;

namespace FMTTM
{
    class Config : OptionInterface
    {
        public Config() : base(plugin: FMTTMMod.Instance) { }

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[1];
            Tabs[0] = new OpTab("FMTTM");

            // metadata
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

            // player control config
            Vector2 rectSize = new Vector2(245, 125);
            int[,] controlConfigBoxCoords = new int[,]
            {
                { 50, 365 }, { 305, 365 },
                { 50, 230 }, { 305, 230 }
            };
            
            for (int i = 0; i < controlConfigBoxCoords.GetLength(0); i++)
            {
                // rect's position coords (bottom-left corner)
                Vector2 bl = new Vector2(controlConfigBoxCoords[i, 0], controlConfigBoxCoords[i, 1]);

                OpRect rect = new OpRect(bl, rectSize);
                OpLabel label = new OpLabel(
                    new Vector2(bl.x + 6, bl.y + 60),
                    new Vector2(100, 15), $"Player {i + 1}");

                // TODO: add key selector elements

                Tabs[0].AddItems(new UIelement[] { rect, label });
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