using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RisingFormats
{
    enum BinTypes
    {
        Byte,
        UInt32,
        Int32,
        Float32,
        Int16,
        UInt16,
    }
    class BinReader
    {
        byte[] _data;
        int _pointer = 0;
        public BinReader(byte[] data)
        {
            _data = data;
        }
        public void Seek(uint offset)
        {
            _pointer = (int)offset;
        }

        // Single Value Reading
        public byte ReadByte()
        {
            return _data[_pointer++];
        }
        public uint ReadUInt32()
        {
            _pointer += 4;
            return BitConverter.ToUInt32( _data, _pointer-4 );
        }
        public int ReadInt32()
        {
            _pointer += 4;
            return BitConverter.ToInt32(_data, _pointer - 4);
        }
        public float ReadFloat32()
        {
            _pointer += 4;
            return BitConverter.ToSingle(_data, _pointer - 4);
        }
        public short ReadInt16()
        {
            _pointer += 2;
            return BitConverter.ToInt16(_data, _pointer - 2);
        }
        public ushort ReadUInt16()
        {
            _pointer += 2;
            return BitConverter.ToUInt16(_data, _pointer - 2);
        }
        public string ReadString(int length)
        {
            string res = string.Empty;
            for (int i = 0; i < length; i++)
            {
                res += (char)_data[_pointer++];
            }
            return res;
        }
        // Arrays
        public string[] ReadStringArray(uint length, uint arrLen)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < arrLen; i++)
            {
                list.Add(ReadString((int)length));
            }
            return list.ToArray();
        }
        public uint[] ReadUInt32Array(uint length)
        {
            List<uint> list = new List<uint>();
            for (int i = 0; i < length; i++)
            {
                list.Add(ReadUInt32());
            }
            return list.ToArray();
        }
        public int[] ReadInt32Array(uint length)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < length; i++)
            {
                list.Add(ReadInt32());
            }
            return list.ToArray();
        }
    }
}