using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malware2
{
    public class SpecialCargaStars
    {
        private const int numStars = 10000; // Quantidade de Estrelas Na Tela
        private struct Star
        {
            public float x, y, z;
        }

        private static Star[] stars = new Star[numStars];

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
                                              IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, uint dwRop);

        [DllImport("msimg32.dll")]
        private static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
                                              IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BLENDFUNCTION blendFunction);

        private const int SRCCOPY = 0x00CC0020;
        private const int BLACKNESS = 0x00000042;
        private const int BI_RGB = 0;

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public RGBQUAD bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        private static void InitStars()
        {
            Random rand = new Random();
            for (int i = 0; i < numStars; ++i)
            {
                stars[i].x = (float)(rand.Next(2000) - 1000);
                stars[i].y = (float)(rand.Next(2000) - 1000);
                stars[i].z = (float)(rand.Next(2000) + 1);
            }
        }

        public static void EfeitoEstrelas()
        {
            InitStars();

            IntPtr dc = GetDC(IntPtr.Zero);
            IntPtr dcCopy = CreateCompatibleDC(dc);
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = BI_RGB;

            IntPtr ppvBits;
            IntPtr bmp = CreateDIBSection(dc, ref bmpi, 0, out ppvBits, IntPtr.Zero, 0);
            SelectObject(dcCopy, bmp);

            unsafe
            {
                RGBQUAD* rgbquad = (RGBQUAD*)ppvBits;

                while (true)
                {
                    StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, BLACKNESS);

                    for (int s = 0; s < numStars; ++s)
                    {
                        stars[s].z -= 10;

                        if (stars[s].z <= 0)
                        {
                            Random rand = new Random();
                            stars[s].x = (float)(rand.Next(1000) - 500);
                            stars[s].y = (float)(rand.Next(1000) - 500);
                            stars[s].z = (float)(rand.Next(2000) + 10);
                        }

                        int sx = (int)((stars[s].x / stars[s].z) * w / 2 + w / 2);
                        int sy = (int)((stars[s].y / stars[s].z) * h / 2 + h / 2);

                        if (sx >= 0 && sx < w && sy >= 0 && sy < h)
                        {
                            int index = sy * w + sx;

                            rgbquad[index].rgbRed = 255;
                            rgbquad[index].rgbGreen = 255;
                            rgbquad[index].rgbBlue = 255;
                        }
                    }

                    BLENDFUNCTION blendFunction = new BLENDFUNCTION
                    {
                        BlendOp = 0,
                        BlendFlags = 0,
                        SourceConstantAlpha = 200,
                        AlphaFormat = 0
                    };

                    AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, blendFunction);

                    Thread.Sleep(10);
                }
            }

            ReleaseDC(IntPtr.Zero, dc);
        }
    }
}
