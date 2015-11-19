using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;

namespace Nucleus
{
    public static class SerializationUtil
    {
        public static readonly Type SingleType = typeof(float);
        public const char Sep = ':';

        public enum StructsTypes
        {
            String, Single
        }

        public static void WriteData(object ob, BinaryWriter writer)
        {
            if (ob is string)
            {
                writer.Write((int)StructsTypes.String);
                writer.Write((string)ob);
            }
            else if (ob is float)
            {
                writer.Write((int)StructsTypes.Single);
                writer.Write((float)ob);
            }
            else if (ob is int)
            {
                writer.Write((int)StructsTypes.Single);
                writer.Write((int)ob);
            }
            else if (ob is uint)
            {
                writer.Write((int)StructsTypes.Single);
                writer.Write((int)ob);
            }
            else if (ob is short)
            {
                writer.Write((int)StructsTypes.Single);
                writer.Write((int)ob);
            }
            else if (ob is ushort)
            {
                writer.Write((int)StructsTypes.Single);
                writer.Write((int)ob);
            }
        }

        public static void ReadData(BinaryReader reader, out float output)
        {
            reader.BaseStream.Position += 4;
            output = reader.ReadSingle();
        }
    }
}
