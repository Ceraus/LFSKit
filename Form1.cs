﻿using HandyStuff;
using PRAGMAsLayeredFSKit.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRAGMAsLayeredFSKit
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }
        private void sdfilesmodbutton_Click(object sender, EventArgs e) {
            MessageBox.Show(
                "This process will put a bunch of files onto your sd-card!\nIf you already have /atmoshpere, /switch folders and hekate_ipl.ini, hbmenu.nro, then this process may fail as files already exist.\n\n" +
                "This is mainly for users who needs the hekate-ipl romfs launch firmware option or for users that have no files at all in their sd yet.\n\n" +
                "If you want to make sure you are 100% ready with the newest files, Remove /atmosphere, /switch, /modules aswell as ALL \"files\" in the ROOT of your SD Card.\n" +
                "If you have the romfs hekate-ipl launch firmware option, and its working for you, this step is redundent."
            );
            MessageBox.Show("Insert your Switch's SDCard into your PC and then hit OK.");
            using (FolderBrowserDialog fbd = new FolderBrowserDialog()) {
                fbd.Description = "I will need to know the location of your SD Card's ROOT! (So if its D:/ drive, just select D:/ and hit OK)";
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                    string sdRoot = fbd.SelectedPath;
                    int loop = 0;
                    ImageConverter imgConverter = new ImageConverter();
                    foreach (string file in new string[] {
                        "atmosphere/",
                        "atmosphere/titles/",
                        "atmosphere/titles/010000000000100D/",
                        "atmosphere/titles/010000000000100D/exefs/",
                        "atmosphere/titles/010000000000100D/exefs/main",
                        "atmosphere/titles/010000000000100D/exefs/main.npdm",
                        "atmosphere/titles/010000000000100D/exefs/rtld.stub",
                        "modules/",
                        "modules/LayeredFS/",
                        "modules/LayeredFS/fs_mitm.kip",
                        "modules/loader.kip",
                        "modules/sm.kip",
                        "modules/nx-dreport.kip",
                        "switch/",
                        "switch/appstore/",
                        "switch/appstore/res/",
                        "switch/appstore/res/default.png",
                        "switch/appstore/res/GET.png",
                        "switch/appstore/res/icon.jpg",
                        "switch/appstore/res/icon.png",
                        "switch/appstore/res/icon_small.png",
                        "switch/appstore/res/INSTALLED.png",
                        "switch/appstore/res/LOCAL.png",
                        "switch/appstore/res/noscreen.png",
                        "switch/appstore/res/popup.png",
                        "switch/appstore/res/productsans.ttf",
                        "switch/appstore/res/shade.png",
                        "switch/appstore/res/UPDATE.png",
                        "switch/appstore/appstore.nro",
                        "switch/EdiZon/",
                        "switch/EdiZon/EdiZon.nacp",
                        "switch/EdiZon/EdiZon.nro",
                        "switch/GagOrder.nro",
                        "hbmenu.nro",
                        "hekate_ipl.ini"
                    }) {
                        string sdFile = Path.Combine(sdRoot, file);
                        if(file.EndsWith("/")) {
                            if (!Directory.Exists(sdFile)) {
                                Directory.CreateDirectory(sdFile);
                            }
                        } else {
                            loop++;
                            #region Get Resource
                            byte[] resource = null;
                            switch (loop) {
                                case 1:
                                    resource = Resources.main_;
                                    break;
                                case 2:
                                    resource = Resources.main_npdm;
                                    break;
                                case 3:
                                    resource = Resources.rtld;
                                    break;
                                case 4:
                                    resource = Resources.fs_mitm;
                                    break;
                                case 5:
                                    resource = Resources.fs_loader;
                                    break;
                                case 6:
                                    resource = Resources.fs_sm;
                                    break;
                                case 7:
                                    resource = Resources.nx_dreport;
                                    break;
                                case 8:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_default, typeof(byte[]));
                                    break;
                                case 9:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_GET, typeof(byte[]));
                                    break;
                                case 10:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_icon_jpg, typeof(byte[]));
                                    break;
                                case 11:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_icon_png, typeof(byte[]));
                                    break;
                                case 12:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_icon_small, typeof(byte[]));
                                    break;
                                case 13:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_INSTALLED, typeof(byte[]));
                                    break;
                                case 14:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_LOCAL, typeof(byte[]));
                                    break;
                                case 15:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_noscreen, typeof(byte[]));
                                    break;
                                case 16:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_popup, typeof(byte[]));
                                    break;
                                case 17:
                                    resource = Resources.as_productsans;
                                    break;
                                case 18:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_shade, typeof(byte[]));
                                    break;
                                case 19:
                                    resource = (byte[])imgConverter.ConvertTo(Resources.as_UPDATE, typeof(byte[]));
                                    break;
                                case 20:
                                    resource = Resources.appstore;
                                    break;
                                case 21:
                                    resource = Resources.EdiZon_nacp;
                                    break;
                                case 22:
                                    resource = Resources.EdiZon_nro;
                                    break;
                                case 23:
                                    resource = Resources.GagOrder;
                                    break;
                                case 24:
                                    resource = Resources.hbmenu;
                                    break;
                                case 25:
                                    resource = Encoding.ASCII.GetBytes(Resources.hekate_ipl);
                                    break;
                            }
                            #endregion
                            #region Save Resource to SD Location
                            File.WriteAllBytes(sdFile, resource);
                            #endregion
                        }
                    }
                    MessageBox.Show("Done!");
                }
            }
        }
        private void OpenKeyTutorialButton_Click(object sender, EventArgs e) {
            new KeyTutorial().Show();
        }
        private void OpenXCIDecrypterButton_Click(object sender, EventArgs e) {
            new XCIForm().Show();
        }
        private void button5_Click(object sender, EventArgs e) {
            new RCMMode().Show();
        }
    }
}