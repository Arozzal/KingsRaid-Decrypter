using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
/*
namespace MiniJSON
{
    // Token: 0x02001E24 RID: 7716
    public static class JsonBin
    {
        // Token: 0x0600FB26 RID: 64294 RVA: 0x0038DE18 File Offset: 0x0038C218
        public static object Deserialize(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            return JsonBin.Parser.Parse(bytes);
        }

        // Token: 0x0600FB27 RID: 64295 RVA: 0x0038DE30 File Offset: 0x0038C230
        public static byte[] Serialize(object obj)
        {
            return JsonBin.Serializer.Serialize(obj);
        }

        // Token: 0x04008C56 RID: 35926
        private const int Version = 20160919;

        // Token: 0x04008C57 RID: 35927
        private const byte JsonTypeNull = 0;

        // Token: 0x04008C58 RID: 35928
        private const byte JsonTypeObject = 10;

        // Token: 0x04008C59 RID: 35929
        private const byte JsonTypeArray = 20;

        // Token: 0x04008C5A RID: 35930
        private const byte JsonTypeString = 30;

        // Token: 0x04008C5B RID: 35931
        private const byte JsonTypeBool = 40;

        // Token: 0x04008C5C RID: 35932
        private const byte JsonTypeDouble = 50;

        // Token: 0x04008C5D RID: 35933
        private const byte JsonTypeLong = 60;

        // Token: 0x04008C5E RID: 35934
        private const byte JsonTypeInt = 61;

        // Token: 0x04008C5F RID: 35935
        private const byte JsonTypeShort = 62;

        // Token: 0x04008C60 RID: 35936
        private const byte JsonTypeError = 255;

        // Token: 0x02001E25 RID: 7717
        public class JsonBytesReader
        {
            // Token: 0x0600FB28 RID: 64296 RVA: 0x0038DE38 File Offset: 0x0038C238
            public JsonBytesReader(byte[] bytes)
            {
                this.Bytes = bytes;
                this.Position = 0;
            }

            // Token: 0x17002E1F RID: 11807
            // (get) Token: 0x0600FB29 RID: 64297 RVA: 0x0038DE4E File Offset: 0x0038C24E
            // (set) Token: 0x0600FB2A RID: 64298 RVA: 0x0038DE56 File Offset: 0x0038C256
            public byte[] Bytes { get; private set; }

            // Token: 0x17002E20 RID: 11808
            // (get) Token: 0x0600FB2B RID: 64299 RVA: 0x0038DE5F File Offset: 0x0038C25F
            // (set) Token: 0x0600FB2C RID: 64300 RVA: 0x0038DE67 File Offset: 0x0038C267
            public int Position { get; private set; }

            // Token: 0x17002E21 RID: 11809
            // (get) Token: 0x0600FB2D RID: 64301 RVA: 0x0038DE70 File Offset: 0x0038C270
            public int RemainedLength
            {
                get
                {
                    return ((this.Bytes == null) ? 0 : this.Bytes.Length) - this.Position;
                }
            }

            // Token: 0x0600FB2E RID: 64302 RVA: 0x0038DE92 File Offset: 0x0038C292
            private bool CanRead(int byteCount)
            {
                return this.Bytes != null && this.Bytes.Length - this.Position >= byteCount;
            }

            // Token: 0x0600FB2F RID: 64303 RVA: 0x0038DEBC File Offset: 0x0038C2BC
            public string ReadString()
            {
                int num = this.ReadInt();
                if (num == 0)
                {
                    return string.Empty;
                }
                if (!this.CanRead(num))
                {
                    return string.Empty;
                }
                string @string = Encoding.Unicode.GetString(this.Bytes, this.Position, num);
                this.Position += num;
                return @string;
            }

            // Token: 0x0600FB30 RID: 64304 RVA: 0x0038DF18 File Offset: 0x0038C318
            public bool ReadBool()
            {
                if (!this.CanRead(1))
                {
                    return false;
                }
                bool result = BitConverter.ToBoolean(this.Bytes, this.Position);
                this.Position++;
                return result;
            }

            // Token: 0x0600FB31 RID: 64305 RVA: 0x0038DF54 File Offset: 0x0038C354
            public byte ReadByte(byte defaultValue = 0)
            {
                if (!this.CanRead(1))
                {
                    return defaultValue;
                }
                byte result = this.Bytes[this.Position];
                this.Position++;
                return result;
            }

            // Token: 0x0600FB32 RID: 64306 RVA: 0x0038DF8C File Offset: 0x0038C38C
            public short ReadShort()
            {
                if (!this.CanRead(2))
                {
                    return 0;
                }
                short result = BitConverter.ToInt16(this.Bytes, this.Position);
                this.Position += 2;
                return result;
            }

            // Token: 0x0600FB33 RID: 64307 RVA: 0x0038DFC8 File Offset: 0x0038C3C8
            public int ReadInt()
            {
                if (!this.CanRead(4))
                {
                    return 0;
                }
                int result = BitConverter.ToInt32(this.Bytes, this.Position);
                this.Position += 4;
                return result;
            }

            // Token: 0x0600FB34 RID: 64308 RVA: 0x0038E004 File Offset: 0x0038C404
            public long ReadLong()
            {
                if (!this.CanRead(8))
                {
                    return 0L;
                }
                long result = BitConverter.ToInt64(this.Bytes, this.Position);
                this.Position += 8;
                return result;
            }

            // Token: 0x0600FB35 RID: 64309 RVA: 0x0038E044 File Offset: 0x0038C444
            public double ReadDouble()
            {
                if (!this.CanRead(8))
                {
                    return 0.0;
                }
                double result = BitConverter.ToDouble(this.Bytes, this.Position);
                this.Position += 8;
                return result;
            }
        }

        // Token: 0x02001E26 RID: 7718
        private sealed class Parser : IDisposable
        {
            // Token: 0x0600FB36 RID: 64310 RVA: 0x0038E088 File Offset: 0x0038C488
            private Parser(byte[] bytes)
            {
                this._reader = new JsonBin.JsonBytesReader(bytes);
                this._stringList = new List<string>();
            }

            // Token: 0x0600FB37 RID: 64311 RVA: 0x0038E0A7 File Offset: 0x0038C4A7
            public void Dispose()
            {
                this._reader = null;
                this._stringList = null;
            }

            // Token: 0x0600FB38 RID: 64312 RVA: 0x0038E0B8 File Offset: 0x0038C4B8
            public static object Parse(byte[] bytes)
            {
                object result;
                using (JsonBin.Parser parser = new JsonBin.Parser(bytes))
                {
                    result = parser.ParseRoot();
                }
                return result;
            }

            // Token: 0x0600FB39 RID: 64313 RVA: 0x0038E0F8 File Offset: 0x0038C4F8
            private object ParseRoot()
            {
                int num = this._reader.ReadInt();
                if (num != 20160919)
                {
                    return null;
                }
                this.ParseStringList();
                return this.ParseValue();
            }

            // Token: 0x0600FB3A RID: 64314 RVA: 0x0038E12C File Offset: 0x0038C52C
            private void ParseStringList()
            {
                int num = this._reader.ReadInt();
                if (this._stringList.Capacity < this._stringList.Count + num)
                {
                    this._stringList.Capacity = this._stringList.Count + num;
                }
                for (int i = 0; i < num; i++)
                {
                    string item = this._reader.ReadString();
                    this._stringList.Add(item);
                }
            }

            // Token: 0x0600FB3B RID: 64315 RVA: 0x0038E1A4 File Offset: 0x0038C5A4
            private string ParseStringFromList()
            {
                int num = this._reader.ReadInt();
                int num2 = num - 1;
                if (num2 < 0 || num2 >= this._stringList.Count)
                {
                    return string.Empty;
                }
                return this._stringList[num2];
            }

            // Token: 0x0600FB3C RID: 64316 RVA: 0x0038E1EC File Offset: 0x0038C5EC
            private Dictionary<string, object> ParseValueObject()
            {
                int num = this._reader.ReadInt();
                Dictionary<string, object> dictionary = JsonPool.instance.CreateDic(num);
                for (int i = 0; i < num; i++)
                {
                    string text = this.ParseStringFromList();
                    object value = this.ParseValue();
                    if (!string.IsNullOrEmpty(text))
                    {
                        dictionary[text] = value;
                    }
                }
                return dictionary;
            }

            // Token: 0x0600FB3D RID: 64317 RVA: 0x0038E24C File Offset: 0x0038C64C
            private List<object> ParseValueArray()
            {
                int num = this._reader.ReadInt();
                List<object> list = JsonPool.instance.CreateList(num);
                for (int i = 0; i < num; i++)
                {
                    object item = this.ParseValue();
                    list.Add(item);
                }
                return list;
            }

            // Token: 0x0600FB3E RID: 64318 RVA: 0x0038E294 File Offset: 0x0038C694
            private object ParseValue()
            {
                byte b = this._reader.ReadByte(byte.MaxValue);
                if (b == 0)
                {
                    return null;
                }
                if (b == 10)
                {
                    return this.ParseValueObject();
                }
                if (b == 20)
                {
                    return this.ParseValueArray();
                }
                if (b == 30)
                {
                    return this.ParseValueString();
                }
                if (b == 40)
                {
                    return this.ParseValueBool();
                }
                if (b == 50)
                {
                    return this.ParseValueDouble();
                }
                if (b == 60)
                {
                    return this.ParseValueLong();
                }
                if (b == 61)
                {
                    return this.ParseValueInt();
                }
                if (b == 62)
                {
                    return this.ParseValueShort();
                }
                return null;
            }

            // Token: 0x0600FB3F RID: 64319 RVA: 0x0038E34C File Offset: 0x0038C74C
            private string ParseValueString()
            {
                return this.ParseStringFromList();
            }

            // Token: 0x0600FB40 RID: 64320 RVA: 0x0038E354 File Offset: 0x0038C754
            private long ParseValueLong()
            {
                return this._reader.ReadLong();
            }

            // Token: 0x0600FB41 RID: 64321 RVA: 0x0038E361 File Offset: 0x0038C761
            private long ParseValueInt()
            {
                return (long)this._reader.ReadInt();
            }

            // Token: 0x0600FB42 RID: 64322 RVA: 0x0038E36F File Offset: 0x0038C76F
            private long ParseValueShort()
            {
                return (long)this._reader.ReadShort();
            }

            // Token: 0x0600FB43 RID: 64323 RVA: 0x0038E37D File Offset: 0x0038C77D
            private double ParseValueDouble()
            {
                return this._reader.ReadDouble();
            }

            // Token: 0x0600FB44 RID: 64324 RVA: 0x0038E38A File Offset: 0x0038C78A
            private bool ParseValueBool()
            {
                return this._reader.ReadBool();
            }

            // Token: 0x04008C63 RID: 35939
            private const string WHITE_SPACE = " \t\n\r";

            // Token: 0x04008C64 RID: 35940
            private const string WORD_BREAK = " \t\n\r{}[],:\"";

            // Token: 0x04008C65 RID: 35941
            private JsonBin.JsonBytesReader _reader;

            // Token: 0x04008C66 RID: 35942
            private List<string> _stringList;
        }

        // Token: 0x02001E27 RID: 7719
        private sealed class Serializer
        {
            // Token: 0x0600FB45 RID: 64325 RVA: 0x0038E397 File Offset: 0x0038C797
            private Serializer()
            {
                this._bodyStream = new MemoryStream();
                this._stringMapper = new Dictionary<string, int>();
                this._stringList = new List<string>();
                this._converter = new BitConverter2();
            }

            // Token: 0x0600FB46 RID: 64326 RVA: 0x0038E3CC File Offset: 0x0038C7CC
            public static byte[] Serialize(object obj)
            {
                JsonBin.Serializer serializer = new JsonBin.Serializer();
                serializer.SerializeValue(obj);
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.WRITEINT(20160919, serializer._converter);
                serializer.SerializeStringList(memoryStream);
                serializer._bodyStream.WriteTo(memoryStream);
                return memoryStream.ToArray();
            }

            // Token: 0x0600FB47 RID: 64327 RVA: 0x0038E418 File Offset: 0x0038C818
            private void SerializeStringList(MemoryStream stream)
            {
                stream.WRITEINT(this._stringList.Count, this._converter);
                for (int i = 0; i < this._stringList.Count; i++)
                {
                    stream.WRITESTRING(this._stringList[i], this._converter);
                }
            }

            // Token: 0x0600FB48 RID: 64328 RVA: 0x0038E470 File Offset: 0x0038C870
            private int GetStringIndex(string value)
            {
                if (string.IsNullOrEmpty(value))
                {
                    return 0;
                }
                int num;
                if (!this._stringMapper.TryGetValue(value, out num))
                {
                    num = this._stringList.Count + 1;
                    this._stringList.Add(value);
                    this._stringMapper.Add(value, num);
                }
                return num;
            }

            // Token: 0x0600FB49 RID: 64329 RVA: 0x0038E4C8 File Offset: 0x0038C8C8
            private void SerializeString(string value)
            {
                int stringIndex = this.GetStringIndex(value);
                this._bodyStream.WRITEINT(stringIndex, this._converter);
            }

            // Token: 0x0600FB4A RID: 64330 RVA: 0x0038E4F0 File Offset: 0x0038C8F0
            private void SerializeValue(object value)
            {
                string value2;
                IList anArray;
                IDictionary obj;
                if (value == null)
                {
                    this._bodyStream.WriteByte(0);
                }
                else if ((value2 = (value as string)) != null)
                {
                    this.SerializeValueString(value2);
                }
                else if (value is bool)
                {
                    this.SerializeValueBool((bool)value);
                }
                else if ((anArray = (value as IList)) != null)
                {
                    this.SerializeArray(anArray);
                }
                else if ((obj = (value as IDictionary)) != null)
                {
                    this.SerializeObject(obj);
                }
                else if (value is char)
                {
                    this.SerializeValueString(value.ToString());
                }
                else
                {
                    this.SerializeValueOther(value);
                }
            }

            // Token: 0x0600FB4B RID: 64331 RVA: 0x0038E5A0 File Offset: 0x0038C9A0
            private void SerializeObject(IDictionary obj)
            {
                this._bodyStream.WriteByte(10);
                this._bodyStream.WRITEINT(obj.Count, this._converter);
                IEnumerator enumerator = obj.Keys.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        object obj2 = enumerator.Current;
                        this.SerializeString(obj2.ToString());
                        this.SerializeValue(obj[obj2]);
                    }
                }
                finally
                {
                    IDisposable disposable;
                    if ((disposable = (enumerator as IDisposable)) != null)
                    {
                        disposable.Dispose();
                    }
                }
            }

            // Token: 0x0600FB4C RID: 64332 RVA: 0x0038E638 File Offset: 0x0038CA38
            private void SerializeArray(IList anArray)
            {
                this._bodyStream.WriteByte(20);
                this._bodyStream.WRITEINT(anArray.Count, this._converter);
                IEnumerator enumerator = anArray.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        object value = enumerator.Current;
                        this.SerializeValue(value);
                    }
                }
                finally
                {
                    IDisposable disposable;
                    if ((disposable = (enumerator as IDisposable)) != null)
                    {
                        disposable.Dispose();
                    }
                }
            }

            // Token: 0x0600FB4D RID: 64333 RVA: 0x0038E6B8 File Offset: 0x0038CAB8
            private void SerializeValueString(string value)
            {
                this._bodyStream.WriteByte(30);
                this.SerializeString(value);
            }

            // Token: 0x0600FB4E RID: 64334 RVA: 0x0038E6CE File Offset: 0x0038CACE
            private void SerializeValueBool(bool value)
            {
                this._bodyStream.WriteByte(40);
                this._bodyStream.WRITEBOOL(value, this._converter);
            }

            // Token: 0x0600FB4F RID: 64335 RVA: 0x0038E6F0 File Offset: 0x0038CAF0
            private void SerializeValueLong(long value)
            {
                if (value < 32767L && value > -32768L)
                {
                    this._bodyStream.WriteByte(62);
                    this._bodyStream.WRITESHORT((short)value, this._converter);
                }
                else if (value < 2147483647L && value > -2147483648L)
                {
                    this._bodyStream.WriteByte(61);
                    this._bodyStream.WRITEINT((int)value, this._converter);
                }
                else
                {
                    this._bodyStream.WriteByte(60);
                    this._bodyStream.WRITELONG(value, this._converter);
                }
            }

            // Token: 0x0600FB50 RID: 64336 RVA: 0x0038E796 File Offset: 0x0038CB96
            private void SerializeValueDouble(double value)
            {
                this._bodyStream.WriteByte(50);
                this._bodyStream.WRITEDOUBLE(value, this._converter);
            }

            // Token: 0x0600FB51 RID: 64337 RVA: 0x0038E7B8 File Offset: 0x0038CBB8
            private void SerializeValueOther(object value)
            {
                if (value is int)
                {
                    this.SerializeValueLong((long)((int)value));
                }
                else if (value is uint)
                {
                    this.SerializeValueLong((long)((ulong)((uint)value)));
                }
                else if (value is long)
                {
                    this.SerializeValueLong((long)value);
                }
                else if (value is sbyte)
                {
                    this.SerializeValueLong((long)((sbyte)value));
                }
                else if (value is byte)
                {
                    this.SerializeValueLong((long)((byte)value));
                }
                else if (value is short)
                {
                    this.SerializeValueLong((long)((short)value));
                }
                else if (value is ushort)
                {
                    this.SerializeValueLong((long)((ushort)value));
                }
                else if (value is float)
                {
                    this.SerializeValueDouble((double)((float)value));
                }
                else if (value is double)
                {
                    this.SerializeValueDouble((double)value);
                }
                else
                {
                    this.SerializeValueString(value.ToString());
                }
            }

            // Token: 0x04008C67 RID: 35943
            private MemoryStream _bodyStream;

            // Token: 0x04008C68 RID: 35944
            private Dictionary<string, int> _stringMapper;

            // Token: 0x04008C69 RID: 35945
            private List<string> _stringList;

            // Token: 0x04008C6A RID: 35946
            private BitConverter2 _converter;
        }
    }
}
*/