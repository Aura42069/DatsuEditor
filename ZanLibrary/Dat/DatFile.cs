using System;
using System.IO.Hashing;
using System.Text;
using System.Xml.Linq;

namespace RisingFormats.Dat
{
    struct DatHeader
    {
        public string Magic;
        public uint FileAmount;
        public uint PositionsOffset;
        public uint ExtensionsOffset;
        public uint NamesOffset;
        public uint SizesOffset;
        public uint HashMapOffset;
    }
    public struct DatFileEntry
    {
        public string Name;
        public byte[] Data;

        public DatFileEntry(string Name, byte[] Data)
        {
            this.Name = Name;
            this.Data = Data;
        }
    }
    public struct DatHashData
    {
        public int PrehashShift;
        public uint BucketOffsetsSize;
        public List<ushort> BucketOffsets;
        public List<int> Hashes;
        public List<ushort> Indices;
        public uint BucketOffsetsOffset;
        public uint HashesOffset;
        public uint IndicesOffset;
    }

    public class DatFile
    {
        public static DatFileEntry[] Load(byte[] data)
        {
            uint nameSize = 1;
            DatHeader header = new();

            List<DatFileEntry> Files = new();

            BinReader reader = new(data);
            header.Magic = reader.ReadString(4);
            header.FileAmount = reader.ReadUInt32();
            header.PositionsOffset = reader.ReadUInt32();
            header.ExtensionsOffset = reader.ReadUInt32();
            header.NamesOffset = reader.ReadUInt32();
            header.SizesOffset = reader.ReadUInt32();
            header.HashMapOffset = reader.ReadUInt32();

            reader.ReadUInt32();

            reader.Seek(header.PositionsOffset);
            int[] fileOffsets = reader.ReadInt32Array(header.FileAmount);

            reader.Seek(header.SizesOffset);
            int[] fileSizes = reader.ReadInt32Array(header.FileAmount);

            reader.Seek(header.NamesOffset);
            nameSize = reader.ReadUInt32();
            string[] fileNames = reader.ReadStringArray(nameSize, header.FileAmount);

            for (int i = 0; i < header.FileAmount; i++)
            {
                int filePosition = fileOffsets[i];
                int fileSize = fileSizes[i];
                string fileName = fileNames[i];

                byte[] fileData = new ArraySegment<byte>(data, filePosition, fileSize).ToArray();
                Files.Add(new DatFileEntry(fileName.Replace("\0", string.Empty), fileData));
            }

            return Files.ToArray();
        }

        public static byte[] Save(DatFileEntry[] Files)
        {
            /*
             * Struct generation phase
             */

            DatHeader header = new();

            header.FileAmount = (uint)Files.Length;

            int LongestFilename = 0;

            List<int> offsets = new();
            List<int> sizes = new();
            List<string> extensions = new();
            List<string> names = new();

            foreach (DatFileEntry file in Files)
            {
                if (LongestFilename < file.Name.Length)
                    LongestFilename = file.Name.Length;

                sizes.Add(file.Data.Length);
                extensions.Add(Path.GetExtension(file.Name).Substring(1));
                names.Add(file.Name);
            }

            LongestFilename += 1;

            header.PositionsOffset = 0x20;
            header.ExtensionsOffset = 0x20 + (4 * header.FileAmount);
            header.NamesOffset = header.ExtensionsOffset + (4 * header.FileAmount);
            header.SizesOffset = (uint)(header.NamesOffset + (LongestFilename * header.FileAmount) + 4);
            header.HashMapOffset = header.SizesOffset + (4 * header.FileAmount);

            DatHashData hashData = DatHashUtil.GenerateHashData(Files);

            int TempPos = (int)(header.HashMapOffset + hashData.IndicesOffset + (2 * header.FileAmount));
            int startpad = BinUtils.CalcPadding(16, TempPos);

            int _pointer = TempPos + startpad;

            foreach (var file in Files)
            {
                offsets.Add(_pointer);
                _pointer += (int)(file.Data.Length);
                uint pad = BinUtils.CalcPadding(16, (uint)(_pointer));
                _pointer += (int)pad;
            }

            /*
             * Byte generation phase
             */
            BinWriter FileData = new();

            FileData.WriteString("DAT\x00"); // DAT\x00
            FileData.WriteUInt32(header.FileAmount);
            FileData.WriteUInt32(header.PositionsOffset);
            FileData.WriteUInt32(header.ExtensionsOffset);
            FileData.WriteUInt32(header.NamesOffset);
            FileData.WriteUInt32(header.SizesOffset);
            FileData.WriteUInt32(header.HashMapOffset);
            FileData.WriteUInt32(0);

            FileData.Seek(header.PositionsOffset);
            for (int i = 0; i < header.FileAmount; i++)
                FileData.WriteUInt32((uint)offsets[i]);

            FileData.Seek(header.ExtensionsOffset);
            for (int i = 0; i < header.FileAmount; i++)
            {
                FileData.WriteString(extensions[i]);
                FileData.WriteByte(0x00);
            }

            FileData.Seek(header.NamesOffset);
            FileData.WriteInt32(LongestFilename);

            for (int i = 0; i < header.FileAmount; i++)
            {
                FileData.WriteString(names[i], LongestFilename);
                // FileData.WriteByte(0x00);
            }

            FileData.Seek(header.SizesOffset);
            for (int i = 0; i < header.FileAmount; i++)
                FileData.WriteUInt32((uint)sizes[i]);

            /*
             * Hashes :sob::sob::sob::sob::sob:
             */
            FileData.Seek(header.HashMapOffset);
            FileData.WriteInt32(hashData.PrehashShift);
            FileData.WriteInt32(16);
            FileData.WriteInt32(16 + hashData.BucketOffsets.Count * 2);
            FileData.WriteInt32(16 + (hashData.BucketOffsets.Count * 2) + (hashData.Hashes.Count * 4));

            for (int i = 0; i < hashData.BucketOffsets.Count; i++)
                FileData.WriteUInt16(hashData.BucketOffsets[i]);

            for (int i = 0; i < header.FileAmount; i++)
                FileData.WriteInt32(hashData.Hashes[i]);

            for (int i = 0; i < header.FileAmount; i++)
                FileData.WriteUInt16(hashData.Indices[i]);

            int hashPad = (int)BinUtils.CalcPadding(16, (uint)FileData.Tell());
            for (int i = 0; i < hashPad; i++)
                FileData.WriteByte(0x00);

            for (int i = 0; i < header.FileAmount; i++)
            {
                if (offsets[i] > FileData.Size())
                {
                    int extend = offsets[i] - FileData.Size();
                    for (int j = 0; j < extend; j++)
                    {
                        FileData.WriteByte(0x00);
                    }
                }
                FileData.Seek((uint)offsets[i]);
                FileData.WriteByteArray(Files[i].Data);
            }

            //for (int i = 0; i < names.Count; i++)
            //{
            //    uint search = (Crc32.HashToUInt32(Encoding.ASCII.GetBytes(names[i].ToLower())) & ~0x80000000) >> hashData.PrehashShift;
            //    Console.WriteLine($"Exp: {names[i]}\tOut: {names[hashData.Indices[hashData.BucketOffsets[(int)search]]]}");
            //}

            return FileData.GetArray();
        }
    }

    // Taken and rewritten from here
    // https://github.com/ThisKwasior/KwasTools/blob/master/kwaslib/platinum/dat.c#L330
    //public class DatHashUtil
    //{
    //    static uint BitCount(uint value)
    //    {
    //        uint count = 0;

    //        while (value > 0)
    //        {
    //            count += 1;
    //            value >>= 1;
    //        }

    //        return count;
    //    }

    //    static uint NextPowOf2Bits(uint value)
    //    {
    //        if (value == 0)
    //            return 1;

    //        /*
    //            MGRR has this wrong with 1 or 2 files 
    //            And it breaks on larget values for some reason, 1378 for example.
    //        */
    //        if (value == 1 || value == 2)
    //            return 2;

    //        return BitCount(value - 1);
    //    }
    //    static uint CalcPrehashShift(uint value)
    //    {
    //        uint max_prehash = 31;
    //        uint calc_prehash = 32 - NextPowOf2Bits(value);
    //        return (max_prehash > calc_prehash) ? calc_prehash : max_prehash;
    //    }

    //    // Why isn't CRC32 included in base .NET :sob:
    //    static uint HashFilename(string name)
    //    {
    //     return System.IO.Hashing.Crc32.HashToUInt32(Encoding.ASCII.GetBytes(name)) & 0x7FFFFFFF;
    //    }

    //    static ushort[] GenBucketList(uint files_amount, ushort[] bucket_offsets, uint[] hashes, uint prehash_shift)
    //    {
    //        ushort[] _bucket_offsets = new ushort[bucket_offsets.Length];

    //     for(uint i = 0; i != files_amount; ++i)
    //     {
    //      uint bucket_off_id = hashes[i] >> (int)prehash_shift;

    //      if(bucket_offsets[bucket_off_id] == 0xFFFF)
    //       _bucket_offsets[bucket_off_id] = (ushort)i;
    //        }
    //        return _bucket_offsets;
    //    }


    //    public static DatHashData GenerateHashData(DatFileEntry[] Files)
    //    {
    //        var hashdata = new DatHashData();

    //        hashdata.PrehashShift = (int)CalcPrehashShift((uint)Files.Length);
    //        hashdata.BucketOffsetsSize = 1 << (31 - hashdata.PrehashShift);

    //        hashdata.BucketOffsets = new ushort[Files.Length];
    //        hashdata.Hashes = new uint[Files.Length];
    //        hashdata.Indices = new ushort[Files.Length];

    //        for (int i = 0; i < hashdata.BucketOffsetsSize; i++)
    //        {
    //            hashdata.BucketOffsets[i] = 0xFF;
    //        }

    //        /* Hash filenames */
    //        for (uint i = 0; i != Files.Length; ++i)
    //        {
    //            hashdata.Hashes[i] = HashFilename(Files.ElementAt(new Index((int)i)).Name);
    //            hashdata.Indices[i] = (ushort)i;
    //        }

    //        // What if I remove this step?
    //        /* Sort by hash nibbles */
    //        // SortHashes(hashdata.Hashes, hashdata.Indices, Files.Count);
    //        // hashdata.Hashes.ToList().Sort();
    //        hashdata.Hashes.ToList().OrderBy((x) => x >> hashdata.PrehashShift);

    //        /* Generating bucket list */
    //        hashdata.BucketOffsets = GenBucketList((uint)Files.Length, hashdata.BucketOffsets, hashdata.Hashes, (uint)hashdata.PrehashShift);

    //        /* Set offset variables */
    //        hashdata.BucketOffsetsOffset = 0x10; /* Constant */
    //        hashdata.HashesOffset = (uint)(hashdata.BucketOffsetsOffset + hashdata.BucketOffsetsSize * 2);
    //        hashdata.IndicesOffset = (uint)(hashdata.HashesOffset + Files.Length * 4);

    //        return hashdata;
    //    }
    //}
    public class DatHashUtil
    {
        static int CalculateShift(int FileAmount)
        {
            for (int i = 0; i < 31; i++)
                if (1 << i >= FileAmount)
                    return 31 - i;
            return 0;
        }
        struct NameIndexHash
        {
            public string Filename;
            public ushort Index;
            public int Crc32Hash;
        };
        public static DatHashData GenerateHashData(DatFileEntry[] Files)
        {
            DatHashData HashData = new();

            HashData.PrehashShift = CalculateShift(Files.Length);

            HashData.BucketOffsets = new();

            for (int i = 0; i < 1 << (31 - HashData.PrehashShift); i++)
            {
                HashData.BucketOffsets.Add(0xFFFF);
            }

            List<NameIndexHash> NIH = new();
            for (ushort i = 0; i < Files.Length; i++)
            {
                var entry = new NameIndexHash
                {
                    Filename = Files[i].Name,
                    Index = i,
                    Crc32Hash = (int)(Crc32.HashToUInt32(Encoding.ASCII.GetBytes(Files[i].Name.ToLower())) & ~0x80000000)
                };
                NIH.Add(entry);
                Console.WriteLine($"{Path.GetFileName(entry.Filename).PadLeft(64)}  |  {entry.Index}  |  {entry.Crc32Hash}");
            }

            NIH.OrderBy((x) => x.Crc32Hash >> HashData.PrehashShift);

            HashData.Hashes = new();
            HashData.Indices = new();

            foreach (var hash in NIH)
            {
                HashData.Hashes.Add(hash.Crc32Hash);
            }

            HashData.Hashes.OrderBy((x) => x >> HashData.PrehashShift);

            for (int i = 0; i < NIH.Count; i++)
            {
                if (HashData.BucketOffsets[NIH[i].Crc32Hash >> HashData.PrehashShift] == 0xFFFF)
                    HashData.BucketOffsets[NIH[i].Crc32Hash >> HashData.PrehashShift] = (ushort)i;
                HashData.Indices.Add(NIH[i].Index);
            }

            HashData.BucketOffsetsSize = (uint)HashData.BucketOffsets.Count * 2;

            return HashData;
        }
    }
}
