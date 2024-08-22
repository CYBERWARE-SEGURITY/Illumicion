using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malware2
{
    public class Byte1 : WaveProvider32//
    {
        private int t = 0;
        private bool switchSound = true;

        public Byte1()
        {
            this.SetWaveFormat(8000, 1); // taxa de amostragem mono
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                byte soundByte = switchSound ? GenerateBytebeatStrong(t) : GenerateBytebeatWeak(t);
                buffer[i + offset] = soundByte / 255f;
                t++;
            }

            switchSound = !switchSound; // Alterna para o próximo som na próxima leitura

            return sampleCount;
        }

        private byte GenerateBytebeatStrong(int t)
        {

            return (byte)((t >> 7 | t | t >> 6) * 10 + 4 * (t & t >> 13 | t >> 6));
        }

        private byte GenerateBytebeatWeak(int t)
        {
            // Implementação de um som diferente para alternar
            return (byte)((t >> 7 | t | t >> 6) * 10 + 4 * (t & t >> 13 | t >> 6));
        }
    }

    public class Byte2 : WaveProvider32//
    {
        private int t = 0;
        private bool switchSound = true;

        public Byte2()
        {
            this.SetWaveFormat(8000, 1);
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                byte soundByte = switchSound ? GenerateBytebeatStrong(t) : GenerateBytebeatWeak(t);
                buffer[i + offset] = soundByte / 255f;
                t++;
            }

            switchSound = !switchSound;

            return sampleCount;
        }

        private byte GenerateBytebeatStrong(int t)
        {

            return (byte)(100 * ((t << 2 | t >> 5 | t ^ 63) & (t << 10 | t >> 11)));
        }

        private byte GenerateBytebeatWeak(int t)
        {
            return (byte)(100 * ((t << 2 | t >> 5 | t ^ 63) & (t << 10 | t >> 11)));
        }
    }

    public class Byte3 : WaveProvider32//
    {
        private int t = 0;
        private bool switchSound = true;

        public Byte3()
        {
            this.SetWaveFormat(8000, 1); // taxa de amostragem
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                byte soundByte = switchSound ? GenerateBytebeatStrong(t) : GenerateBytebeatWeak(t);
                buffer[i + offset] = soundByte / 255f;
                t++;
            }

            switchSound = !switchSound; // Alterna para o próximo som

            return sampleCount;
        }

        private byte GenerateBytebeatStrong(int t)
        {
            // ByteBeat RICK ROLL MUSIC Não ALTERE!!
            return (byte)(125E3 < t ? 4.238 * t : 124375 < t ? .01 * t : 125E3 < t ? 4.238 * t : 122500 < t ? 4.757 * t : 12E4 < t ? 3.364 * t : 118750 < t ? .01 * t : 115E3 < t ? 3.364 * t : 113750 < t ? 3.564 * t : 11E4 < t ? 4 * t : 108750 < t ? .01 * t : 106250 < t ? 4.757 * t : 105E3 < t ? .01 * t : 102500 < t ? 4.238 * t : 101250 < t ? 3.564 * t : 1E5 < t ? 4.238 * t : 98750 < t ? 3.564 * t : 97500 < t ? 3.175 * t : 95E3 < t ? 3.564 * t : 93750 < t ? 4 * t : 91250 < t ? 4.238 * t : 9E4 < t ? .01 * t : 87500 < t ? 4.757 * t : 86250 < t ? .01 * t : 83750 < t ? 4.757 * t : 82500 < t ? 3.564 * t : 81250 < t ? 4.238 * t : 8E4 < t ? 3.564 * t : 78750 < t ? 3.175 * t : 77500 < t ? .01 * t : 72500 < t ? 4.757 * t : 71250 < t ? .01 * t : 68750 < t ? 5.339 * t : 67500 < t ? .01 * t : 65E3 < t ? 5.339 * t : 63750 < t ? 3.564 * t : 62500 < t ? 4.238 * t : 61250 < t ? 3.564 * t : 6E4 < t ? 3.175 * t : 52500 < t ? 3.175 * t : 45E3 < t ? 4.757 * t : 36250 < t ? 4.283 * t : 35E3 < t ? 5.339 * t : 33750 < t ? 5.657 * t : 32500 < t ? 6.35 * t : 25E3 < t ? 5.656 * t : 2E4 < t ? 5.04 * t : 18500 < t ? 3.36 * t : 7500 < t ? 5.04 * t : 4.236 * t);
        }

        private byte GenerateBytebeatWeak(int t)
        {
            // Implementação de um som diferente para alternar
            // ByteBeat RICK ROLL MUSIC Não ALTERE!!
            return (byte)(125E3 < t ? 4.238 * t : 124375 < t ? .01 * t : 125E3 < t ? 4.238 * t : 122500 < t ? 4.757 * t : 12E4 < t ? 3.364 * t : 118750 < t ? .01 * t : 115E3 < t ? 3.364 * t : 113750 < t ? 3.564 * t : 11E4 < t ? 4 * t : 108750 < t ? .01 * t : 106250 < t ? 4.757 * t : 105E3 < t ? .01 * t : 102500 < t ? 4.238 * t : 101250 < t ? 3.564 * t : 1E5 < t ? 4.238 * t : 98750 < t ? 3.564 * t : 97500 < t ? 3.175 * t : 95E3 < t ? 3.564 * t : 93750 < t ? 4 * t : 91250 < t ? 4.238 * t : 9E4 < t ? .01 * t : 87500 < t ? 4.757 * t : 86250 < t ? .01 * t : 83750 < t ? 4.757 * t : 82500 < t ? 3.564 * t : 81250 < t ? 4.238 * t : 8E4 < t ? 3.564 * t : 78750 < t ? 3.175 * t : 77500 < t ? .01 * t : 72500 < t ? 4.757 * t : 71250 < t ? .01 * t : 68750 < t ? 5.339 * t : 67500 < t ? .01 * t : 65E3 < t ? 5.339 * t : 63750 < t ? 3.564 * t : 62500 < t ? 4.238 * t : 61250 < t ? 3.564 * t : 6E4 < t ? 3.175 * t : 52500 < t ? 3.175 * t : 45E3 < t ? 4.757 * t : 36250 < t ? 4.283 * t : 35E3 < t ? 5.339 * t : 33750 < t ? 5.657 * t : 32500 < t ? 6.35 * t : 25E3 < t ? 5.656 * t : 2E4 < t ? 5.04 * t : 18500 < t ? 3.36 * t : 7500 < t ? 5.04 * t : 4.236 * t);
        }
    }

    public class Byte4 : WaveProvider32//
    {
        private int t = 0;
        private bool switchSound = true;

        public Byte4()
        {
            this.SetWaveFormat(8000, 1);
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                byte soundByte = switchSound ? GenerateBytebeatStrong(t) : GenerateBytebeatWeak(t);
                buffer[i + offset] = soundByte / 255f;
                t++;
            }

            switchSound = !switchSound;

            return sampleCount;
        }

        private byte GenerateBytebeatStrong(int t)
        {

            return (byte)(t * (0xCA98CA98CA98 >> (t >> 9 & t >> 10) & 15));
        }

        private byte GenerateBytebeatWeak(int t)
        {
            
            return (byte)(t * (0xCA98CA98CA98 >> (t >> 9 & t >> 10) & 15));
        }
    }

    public class Byte5 : WaveProvider32//
    {
        private int t = 0;
        private bool switchSound = true;

        public Byte5()
        {
            this.SetWaveFormat(44100, 1);
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                byte soundByte = switchSound ? GenerateBytebeatStrong(t) : GenerateBytebeatWeak(t);
                buffer[i + offset] = soundByte / 255f;
                t++;
            }

            switchSound = !switchSound; 

            return sampleCount;
        }

        private byte GenerateBytebeatStrong(int t)
        {

            return (byte)(8E5 * t / (t >> 2 ^ t >> 12));
        }

        private byte GenerateBytebeatWeak(int t)
        {
            return (byte)(8E5 * t / (t >> 2 ^ t >> 12));
        }
    }

    public class Byte6 : WaveProvider32
    {
        private int t = 0;
        private bool switchSound = true;

        public Byte6()
        {
            this.SetWaveFormat(5000, 1);
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                byte soundByte = switchSound ? GenerateBytebeatStrong(t) : GenerateBytebeatWeak(t);
                buffer[i + offset] = soundByte / 255f;
                t++;
            }

            switchSound = !switchSound;

            return sampleCount;
        }

        private byte GenerateBytebeatStrong(int t)
        {

            return (byte)(5 * t & t >> 7 | 3 * t & 4 * t >> 10);
        }

        private byte GenerateBytebeatWeak(int t)
        {

            return (byte)(5 * t & t >> 7 | 3 * t & 4 * t >> 10);
        }
    }
}
