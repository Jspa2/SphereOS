﻿using SphereOS.Core;
using SphereOS.Gui.UILib;
using System.Drawing;
using Cosmos.System.Graphics;

namespace SphereOS.Gui.Apps.CodeStudio
{
    internal class CodeStudio : Process
    {
        internal CodeStudio() : base("CodeStudio", ProcessType.Application) { }

        Window splash;

        WindowManager wm = ProcessManager.GetProcess<WindowManager>();

        [IL2CPU.API.Attribs.ManifestResourceStream(ResourceName = "SphereOS.Gui.Resources.CodeStudio.Splash.bmp")]
        private static byte[] _splashBytes;
        private static Bitmap splashBitmap = new Bitmap(_splashBytes);

        private Ide ide;

        private bool ideCreated = false;

        internal override void Start()
        {
            base.Start();
            splash = new Window(this, 372, 250, 535, 300);
            wm.AddWindow(splash);

            splash.DrawImage(splashBitmap, 0, 0);
            //splash.DrawString("Starting...", Color.White, 20, splash.Height - 16 - 20);

            wm.Update(splash);
        }

        internal override void Run()
        {
            if (!ideCreated)
            {
                ide = new Ide(this, wm);
                ide.Start();
                wm.RemoveWindow(splash);
                ideCreated = true;
            }
        }
    }
}
