using System;

namespace MiniJSON
{
    // Token: 0x02001E22 RID: 7714
    public class BitConverter2
    {
        // Token: 0x0600FB0C RID: 64268 RVA: 0x0038D93C File Offset: 0x0038BD3C
        public BitConverter2()
        {
            this.Bytes = new byte[8];
            this.BytesLength = 0;
        }

        // Token: 0x17002E1D RID: 11805
        // (get) Token: 0x0600FB0D RID: 64269 RVA: 0x0038D957 File Offset: 0x0038BD57
        // (set) Token: 0x0600FB0E RID: 64270 RVA: 0x0038D95F File Offset: 0x0038BD5F
        public int BytesLength { get; private set; }

        // Token: 0x17002E1E RID: 11806
        // (get) Token: 0x0600FB0F RID: 64271 RVA: 0x0038D968 File Offset: 0x0038BD68
        // (set) Token: 0x0600FB10 RID: 64272 RVA: 0x0038D970 File Offset: 0x0038BD70
        public byte[] Bytes { get; private set; }

        // Token: 0x0600FB11 RID: 64273 RVA: 0x0038D979 File Offset: 0x0038BD79
        public void SetBool(bool value)
        {
            BitConverter2.BoolToBytes(value, this.Bytes, 0);
            this.BytesLength = 1;
        }

        // Token: 0x0600FB12 RID: 64274 RVA: 0x0038D98F File Offset: 0x0038BD8F
        public void SetShort(short value)
        {
            BitConverter2.ShortToBytes(value, this.Bytes, 0);
            this.BytesLength = 2;
        }

        // Token: 0x0600FB13 RID: 64275 RVA: 0x0038D9A5 File Offset: 0x0038BDA5
        public void SetInt(int value)
        {
            BitConverter2.IntToBytes(value, this.Bytes, 0);
            this.BytesLength = 4;
        }

        // Token: 0x0600FB14 RID: 64276 RVA: 0x0038D9BB File Offset: 0x0038BDBB
        public void SetLong(long value)
        {
            BitConverter2.LongToBytes(value, this.Bytes, 0);
            this.BytesLength = 8;
        }

        // Token: 0x0600FB15 RID: 64277 RVA: 0x0038D9D1 File Offset: 0x0038BDD1
        public void SetDouble(double value)
        {
            BitConverter2.DoubleToBytes(value, this.Bytes, 0);
            this.BytesLength = 8;
        }

        // Token: 0x0600FB16 RID: 64278 RVA: 0x0038D9E8 File Offset: 0x0038BDE8
        public static byte[] GetBytes(bool value)
        {
            byte[] array = new byte[1];
            BitConverter2.BoolToBytes(value, array, 0);
            return array;
        }

        // Token: 0x0600FB17 RID: 64279 RVA: 0x0038DA08 File Offset: 0x0038BE08
        public static byte[] GetBytes(short value)
        {
            byte[] array = new byte[2];
            BitConverter2.ShortToBytes(value, array, 0);
            return array;
        }

        // Token: 0x0600FB18 RID: 64280 RVA: 0x0038DA28 File Offset: 0x0038BE28
        public static byte[] GetBytes(int value)
        {
            byte[] array = new byte[4];
            BitConverter2.IntToBytes(value, array, 0);
            return array;
        }

        // Token: 0x0600FB19 RID: 64281 RVA: 0x0038DA48 File Offset: 0x0038BE48
        public static byte[] GetBytes(long value)
        {
            byte[] array = new byte[8];
            BitConverter2.LongToBytes(value, array, 0);
            return array;
        }

        // Token: 0x0600FB1A RID: 64282 RVA: 0x0038DA68 File Offset: 0x0038BE68
        public static byte[] GetBytes(double value)
        {
            byte[] array = new byte[8];
            BitConverter2.DoubleToBytes(value, array, 0);
            return array;
        }

        // Token: 0x0600FB1B RID: 64283 RVA: 0x0038DA85 File Offset: 0x0038BE85
        public static void BoolToBytes(bool value, byte[] dest1, int offset)
        {
            if (value)
            {
                dest1[offset] = 1;
            }
            else
            {
                dest1[offset] = 0;
            }
        }

        // Token: 0x0600FB1C RID: 64284 RVA: 0x0038DA9C File Offset: 0x0038BE9C
        public static void ShortToBytes(short value, byte[] dest2, int offset)
        {
            if (BitConverter.IsLittleEndian)
            {
                dest2[offset] = (byte)(value & 255);
                dest2[offset + 1] = (byte)(value >> 8 & 255);
            }
            else
            {
                dest2[offset + 1] = (byte)(value & 255);
                dest2[offset] = (byte)(value >> 8 & 255);
            }
        }

        // Token: 0x0600FB1D RID: 64285 RVA: 0x0038DAEC File Offset: 0x0038BEEC
        public static void IntToBytes(int value, byte[] dest4, int offset)
        {
            if (BitConverter.IsLittleEndian)
            {
                dest4[offset] = (byte)(value & 255);
                dest4[offset + 1] = (byte)(value >> 8 & 255);
                dest4[offset + 2] = (byte)(value >> 16 & 255);
                dest4[offset + 3] = (byte)(value >> 24);
            }
            else
            {
                dest4[offset + 3] = (byte)(value & 255);
                dest4[offset + 2] = (byte)(value >> 8 & 255);
                dest4[offset + 1] = (byte)(value >> 16 & 255);
                dest4[offset] = (byte)(value >> 24);
            }
        }

        // Token: 0x0600FB1E RID: 64286 RVA: 0x0038DB70 File Offset: 0x0038BF70
        public static void LongToBytes(long value, byte[] dest8, int offset)
        {
            if (BitConverter.IsLittleEndian)
            {
                dest8[offset] = (byte)(value >> 0 & 255L);
                dest8[offset + 1] = (byte)(value >> 8 & 255L);
                dest8[offset + 2] = (byte)(value >> 16 & 255L);
                dest8[offset + 3] = (byte)(value >> 24 & 255L);
                dest8[offset + 4] = (byte)(value >> 32 & 255L);
                dest8[offset + 5] = (byte)(value >> 40 & 255L);
                dest8[offset + 6] = (byte)(value >> 48 & 255L);
                dest8[offset + 7] = (byte)(value >> 56 & 255L);
            }
            else
            {
                dest8[offset + 7] = (byte)(value >> 0 & 255L);
                dest8[offset + 6] = (byte)(value >> 8 & 255L);
                dest8[offset + 5] = (byte)(value >> 16 & 255L);
                dest8[offset + 4] = (byte)(value >> 24 & 255L);
                dest8[offset + 3] = (byte)(value >> 32 & 255L);
                dest8[offset + 2] = (byte)(value >> 40 & 255L);
                dest8[offset + 1] = (byte)(value >> 48 & 255L);
                dest8[offset] = (byte)(value >> 56 & 255L);
            }
        }

        // Token: 0x0600FB1F RID: 64287 RVA: 0x0038DC94 File Offset: 0x0038C094
        public static void DoubleToBytes(double value, byte[] dest8, int offset)
        {
            long value2 = BitConverter.DoubleToInt64Bits(value);
            BitConverter2.LongToBytes(value2, dest8, offset);
        }
    }
}
