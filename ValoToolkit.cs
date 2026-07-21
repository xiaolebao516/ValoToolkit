using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace ValorantResolutionAssistant
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 1 && args[0] == "--enable-monitors")
            {
                RunMonitorAction(true);
                return;
            }
            if (args.Length == 1 && args[0] == "--disable-monitors")
            {
                RunMonitorAction(false);
                return;
            }
            if (args.Length == 1 && args[0] == "--disable-memory-integrity")
            {
                RunMemoryIntegrityAction();
                return;
            }
            if (args.Length == 1 && args[0] == "--optimize-ace")
            {
                RunAceOptimizationAction();
                return;
            }

            Application.Run(new MainForm());
        }

        private static void RunMonitorAction(bool enable)
        {
            try
            {
                if (!AdminHelper.IsAdministrator())
                {
                    MessageBox.Show(Texts.AdminRequired, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(MonitorDeviceManager.SetEnabled(enable), Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RunMemoryIntegrityAction()
        {
            try
            {
                if (!AdminHelper.IsAdministrator())
                {
                    MessageBox.Show(Texts.AdminRequired, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MemoryIntegrityManager.Disable();
                MessageBox.Show(Texts.MemoryIntegrityDone, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RunAceOptimizationAction()
        {
            try
            {
                if (!AdminHelper.IsAdministrator())
                {
                    MessageBox.Show(Texts.AdminRequired, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(ValorantOptimization.OptimizeAceProcesses(), Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    internal static class Texts
    {
        public const string AppName = "\u74e6\u5206\u8fa8\u7387\u52a9\u624b";
        public const string Monitor = "\u76d1\u89c6\u5668";
        public const string Common = "\u5e38\u7528";
        public const string Special = "\u7279\u6b8a";
        public const string Reset = "reset";
        public const string Help = "\u5e2e\u52a9";
        public const string HelpTitle = "\u4f7f\u7528\u8bf4\u660e";
        public const string SwitchFailed = "\u5207\u6362\u5931\u8d25";
        public const string ActionFailed = "\u64cd\u4f5c\u5931\u8d25";
        public const string AdminRequired = "\u8fd9\u4e2a\u64cd\u4f5c\u9700\u8981\u7ba1\u7406\u5458\u6743\u9650\u3002";
        public const string UnsupportedTitle = "\u5206\u8fa8\u7387\u672a\u6dfb\u52a0";
        public const string ConfirmDisable = "\u786e\u5b9a\u8981\u7981\u7528\u201c\u8bbe\u5907\u7ba1\u7406\u5668 > \u76d1\u89c6\u5668\u201d\u91cc\u7684\u76d1\u89c6\u5668\u8bbe\u5907\u5417\uff1f\u5c4f\u5e55\u53ef\u80fd\u4f1a\u77ed\u6682\u95ea\u70c1\u3002";
        public const string MonitorEnabled = "\u76d1\u89c6\u5668\u8bbe\u5907\u5df2\u542f\u7528\u3002";
        public const string MonitorDisabled = "\u76d1\u89c6\u5668\u8bbe\u5907\u5df2\u7981\u7528\u3002";
        public const string SpecialHint = "\u9700\u5148\u5728 NVIDIA \u63a7\u5236\u9762\u677f\u91cc\u521b\u5efa\u81ea\u5b9a\u4e49\u5206\u8fa8\u7387";
        public const string MemoryIntegrityDone = "\u5df2\u5199\u5165\u5185\u6838\u9694\u79bb\u8bbe\u7f6e\u3002\r\n\r\n\u8bf7\u91cd\u65b0\u542f\u52a8 Windows \u540e\u68c0\u67e5\u8bbe\u7f6e\u662f\u5426\u751f\u6548\u3002";
        public const string MemoryIntegrityConfirm = "\u5173\u95ed\u5185\u6838\u9694\u79bb\uff08\u5185\u5b58\u5b8c\u6574\u6027\uff09\u4f1a\u964d\u4f4e Windows \u7684\u5185\u6838\u4fdd\u62a4\u80fd\u529b\uff0c\u5e76\u53ef\u80fd\u9700\u8981\u91cd\u542f\u3002\r\n\r\n\u786e\u5b9a\u8981\u7ee7\u7eed\u5417\uff1f";
        public const string DxCacheConfirm = "\u786e\u5b9a\u8981\u5220\u9664\u4ee5\u4e0b NVIDIA DXCache \u6587\u4ef6\u5939\u5417\uff1f\r\n\r\n";
        public const string AceOptimizationDone = "\u5df2\u5904\u7406 ACE \u8fdb\u7a0b\uff1a";
        public const string AceOptimizationNone = "\u5f53\u524d\u672a\u627e\u5230 SGuard64.exe \u6216 SGuardSvc64.exe\u3002\r\n\r\n\u8bf7\u5148\u542f\u52a8\u65e0\u754f\u5951\u7ea6\u540e\u518d\u6267\u884c\u3002";
        public const string Optimization = "\u4f18\u5316";
        public const string OptimizationTitle = "\u4e00\u952e\u4f18\u5316";
        public const string OptimizationSubtitle = "\u9488\u5bf9\u65e0\u754f\u5951\u7ea6\u7684\u5e38\u7528\u8bbe\u7f6e";
        public const string HelpText =
            "\u7279\u6b8a 4:3\uff1a\u7981\u7528\u76d1\u89c6\u5668\u540e\uff0c\u5728\u6e38\u620f\u5185\u9009\u201c\u5168\u5c4f\u7a97\u53e3 + \u586b\u5145\u201d\uff0c\u518d\u9009\u7279\u6b8a\u5206\u8fa8\u7387\uff0c\u901a\u5e38\u4e00\u6b21\u8bbe\u7f6e\u53ef\u957f\u671f\u4f7f\u7528\u3002\r\n\r\n" +
            "\u5e38\u89c4 4:3\uff1a\u505a\u6cd5\u540c\u4e0a\uff0c\u4f46\u6bcf\u6b21\u91cd\u65b0\u8fdb\u5165\u5bf9\u5c40\u540e\uff0c\u901a\u5e38\u9700\u8981\u5148\u70b9 reset \u5207\u56de\u539f\u6bd4\u4f8b\uff0c\u518d\u70b9\u5bf9\u5e94 4:3\u3002\r\n\r\n" +
            "reset\uff1a\u81ea\u52a8\u9009\u62e9\u5f53\u524d\u663e\u793a\u5668\u652f\u6301\u7684\u6700\u9ad8 16:9 \u5206\u8fa8\u7387\u3002\r\n\r\n" +
            "\u7279\u6b8a\u5206\u8fa8\u7387\u5982\u679c\u6ca1\u6709\u5148\u5728 NVIDIA \u63a7\u5236\u9762\u677f\u521b\u5efa\uff0cWindows \u65e0\u6cd5\u76f4\u63a5\u5207\u6362\u3002";
    }

    internal static class Theme
    {
        public static readonly Color Background = Color.FromArgb(0x11, 0x13, 0x18);
        public static readonly Color Sidebar = Color.FromArgb(0x0B, 0x0D, 0x11);
        public static readonly Color CardBackground = Color.FromArgb(0x18, 0x1B, 0x22);
        public static readonly Color CardBorder = Color.FromArgb(0x25, 0x2A, 0x33);
        public static readonly Color ButtonBackground = Color.FromArgb(0x10, 0x13, 0x18);
        public static readonly Color ButtonBorder = Color.FromArgb(0x34, 0x3A, 0x45);
        public static readonly Color PrimaryText = Color.FromArgb(0xF2, 0xF3, 0xF5);
        public static readonly Color SecondaryText = Color.FromArgb(0x8D, 0x93, 0xA1);
        public static readonly Color Accent = Color.FromArgb(0xFF, 0x46, 0x55);
        public static readonly Color PreviewBackground = Color.FromArgb(0x20, 0x24, 0x2D);
    }

    public sealed class MainForm : Form
    {
        private readonly Font titleFont = new Font("Microsoft YaHei UI", 20f, FontStyle.Bold);
        private readonly Font pageTitleFont = new Font("Microsoft YaHei UI", 25f, FontStyle.Bold);
        private readonly Font uiFont = new Font("Microsoft YaHei UI", 10f, FontStyle.Regular);
        private readonly Font sectionFont = new Font("Microsoft YaHei UI", 13f, FontStyle.Bold);
        private readonly Font smallFont = new Font("Microsoft YaHei UI", 9f, FontStyle.Regular);
        private readonly Font navFont = new Font("Microsoft YaHei UI", 12f, FontStyle.Bold);
        private ToggleSwitch monitorToggle;
        private Label statusLabel;
        private readonly Panel resolutionPage;
        private readonly Panel crosshairPage;
        private readonly Panel optimizationPage;
        private readonly Panel helpPage;
        private readonly NavButton resolutionNav;
        private readonly NavButton crosshairNav;
        private readonly NavButton optimizationNav;
        private readonly NavButton helpNav;
        private PillButton copiedButton;
        private Timer copiedTimer;
        private string copiedOriginalText;
        private bool copiedOriginalAccent;
        private Point dragStart;

        public MainForm()
        {
            Text = "Valo Toolkit";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(760, 520);
            BackColor = Theme.Background;
            Font = uiFont;
            LoadAppIcon();

            var titleBar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, 36),
                BackColor = Theme.Background
            };
            titleBar.MouseDown += TitleBarMouseDown;
            titleBar.MouseMove += TitleBarMouseMove;
            Controls.Add(titleBar);

            var titleLabel = new Label
            {
                Text = "Valo Toolkit",
                ForeColor = Theme.SecondaryText,
                BackColor = Theme.Background,
                Font = smallFont,
                Location = new Point(14, 8),
                Size = new Size(140, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            titleLabel.MouseDown += TitleBarMouseDown;
            titleLabel.MouseMove += TitleBarMouseMove;
            titleBar.Controls.Add(titleLabel);

            var minimize = new TitleButton
            {
                Text = "-",
                Location = new Point(ClientSize.Width - 86, 0),
                Size = new Size(43, 36)
            };
            minimize.Click += delegate { WindowState = FormWindowState.Minimized; };
            titleBar.Controls.Add(minimize);

            var close = new TitleButton
            {
                Text = "\u00d7",
                CloseButton = true,
                Location = new Point(ClientSize.Width - 43, 0),
                Size = new Size(43, 36)
            };
            close.Click += delegate { Close(); };
            titleBar.Controls.Add(close);

            var sidebar = new Panel
            {
                Location = new Point(0, 36),
                Size = new Size(174, ClientSize.Height - 36),
                BackColor = Theme.Sidebar
            };
            sidebar.Paint += delegate(object sender, PaintEventArgs e)
            {
                using (var pen = new Pen(Theme.CardBorder))
                {
                    e.Graphics.DrawLine(pen, sidebar.Width - 1, 0, sidebar.Width - 1, sidebar.Height);
                }
            };
            Controls.Add(sidebar);

            sidebar.Controls.Add(new Label
            {
                Text = "VT",
                Font = titleFont,
                ForeColor = Theme.PrimaryText,
                BackColor = Theme.Sidebar,
                Location = new Point(20, 34),
                Size = new Size(140, 36),
                TextAlign = ContentAlignment.MiddleLeft
            });

            resolutionNav = new NavButton
            {
                Text = "4:3",
                Symbol = "\u25ad",
                Active = true,
                Location = new Point(12, 118),
                Size = new Size(150, 54),
                Font = navFont
            };
            resolutionNav.Click += delegate { ShowPage(resolutionPage, resolutionNav); };
            sidebar.Controls.Add(resolutionNav);

            crosshairNav = new NavButton
            {
                Text = "\u51c6\u661f",
                Symbol = "\u25ce",
                Location = new Point(12, 188),
                Size = new Size(150, 54),
                Font = navFont
            };
            crosshairNav.Click += delegate { ShowPage(crosshairPage, crosshairNav); };
            sidebar.Controls.Add(crosshairNav);

            optimizationNav = new NavButton
            {
                Text = Texts.Optimization,
                Symbol = "!",
                Location = new Point(12, 258),
                Size = new Size(150, 54),
                Font = navFont
            };
            optimizationNav.Click += delegate { ShowPage(optimizationPage, optimizationNav); };
            sidebar.Controls.Add(optimizationNav);

            helpNav = new NavButton
            {
                Text = "\u5e2e\u52a9",
                Symbol = "?",
                Location = new Point(12, 328),
                Size = new Size(150, 54),
                Font = navFont
            };
            helpNav.Click += delegate { ShowPage(helpPage, helpNav); };
            sidebar.Controls.Add(helpNav);

            resolutionPage = MakePage();
            crosshairPage = MakePage();
            optimizationPage = MakePage();
            helpPage = MakePage();
            Controls.Add(resolutionPage);
            Controls.Add(crosshairPage);
            Controls.Add(optimizationPage);
            Controls.Add(helpPage);

            BuildResolutionPage();
            BuildCrosshairPage();
            BuildOptimizationPage();
            BuildHelpPage();
            ShowPage(resolutionPage, resolutionNav);
        }

        private Panel MakePage()
        {
            return new Panel
            {
                Location = new Point(174, 36),
                Size = new Size(ClientSize.Width - 174, ClientSize.Height - 36),
                BackColor = Theme.Background,
                Visible = false
            };
        }

        private void LoadAppIcon()
        {
            var iconPath = System.IO.Path.Combine(Application.StartupPath, "Assets", "Icons", "app_icon.ico");
            if (System.IO.File.Exists(iconPath))
            {
                Icon = new Icon(iconPath);
                return;
            }

            var executableIcon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (executableIcon != null)
            {
                Icon = executableIcon;
            }
        }

        private void TitleBarMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragStart = e.Location;
            }
        }

        private void TitleBarMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - dragStart.X;
                Top += e.Y - dragStart.Y;
            }
        }

        private void ShowPage(Panel page, NavButton activeNav)
        {
            resolutionPage.Visible = page == resolutionPage;
            crosshairPage.Visible = page == crosshairPage;
            optimizationPage.Visible = page == optimizationPage;
            helpPage.Visible = page == helpPage;
            resolutionNav.Active = activeNav == resolutionNav;
            crosshairNav.Active = activeNav == crosshairNav;
            optimizationNav.Active = activeNav == optimizationNav;
            helpNav.Active = activeNav == helpNav;
            resolutionNav.Invalidate();
            crosshairNav.Invalidate();
            optimizationNav.Invalidate();
            helpNav.Invalidate();
        }

        private void BuildResolutionPage()
        {
            resolutionPage.Controls.Add(new Label
            {
                Text = "\u5206\u8fa8\u7387",
                Font = pageTitleFont,
                ForeColor = Theme.PrimaryText,
                BackColor = Theme.Background,
                Location = new Point(42, 30),
                Size = new Size(260, 48)
            });

            statusLabel = new Label
            {
                Text = "\u5f53\u524d\uff1a1920 x 1080",
                ForeColor = Theme.SecondaryText,
                BackColor = Theme.Background,
                Font = new Font("Microsoft YaHei UI", 13f, FontStyle.Regular),
                Location = new Point(45, 78),
                Size = new Size(260, 28)
            };
            resolutionPage.Controls.Add(statusLabel);

            var specialCard = new SoftPanel
            {
                Location = new Point(42, 134),
                Size = new Size(504, 110),
                Radius = 14
            };
            resolutionPage.Controls.Add(specialCard);

            specialCard.Controls.Add(MakeSectionLabel("\u7279\u6b8a 4:3", 20, 18, 180));
            AddResolutionButton(specialCard, "1568 x 1080", null, 1568, 1080, true, 20, 54, 142);
            AddResolutionButton(specialCard, "1280 x 882", null, 1280, 882, true, 181, 54, 142);
            var addCustom = MakeOutlineButton("\u6dfb\u52a0\u81ea\u5b9a\u4e49", 342, 54, 142, true);
            addCustom.Click += delegate { OpenNvidiaControlPanel(); };
            specialCard.Controls.Add(addCustom);

            var commonCard = new SoftPanel
            {
                Location = new Point(42, 268),
                Size = new Size(504, 110),
                Radius = 14
            };
            resolutionPage.Controls.Add(commonCard);

            commonCard.Controls.Add(MakeSectionLabel("\u5e38\u7528 4:3", 20, 18, 180));
            AddResolutionButton(commonCard, "1440 x 1080", null, 1440, 1080, false, 20, 54, 142);
            AddResolutionButton(commonCard, "1280 x 960", null, 1280, 960, false, 181, 54, 142);
            AddResolutionButton(commonCard, "1280 x 1024", null, 1280, 1024, false, 342, 54, 142);

            var reset = MakeOutlineButton("\u6062\u590d\u9ed8\u8ba4", 42, 414, 180, false);
            reset.Subtitle = null;
            reset.Click += delegate { ResetNativeResolution(); };
            resolutionPage.Controls.Add(reset);

            monitorToggle = new ToggleSwitch
            {
                Location = new Point(354, 424),
                Size = new Size(43, 24),
                Checked = MonitorDeviceManager.HasEnabledMonitor()
            };
            monitorToggle.SetChecked(monitorToggle.Checked, false);
            monitorToggle.Click += delegate { ToggleMonitor(); };
            resolutionPage.Controls.Add(new Label
            {
                Text = Texts.Monitor,
                ForeColor = Theme.PrimaryText,
                BackColor = Theme.Background,
                Font = uiFont,
                Location = new Point(276, 423),
                Size = new Size(76, 26),
                TextAlign = ContentAlignment.MiddleRight
            });
            resolutionPage.Controls.Add(monitorToggle);

            UpdateStatus();
        }

        private void BuildCrosshairPage()
        {
            crosshairPage.Controls.Add(new Label
            {
                Text = "\u51c6\u661f",
                Font = pageTitleFont,
                ForeColor = Theme.PrimaryText,
                BackColor = Theme.Background,
                Location = new Point(42, 30),
                Size = new Size(260, 48)
            });

            crosshairPage.Controls.Add(new Label
            {
                Text = "\u5feb\u901f\u590d\u5236\u5e38\u7528\u51c6\u661f",
                ForeColor = Theme.SecondaryText,
                BackColor = Theme.Background,
                Font = new Font("Microsoft YaHei UI", 13f, FontStyle.Regular),
                Location = new Point(45, 78),
                Size = new Size(300, 28)
            });

            var top = 130;
            foreach (var preset in CrosshairPreset.All)
            {
                AddCrosshairRow(top, preset);
                top += 72;
            }

            var moreCard = new SoftPanel
            {
                Location = new Point(42, 418),
                Size = new Size(504, 44),
                Radius = 14
            };
            crosshairPage.Controls.Add(moreCard);

            moreCard.Controls.Add(new Label
            {
                Text = "\u66f4\u591a\u51c6\u661f",
                ForeColor = Theme.PrimaryText,
                BackColor = Color.Transparent,
                Font = smallFont,
                Location = new Point(16, 11),
                Size = new Size(160, 22)
            });
            var copyLink = MakeOutlineButton("\u590d\u5236\u94fe\u63a5", 292, 4, 84, false);
            copyLink.Click += delegate { CopyCrosshairLink(copyLink); };
            moreCard.Controls.Add(copyLink);

            var openWebsite = MakeOutlineButton("\u6253\u5f00\u7f51\u7ad9", 386, 4, 100, false);
            openWebsite.Click += delegate { OpenCrosshairWebsite(); };
            moreCard.Controls.Add(openWebsite);
        }

        private void BuildOptimizationPage()
        {
            optimizationPage.Controls.Add(new Label
            {
                Text = Texts.OptimizationTitle,
                Font = pageTitleFont,
                ForeColor = Theme.PrimaryText,
                BackColor = Theme.Background,
                Location = new Point(42, 30),
                Size = new Size(260, 48)
            });

            optimizationPage.Controls.Add(new Label
            {
                Text = Texts.OptimizationSubtitle,
                ForeColor = Theme.SecondaryText,
                BackColor = Theme.Background,
                Font = new Font("Microsoft YaHei UI", 13f, FontStyle.Regular),
                Location = new Point(45, 78),
                Size = new Size(360, 28)
            });

            AddOptimizationCard(
                "ACE \u8fdb\u7a0b",
                "SGuard64 / SGuardSvc64 \u8bbe\u4e3a\u4f4e\u4f18\u5148\u7ea7\uff0c\u4ec5\u4f7f\u7528\u6700\u540e\u4e00\u4e2a CPU",
                "\u8bbe\u7f6e ACE",
                124,
                delegate { OptimizeAceProcesses(); });
            AddOptimizationCard(
                "\u5185\u6838\u9694\u79bb",
                "\u5173\u95ed\u5185\u5b58\u5b8c\u6574\u6027\uff0c\u9700\u8981\u7ba1\u7406\u5458\u6743\u9650\u548c\u91cd\u542f",
                "\u5173\u95ed\u5185\u6838\u9694\u79bb",
                208,
                delegate { DisableMemoryIntegrity(); });
            AddOptimizationCard(
                "ACLOS \u542f\u52a8\u5668",
                "\u81ea\u52a8\u67e5\u627e WeGameApps \u4e2d\u7684 aclos-launcher.exe \u5e76\u7981\u7528\u5168\u5c4f\u4f18\u5316",
                "\u8bbe\u7f6e\u542f\u52a8\u5668",
                292,
                delegate { ConfigureAclosLauncher(); });
            AddOptimizationCard(
                "NVIDIA DXCache",
                "\u5220\u9664\u7528\u6237 LocalLow\\NVIDIA\\DXCache \u7f13\u5b58\u6587\u4ef6\u5939",
                "\u6e05\u7406 DXCache",
                376,
                delegate { ClearDxCache(); });
        }

        private void AddOptimizationCard(string title, string description, string buttonText, int top, EventHandler handler)
        {
            var card = new SoftPanel
            {
                Location = new Point(42, top),
                Size = new Size(504, 72),
                Radius = 14
            };
            optimizationPage.Controls.Add(card);
            card.Controls.Add(new Label
            {
                Text = title,
                ForeColor = Theme.PrimaryText,
                BackColor = Color.Transparent,
                Font = sectionFont,
                Location = new Point(18, 10),
                Size = new Size(300, 24)
            });
            card.Controls.Add(new Label
            {
                Text = description,
                ForeColor = Theme.SecondaryText,
                BackColor = Color.Transparent,
                Font = smallFont,
                Location = new Point(18, 37),
                Size = new Size(304, 28)
            });
            var button = MakeOutlineButton(buttonText, 334, 18, 150, true);
            button.Click += handler;
            card.Controls.Add(button);
        }

        private void BuildHelpPage()
        {
            helpPage.Controls.Add(new Label
            {
                Text = "\u5e2e\u52a9",
                Font = pageTitleFont,
                ForeColor = Theme.PrimaryText,
                BackColor = Theme.Background,
                Location = new Point(42, 30),
                Size = new Size(260, 48)
            });

            helpPage.Controls.Add(new Label
            {
                Text = "\u4f7f\u7528\u8bf4\u660e",
                ForeColor = Theme.SecondaryText,
                BackColor = Theme.Background,
                Font = new Font("Microsoft YaHei UI", 13f, FontStyle.Regular),
                Location = new Point(45, 78),
                Size = new Size(300, 28)
            });

            var helpCard = new SoftPanel
            {
                Location = new Point(42, 130),
                Size = new Size(504, 310),
                Radius = 14
            };
            helpPage.Controls.Add(helpCard);

            helpCard.Controls.Add(new Label
            {
                Text = Texts.HelpText,
                ForeColor = Theme.PrimaryText,
                BackColor = Color.Transparent,
                Font = uiFont,
                Location = new Point(22, 22),
                Size = new Size(460, 266)
            });
        }

        private void AddCrosshairRow(int top, CrosshairPreset preset)
        {
            var row = new SoftPanel
            {
                Location = new Point(42, top),
                Size = new Size(504, 68),
                Radius = 14
            };
            crosshairPage.Controls.Add(row);

            row.Controls.Add(new CrosshairPreview
            {
                PreviewImage = CrosshairImageLoader.Load(preset.ImageFileName),
                Location = new Point(12, 10),
                Size = new Size(72, 48)
            });
            row.Controls.Add(new Label
            {
                Text = preset.Name,
                ForeColor = Theme.PrimaryText,
                BackColor = Color.Transparent,
                Font = sectionFont,
                Location = new Point(108, 12),
                Size = new Size(220, 24)
            });
            row.Controls.Add(new Label
            {
                Text = preset.Subtitle,
                ForeColor = Theme.SecondaryText,
                BackColor = Color.Transparent,
                Font = uiFont,
                Location = new Point(108, 38),
                Size = new Size(220, 22)
            });
            var copy = MakeOutlineButton("\u590d\u5236\u4ee3\u7801", 384, 16, 96, true);
            copy.Click += delegate { CopyCrosshairCode(preset, copy); };
            row.Controls.Add(copy);
        }

        private void CopyCrosshairCode(CrosshairPreset preset, PillButton button)
        {
            Clipboard.SetText(preset.Code);
            ShowCopiedButton(button, "\u590d\u5236\u4ee3\u7801", true);
        }

        private void CopyCrosshairLink(PillButton button)
        {
            Clipboard.SetText(CrosshairPreset.WebsiteUrl);
            ShowCopiedButton(button, "\u590d\u5236\u94fe\u63a5", false);
        }

        private void OpenCrosshairWebsite()
        {
            Process.Start(new ProcessStartInfo(CrosshairPreset.WebsiteUrl)
            {
                UseShellExecute = true
            });
        }

        private void ShowCopiedButton(PillButton button, string originalText, bool accent)
        {
            ResetCopiedButton();
            button.Text = "\u5df2\u590d\u5236!";
            button.BorderColor = Theme.ButtonBorder;
            button.TextColor = Theme.SecondaryText;
            button.Invalidate();
            copiedButton = button;
            copiedOriginalText = originalText;
            copiedOriginalAccent = accent;

            copiedTimer = new Timer { Interval = 2000 };
            copiedTimer.Tick += delegate
            {
                ResetCopiedButton();
            };
            copiedTimer.Start();
        }

        private void ResetCopiedButton()
        {
            if (copiedTimer != null)
            {
                copiedTimer.Stop();
                copiedTimer.Dispose();
                copiedTimer = null;
            }
            if (copiedButton == null)
            {
                return;
            }

            copiedButton.Text = copiedOriginalText;
            copiedButton.BorderColor = copiedOriginalAccent ? Theme.Accent : Theme.ButtonBorder;
            copiedButton.TextColor = copiedOriginalAccent ? Theme.Accent : Theme.PrimaryText;
            copiedButton.Invalidate();
            copiedButton = null;
            copiedOriginalText = null;
            copiedOriginalAccent = false;
        }

        private Label MakeSectionLabel(string text, int left, int top, int width)
        {
            return new Label
            {
                Text = text,
                Font = sectionFont,
                ForeColor = Theme.PrimaryText,
                BackColor = Color.Transparent,
                Location = new Point(left, top),
                Size = new Size(width, 24),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void AddResolutionButton(Control parent, string text, string subtitle, int width, int height, bool customMode, int left, int top, int buttonWidth)
        {
            var button = new PillButton
            {
                Text = text,
                Subtitle = subtitle,
                Location = new Point(left, top),
                Size = new Size(buttonWidth, 46),
                FillColor = Theme.ButtonBackground,
                BorderColor = Theme.ButtonBorder,
                TextColor = Theme.PrimaryText,
                Radius = 8,
                Font = uiFont,
                Tag = new ResolutionTarget(width, height, customMode)
            };
            button.Click += delegate(object sender, EventArgs args)
            {
                var target = (ResolutionTarget)((Control)sender).Tag;
                SwitchResolution(target.Width, target.Height, target.CustomMode);
            };
            parent.Controls.Add(button);
        }

        private PillButton MakeOutlineButton(string text, int left, int top, int width, bool accent)
        {
            return new PillButton
            {
                Text = text,
                Location = new Point(left, top),
                Size = new Size(width, 40),
                FillColor = Theme.ButtonBackground,
                BorderColor = accent ? Theme.Accent : Theme.ButtonBorder,
                TextColor = accent ? Theme.Accent : Theme.PrimaryText,
                Radius = 8,
                Font = uiFont
            };
        }

        private void OpenNvidiaControlPanel()
        {
            if (TryStartProcess("explorer.exe", @"shell:AppsFolder\NVIDIACorp.NVIDIAControlPanel_56jybvy8sckqj!NVIDIACorp.NVIDIAControlPanel"))
            {
                return;
            }

            var installedPath = @"C:\Program Files\NVIDIA Corporation\Control Panel Client\nvcplui.exe";
            if (TryStartProcess(installedPath) || TryStartProcess("nvcplui.exe"))
            {
                return;
            }

            MessageBox.Show(
                this,
                "\u672a\u627e\u5230 NVIDIA \u63a7\u5236\u9762\u677f\uff0c\u8bf7\u624b\u52a8\u53f3\u952e\u684c\u9762\u6253\u5f00 NVIDIA \u63a7\u5236\u9762\u677f / NVIDIA App\u3002",
                Texts.AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool TryStartProcess(string fileName)
        {
            try
            {
                Process.Start(new ProcessStartInfo(fileName)
                {
                    UseShellExecute = true
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryStartProcess(string fileName, string arguments)
        {
            try
            {
                Process.Start(new ProcessStartInfo(fileName, arguments)
                {
                    UseShellExecute = true
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void OptimizeAceProcesses()
        {
            try
            {
                if (!AdminHelper.IsAdministrator())
                {
                    Process.Start(new ProcessStartInfo(Application.ExecutablePath, "--optimize-ace")
                    {
                        UseShellExecute = true,
                        Verb = "runas"
                    });
                    ShowStatus("\u5df2\u8bf7\u6c42\u7ba1\u7406\u5458\u6743\u9650");
                    return;
                }

                var result = ValorantOptimization.OptimizeAceProcesses();
                MessageBox.Show(this, result, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowStatus(Texts.AceOptimizationDone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableMemoryIntegrity()
        {
            if (MessageBox.Show(this, Texts.MemoryIntegrityConfirm, Texts.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (AdminHelper.IsAdministrator())
                {
                    MemoryIntegrityManager.Disable();
                    MessageBox.Show(this, Texts.MemoryIntegrityDone, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Process.Start(new ProcessStartInfo(Application.ExecutablePath, "--disable-memory-integrity")
                {
                    UseShellExecute = true,
                    Verb = "runas"
                });
                ShowStatus("\u5df2\u8bf7\u6c42\u7ba1\u7406\u5458\u6743\u9650");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureAclosLauncher()
        {
            try
            {
                var launcherPath = ValorantOptimization.ConfigureAclosLauncher();
                MessageBox.Show(this, "\u5df2\u4e3a\u4ee5\u4e0b\u7a0b\u5e8f\u7981\u7528\u5168\u5c4f\u4f18\u5316\uff1a\r\n\r\n" + launcherPath, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowStatus("ACLOS \u542f\u52a8\u5668\u5df2\u8bbe\u7f6e");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDxCache()
        {
            var cachePath = ValorantOptimization.GetDxCachePath();
            if (MessageBox.Show(this, Texts.DxCacheConfirm + cachePath, Texts.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var result = ValorantOptimization.ClearDxCache();
                MessageBox.Show(this, result, Texts.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowStatus("DXCache \u5df2\u5904\u7406");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SwitchResolution(int width, int height, bool customMode)
        {
            try
            {
                DisplayApi.SetResolution(width, height, 0, customMode);
                UpdateStatus();
            }
            catch (UnsupportedResolutionException ex)
            {
                MessageBox.Show(this, ex.Message, Texts.UnsupportedTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Texts.SwitchFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetNativeResolution()
        {
            try
            {
                var mode = DisplayApi.GetBestNativeResolution();
                DisplayApi.SetResolution(mode.Width, mode.Height, mode.Frequency, false);
                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Texts.SwitchFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToggleMonitor()
        {
            var next = !monitorToggle.Checked;
            monitorToggle.SetChecked(next, true);
            LaunchMonitorAction(next);
        }

        private void LaunchMonitorAction(bool enable)
        {
            try
            {
                if (AdminHelper.IsAdministrator())
                {
                    MonitorDeviceManager.SetEnabled(enable);
                    ShowStatus(enable ? Texts.MonitorEnabled : Texts.MonitorDisabled);
                    return;
                }

                Process.Start(new ProcessStartInfo(Application.ExecutablePath, enable ? "--enable-monitors" : "--disable-monitors")
                {
                    UseShellExecute = true,
                    Verb = "runas"
                });
            }
            catch (Exception ex)
            {
                monitorToggle.SetChecked(!enable, true);
                MessageBox.Show(this, ex.Message, Texts.ActionFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatus()
        {
            var bounds = Screen.PrimaryScreen.Bounds;
            statusLabel.Text = "\u5f53\u524d\uff1a" + bounds.Width + "x" + bounds.Height;
        }

        private void ShowStatus(string message)
        {
            statusLabel.Text = message;
        }
    }

    internal sealed class NavButton : Control
    {
        private bool hovering;

        public string Symbol { get; set; }
        public bool Active { get; set; }

        public NavButton()
        {
            Symbol = "";
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovering = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovering = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var fill = Active ? Theme.CardBackground : hovering ? Color.FromArgb(18, 21, 27) : Theme.Sidebar;
            using (var path = Drawing.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 8))
            using (var brush = new SolidBrush(fill))
            using (var borderPen = new Pen(Active ? Color.FromArgb(75, 46, 54) : Theme.Sidebar, 1f))
            using (var textBrush = new SolidBrush(Active ? Theme.PrimaryText : Color.FromArgb(210, 213, 220)))
            using (var accentBrush = new SolidBrush(Theme.Accent))
            using (var symbolFont = new Font("Microsoft YaHei UI", 18f, FontStyle.Bold))
            using (var symbolBrush = new SolidBrush(Active ? Theme.Accent : Color.FromArgb(210, 213, 220)))
            using (var textFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
            using (var symbolFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(borderPen, path);
                if (Active)
                {
                    e.Graphics.FillRectangle(accentBrush, 0, 8, 4, Height - 16);
                }

                e.Graphics.DrawString(Symbol, symbolFont, symbolBrush, new Rectangle(16, 0, 30, Height), symbolFormat);
                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(56, 0, Width - 60, Height), textFormat);
            }
        }
    }

    internal sealed class TitleButton : Control
    {
        private bool hovering;

        public bool CloseButton { get; set; }

        public TitleButton()
        {
            Cursor = Cursors.Hand;
            Font = new Font("Microsoft YaHei UI", 12f, FontStyle.Regular);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovering = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovering = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var background = hovering
                ? CloseButton ? Theme.Accent : Theme.CardBackground
                : Theme.Background;
            using (var brush = new SolidBrush(background))
            using (var textBrush = new SolidBrush(Theme.PrimaryText))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
                e.Graphics.DrawString(Text, Font, textBrush, ClientRectangle, format);
            }
        }
    }

    internal sealed class CrosshairPreview : Control
    {
        private Image previewImage;

        public Image PreviewImage
        {
            set
            {
                if (previewImage != null)
                {
                    previewImage.Dispose();
                    previewImage = null;
                }
                previewImage = value;
                Invalidate();
            }
        }

        public CrosshairPreview()
        {
            BackColor = Theme.CardBackground;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (var path = Drawing.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 7))
            using (var brush = new SolidBrush(Theme.PreviewBackground))
            using (var pen = new Pen(Theme.ButtonBorder, 1f))
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
                if (previewImage != null)
                {
                    e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    var maxWidth = Width - 18;
                    var maxHeight = Height - 14;
                    var scale = Math.Max(1, Math.Min(maxWidth / previewImage.Width, maxHeight / previewImage.Height));
                    var imageWidth = previewImage.Width * scale;
                    var imageHeight = previewImage.Height * scale;
                    var imageRect = new Rectangle((Width - imageWidth) / 2, (Height - imageHeight) / 2, imageWidth, imageHeight);
                    e.Graphics.DrawImage(previewImage, imageRect);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && previewImage != null)
            {
                previewImage.Dispose();
                previewImage = null;
            }
            base.Dispose(disposing);
        }
    }

    internal static class CrosshairImageLoader
    {
        private const string ResourcePrefix = "ValorantResolutionAssistant.Assets.Crosshairs.";

        public static Image Load(string fileName)
        {
            var assetsPath = System.IO.Path.Combine(Application.StartupPath, "Assets", "Crosshairs", fileName);
            if (System.IO.File.Exists(assetsPath))
            {
                return Image.FromFile(assetsPath);
            }

            using (var stream = typeof(CrosshairImageLoader).Assembly.GetManifestResourceStream(ResourcePrefix + fileName))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var image = Image.FromStream(stream))
                {
                    return new Bitmap(image);
                }
            }
        }
    }

    internal sealed class CrosshairPreset
    {
        public const string WebsiteUrl = "https://www.vcrdb.net/";

        public static readonly CrosshairPreset[] All =
        {
            new CrosshairPreset(
                "\u7ecf\u5178\u5341\u5b57",
                "\u7ecf\u5178\u5341\u5b57",
                "classic_cross.png",
                "0;s;1;P;u;1B29C1FF;h;0;f;0;0l;3;0v;3;0o;0;0a;1;0f;0;1b;0;S;c;0;s;0.909;o;1"),
            new CrosshairPreset(
                "\u7ecf\u5178\u7ea2\u70b9",
                "\u7ecf\u5178\u7ea2\u70b9",
                "classic_red_dot.png",
                "0;p;0;s;1;P;c;7;u;000000FF;o;1;d;1;f;0;0b;0;1b;0;A;o;1;d;1;z;1;0b;0;1b;0;S;t;000000FF;s;1.288;o;1"),
            new CrosshairPreset(
                "\u7a7a\u5fc3\u5341\u5b57",
                "\u7a7a\u5fc3\u5341\u5b57",
                "hollow_cross.png",
                "0;P;c;2;u;000000FF;h;0;f;0;0l;4;0v;4;0o;2;0a;1;0f;0;1b;0"),
            new CrosshairPreset(
                "\u6241\u5e73\u51c6\u661f",
                "\u6241\u5e73\u51c6\u661f",
                "flat_crosshair.png",
                "0;s;1;P;f;0;0t;1;0l;4;0v;2;0g;1;0o;0;0a;1;0f;0;1b;0")
        };

        public readonly string Name;
        public readonly string Subtitle;
        public readonly string ImageFileName;
        public readonly string Code;

        private CrosshairPreset(string name, string subtitle, string imageFileName, string code)
        {
            Name = name;
            Subtitle = subtitle;
            ImageFileName = imageFileName;
            Code = code;
        }
    }

    internal sealed class PillButton : Control
    {
        private bool hovering;
        private bool pressed;

        public string Subtitle { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }
        public Color TextColor { get; set; }
        public int Radius { get; set; }

        public PillButton()
        {
            FillColor = Theme.ButtonBackground;
            BorderColor = Theme.ButtonBorder;
            TextColor = Theme.PrimaryText;
            Radius = 8;
            Font = new Font("Microsoft YaHei UI", 9.5f);
            BackColor = Theme.CardBackground;
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovering = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovering = false;
            pressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            pressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var fill = pressed ? Blend(FillColor, Color.Black, 0.04f) : hovering ? Blend(FillColor, Color.Black, 0.02f) : FillColor;
            using (var path = Drawing.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), Radius))
            using (var brush = new SolidBrush(fill))
            using (var pen = new Pen(BorderColor, 1f))
            using (var textBrush = new SolidBrush(TextColor))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = string.IsNullOrEmpty(Subtitle) ? StringAlignment.Center : StringAlignment.Near })
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
                if (string.IsNullOrEmpty(Subtitle))
                {
                    e.Graphics.DrawString(Text, Font, textBrush, ClientRectangle, format);
                }
                else
                {
                    var titleRect = new Rectangle(0, 7, Width, 18);
                    var subtitleRect = new Rectangle(0, 24, Width, 14);
                    using (var subtitleFont = new Font("Microsoft YaHei UI", 7.6f))
                    using (var subtitleBrush = new SolidBrush(Theme.SecondaryText))
                    using (var center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        e.Graphics.DrawString(Text, Font, textBrush, titleRect, center);
                        e.Graphics.DrawString(Subtitle, subtitleFont, subtitleBrush, subtitleRect, center);
                    }
                }
            }
        }

        private static Color Blend(Color a, Color b, float amount)
        {
            return Color.FromArgb(
                a.A,
                (int)(a.R + (b.R - a.R) * amount),
                (int)(a.G + (b.G - a.G) * amount),
                (int)(a.B + (b.B - a.B) * amount));
        }
    }

    internal sealed class SoftPanel : Panel
    {
        public int Radius { get; set; }

        public SoftPanel()
        {
            BackColor = Color.Transparent;
            Radius = 14;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (var path = Drawing.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), Radius))
            using (var brush = new SolidBrush(Theme.CardBackground))
            using (var pen = new Pen(Theme.CardBorder, 1f))
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }
    }

    internal sealed class ToggleSwitch : Control
    {
        private readonly Timer timer;
        private float position;
        private bool targetChecked;

        public bool Checked { get; set; }

        public ToggleSwitch()
        {
            Size = new Size(54, 30);
            Cursor = Cursors.Hand;
            BackColor = Theme.Background;
            position = 1f;
            targetChecked = true;
            timer = new Timer { Interval = 15 };
            timer.Tick += delegate { Animate(); };
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        public void SetChecked(bool value, bool animate)
        {
            Checked = value;
            targetChecked = value;
            if (animate)
            {
                timer.Start();
            }
            else
            {
                position = value ? 1f : 0f;
                Invalidate();
            }
        }

        private void Animate()
        {
            var target = targetChecked ? 1f : 0f;
            position += (target - position) * 0.32f;
            if (Math.Abs(target - position) < 0.02f)
            {
                position = target;
                timer.Stop();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var trackColor = Color.FromArgb(
                (int)(Theme.ButtonBorder.R + (Theme.Accent.R - Theme.ButtonBorder.R) * position),
                (int)(Theme.ButtonBorder.G + (Theme.Accent.G - Theme.ButtonBorder.G) * position),
                (int)(Theme.ButtonBorder.B + (Theme.Accent.B - Theme.ButtonBorder.B) * position));
            using (var track = Drawing.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), Height / 2))
            using (var trackBrush = new SolidBrush(trackColor))
            using (var borderPen = new Pen(Theme.ButtonBorder, 1f))
            {
                e.Graphics.FillPath(trackBrush, track);
                e.Graphics.DrawPath(borderPen, track);
            }

            var knobSize = Height - 6;
            var knobX = 3 + (Width - knobSize - 6) * position;
            using (var knobBrush = new SolidBrush(Theme.PrimaryText))
            using (var knobPen = new Pen(Theme.ButtonBorder, 1f))
            {
                e.Graphics.FillEllipse(knobBrush, knobX, 3, knobSize, knobSize);
                e.Graphics.DrawEllipse(knobPen, knobX, 3, knobSize, knobSize);
            }
        }
    }

    internal static class Drawing
    {
        public static GraphicsPath RoundRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            var diameter = radius * 2;
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    internal sealed class ResolutionTarget
    {
        public readonly int Width;
        public readonly int Height;
        public readonly bool CustomMode;

        public ResolutionTarget(int width, int height, bool customMode)
        {
            Width = width;
            Height = height;
            CustomMode = customMode;
        }
    }

    internal sealed class UnsupportedResolutionException : Exception
    {
        public UnsupportedResolutionException(string message) : base(message)
        {
        }
    }

    internal sealed class DisplayMode
    {
        public readonly int Width;
        public readonly int Height;
        public readonly int Frequency;

        public DisplayMode(int width, int height, int frequency)
        {
            Width = width;
            Height = height;
            Frequency = frequency;
        }
    }

    internal static class AdminHelper
    {
        public static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }

    internal static class ValorantOptimization
    {
        private static readonly string[] AceProcessNames = { "SGuard64", "SGuardSvc64" };

        public static string OptimizeAceProcesses()
        {
            var processorCount = Environment.ProcessorCount;
            var lastProcessor = processorCount - 1;
            if (lastProcessor < 0 || lastProcessor >= IntPtr.Size * 8)
            {
                throw new InvalidOperationException("\u5f53\u524d\u5904\u7406\u5668\u6570\u91cf\u8d85\u51fa\u53ef\u8bbe\u7f6e\u7684\u8303\u56f4\u3002");
            }

            var mask = IntPtr.Size == 8
                ? new IntPtr(1L << lastProcessor)
                : new IntPtr(1 << lastProcessor);
            var results = new List<string>();
            var found = 0;

            foreach (var processName in AceProcessNames)
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    using (process)
                    {
                        found++;
                        try
                        {
                            process.ProcessorAffinity = mask;
                            if (process.ProcessorAffinity != mask)
                            {
                                throw new InvalidOperationException("\u5904\u7406\u5668\u5173\u8054\u6027\u672a\u80fd\u5199\u5165\u76ee\u6807 CPU " + lastProcessor + "\u3002");
                            }

                            process.PriorityClass = ProcessPriorityClass.BelowNormal;
                            results.Add(process.ProcessName + " (PID " + process.Id + ") \u5df2\u7ed1\u5b9a CPU " + lastProcessor);
                        }
                        catch (Exception ex)
                        {
                            results.Add(processName + " (PID " + process.Id + ") \u5931\u8d25\uff1a" + ex.Message);
                        }
                    }
                }
            }

            if (found == 0)
            {
                return Texts.AceOptimizationNone;
            }

            return Texts.AceOptimizationDone + Environment.NewLine + string.Join(Environment.NewLine, results.ToArray());
        }

        public static string ConfigureAclosLauncher()
        {
            var launcherPath = FindAclosLauncher();
            if (launcherPath == null)
            {
                throw new System.IO.FileNotFoundException("\u672a\u5728\u5404\u78c1\u76d8\u7684 WeGameApps\\rail_apps \u4e2d\u627e\u5230 aclos-launcher.exe\u3002");
            }

            using (var layers = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"))
            {
                if (layers == null)
                {
                    throw new InvalidOperationException("\u65e0\u6cd5\u6253\u5f00 Windows \u5e94\u7528\u517c\u5bb9\u6027\u8bbe\u7f6e\u3002");
                }

                var current = Convert.ToString(layers.GetValue(launcherPath, ""));
                if (current.IndexOf("DISABLEDXMAXIMIZEDWINDOWEDMODE", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    current = string.IsNullOrWhiteSpace(current)
                        ? "~ DISABLEDXMAXIMIZEDWINDOWEDMODE"
                        : current + " DISABLEDXMAXIMIZEDWINDOWEDMODE";
                    layers.SetValue(launcherPath, current, RegistryValueKind.String);
                }
            }

            return launcherPath;
        }

        private static string FindAclosLauncher()
        {
            foreach (var drive in System.IO.DriveInfo.GetDrives())
            {
                if (!drive.IsReady || drive.DriveType != System.IO.DriveType.Fixed)
                {
                    continue;
                }

                try
                {
                    var railAppsPath = System.IO.Path.Combine(drive.RootDirectory.FullName, "WeGameApps", "rail_apps");
                    if (!System.IO.Directory.Exists(railAppsPath))
                    {
                        continue;
                    }

                    foreach (var gamePath in System.IO.Directory.GetDirectories(railAppsPath))
                    {
                        var launcherPath = System.IO.Path.Combine(gamePath, "ACLOS", "aclos-launcher.exe");
                        if (System.IO.File.Exists(launcherPath))
                        {
                            return launcherPath;
                        }
                    }
                }
                catch (System.IO.IOException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            return null;
        }

        public static string GetDxCachePath()
        {
            return System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "AppData", "LocalLow", "NVIDIA", "DXCache");
        }

        public static string ClearDxCache()
        {
            var cachePath = GetDxCachePath();
            if (!System.IO.Directory.Exists(cachePath))
            {
                return "\u672a\u627e\u5230 DXCache\uff0c\u65e0\u9700\u6e05\u7406\u3002";
            }

            System.IO.Directory.Delete(cachePath, true);
            return "\u5df2\u5220\u9664 DXCache\uff1a\r\n" + cachePath;
        }
    }

    internal static class MemoryIntegrityManager
    {
        private const string RegistryPath = @"SYSTEM\CurrentControlSet\Control\DeviceGuard\Scenarios\HypervisorEnforcedCodeIntegrity";

        public static void Disable()
        {
            using (var key = Registry.LocalMachine.CreateSubKey(RegistryPath))
            {
                if (key == null)
                {
                    throw new InvalidOperationException("\u65e0\u6cd5\u8bbf\u95ee Windows \u5185\u6838\u9694\u79bb\u8bbe\u7f6e\u3002");
                }

                key.SetValue("Enabled", 0, RegistryValueKind.DWord);
            }
        }
    }

    internal static class MonitorDeviceManager
    {
        public static bool HasEnabledMonitor()
        {
            var devices = GetMonitorDevices();
            foreach (var device in devices)
            {
                var code = device["ConfigManagerErrorCode"];
                if (code != null && Convert.ToInt32(code) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static string SetEnabled(bool enable)
        {
            var devices = GetMonitorDevices();
            if (devices.Count == 0)
            {
                return "\u6ca1\u6709\u627e\u5230\u76d1\u89c6\u5668\u8bbe\u5907\u3002";
            }

            var success = 0;
            var failures = new List<string>();
            foreach (var device in devices)
            {
                try
                {
                    if (TryPnPUtil(device, enable))
                    {
                        success++;
                    }
                    else
                    {
                        failures.Add(GetName(device) + ": pnputil failed");
                    }
                }
                catch (Exception ex)
                {
                    failures.Add(GetName(device) + ": " + ex.Message);
                }
            }

            var action = enable ? "\u542f\u7528" : "\u7981\u7528";
            var result = "\u5df2" + action + " " + success + " \u4e2a\u76d1\u89c6\u5668\u8bbe\u5907\u3002";
            if (failures.Count == 0)
            {
                return result;
            }

            return result + Environment.NewLine + string.Join(Environment.NewLine, failures.ToArray());
        }

        private static List<ManagementObject> GetMonitorDevices()
        {
            using (var searcher = new ManagementObjectSearcher(new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE PNPClass = 'Monitor'")))
            {
                return searcher.Get().Cast<ManagementObject>().ToList();
            }
        }

        private static bool TryPnPUtil(ManagementObject device, bool enable)
        {
            var id = Convert.ToString(device["PNPDeviceID"]);
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var arguments = (enable ? "/enable-device " : "/disable-device ") + Quote(id);
            if (!enable)
            {
                arguments += " /force";
            }

            using (var process = new Process())
            {
                process.StartInfo.FileName = "pnputil.exe";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
        }

        private static string GetName(ManagementObject device)
        {
            var name = Convert.ToString(device["Name"]);
            return string.IsNullOrWhiteSpace(name) ? Convert.ToString(device["PNPDeviceID"]) : name;
        }

        private static string Quote(string value)
        {
            return "\"" + value.Replace("\"", "\\\"") + "\"";
        }
    }

    internal static class DisplayApi
    {
        private const int CDS_UPDATEREGISTRY = 0x00000001;
        private const int CDS_TEST = 0x00000002;
        private const int DISP_CHANGE_SUCCESSFUL = 0;
        private const int DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001;
        private const int DISPLAY_DEVICE_PRIMARY_DEVICE = 0x00000004;
        private const int DM_PELSWIDTH = 0x00080000;
        private const int DM_PELSHEIGHT = 0x00100000;
        private const int DM_DISPLAYFREQUENCY = 0x00400000;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DISPLAY_DEVICE
        {
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            public int StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern bool EnumDisplayDevices(string lpDevice, int iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, int dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern int ChangeDisplaySettingsEx(string deviceName, ref DEVMODE devMode, IntPtr hwnd, int flags, IntPtr lParam);

        public static DisplayMode GetBestNativeResolution()
        {
            var modes = EnumerateModes(GetPrimaryDisplayName())
                .Where(m => Math.Abs((m.Width * 9) - (m.Height * 16)) <= Math.Max(16, m.Width / 80))
                .OrderByDescending(m => m.Width * m.Height)
                .ThenByDescending(m => m.Frequency)
                .ToList();

            if (modes.Count == 0)
            {
                throw new InvalidOperationException("\u6ca1\u6709\u627e\u5230\u53ef\u7528\u7684 16:9 \u7cfb\u7edf\u9ed8\u8ba4\u5206\u8fa8\u7387\u3002");
            }

            return modes[0];
        }

        public static void SetResolution(int width, int height, int frequency, bool customMode)
        {
            var deviceName = GetPrimaryDisplayName();
            var mode = FindDisplayMode(deviceName, width, height, frequency);
            if (mode.dmSize == 0 && customMode)
            {
                throw new UnsupportedResolutionException(width + " x " + height + " \u8fd8\u4e0d\u662f Windows \u53ef\u7528\u7684\u663e\u793a\u6a21\u5f0f\u3002\r\n\r\n" + Texts.SpecialHint);
            }
            if (mode.dmSize == 0)
            {
                mode = NewDevMode();
            }

            mode.dmPelsWidth = width;
            mode.dmPelsHeight = height;
            mode.dmFields = DM_PELSWIDTH | DM_PELSHEIGHT;

            if (frequency > 0)
            {
                mode.dmDisplayFrequency = frequency;
                mode.dmFields |= DM_DISPLAYFREQUENCY;
            }

            var testResult = deviceName != null
                ? ChangeDisplaySettingsEx(deviceName, ref mode, IntPtr.Zero, CDS_TEST, IntPtr.Zero)
                : ChangeDisplaySettings(ref mode, CDS_TEST);
            if (testResult != DISP_CHANGE_SUCCESSFUL)
            {
                var message = "Windows \u62d2\u7edd\u5207\u6362\u5230 " + width + " x " + height + "\u3002\u8fd4\u56de\u503c\uff1a" + testResult;
                if (customMode)
                {
                    message += "\r\n\r\n" + Texts.SpecialHint;
                    throw new UnsupportedResolutionException(message);
                }
                throw new InvalidOperationException(message);
            }

            var result = deviceName != null
                ? ChangeDisplaySettingsEx(deviceName, ref mode, IntPtr.Zero, CDS_UPDATEREGISTRY, IntPtr.Zero)
                : ChangeDisplaySettings(ref mode, CDS_UPDATEREGISTRY);
            if (result != DISP_CHANGE_SUCCESSFUL)
            {
                throw new InvalidOperationException("\u5207\u6362\u5230 " + width + " x " + height + " \u5931\u8d25\u3002\u8fd4\u56de\u503c\uff1a" + result);
            }
        }

        private static IEnumerable<DisplayMode> EnumerateModes(string deviceName)
        {
            var seen = new HashSet<string>();
            for (var index = 0; index < 4096; index++)
            {
                var mode = NewDevMode();
                if (!EnumDisplaySettings(deviceName, index, ref mode))
                {
                    break;
                }

                var key = mode.dmPelsWidth + "x" + mode.dmPelsHeight + "@" + mode.dmDisplayFrequency;
                if (seen.Add(key))
                {
                    yield return new DisplayMode(mode.dmPelsWidth, mode.dmPelsHeight, mode.dmDisplayFrequency);
                }
            }
        }

        private static string GetPrimaryDisplayName()
        {
            string fallback = null;
            for (var index = 0; index < 16; index++)
            {
                var device = NewDisplayDevice();
                if (!EnumDisplayDevices(null, index, ref device, 0))
                {
                    break;
                }

                var attached = (device.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) != 0;
                var primary = (device.StateFlags & DISPLAY_DEVICE_PRIMARY_DEVICE) != 0;
                if (attached && fallback == null)
                {
                    fallback = device.DeviceName;
                }
                if (attached && primary)
                {
                    return device.DeviceName;
                }
            }
            return fallback;
        }

        private static DEVMODE FindDisplayMode(string deviceName, int width, int height, int frequency)
        {
            var fallback = new DEVMODE();
            for (var index = 0; index < 4096; index++)
            {
                var mode = NewDevMode();
                if (!EnumDisplaySettings(deviceName, index, ref mode))
                {
                    break;
                }

                if (mode.dmPelsWidth == width && mode.dmPelsHeight == height)
                {
                    if (frequency <= 0 || mode.dmDisplayFrequency == frequency)
                    {
                        return mode;
                    }
                    if (fallback.dmSize == 0)
                    {
                        fallback = mode;
                    }
                }
            }
            return fallback;
        }

        private static DEVMODE NewDevMode()
        {
            var mode = new DEVMODE();
            mode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            return mode;
        }

        private static DISPLAY_DEVICE NewDisplayDevice()
        {
            var device = new DISPLAY_DEVICE();
            device.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
            return device;
        }
    }
}
