using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming
{
    public static class ObjectUtil
    {
        public static void DeepCopy(object sourceObj, object destinationObj)
        {
            Type t = sourceObj.GetType();
            PropertyInfo[] props = t.GetProperties();
            FieldInfo[] fields = t.GetFields();

            Type c = destinationObj.GetType();
            PropertyInfo[] cprops = c.GetProperties();
            FieldInfo[] cfields = c.GetFields();

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo source = props[i];
                PropertyInfo dest = cprops.FirstOrDefault(k => k.Name == source.Name);

                if (dest != null &&
                    source.PropertyType == dest.PropertyType &&
                    dest.CanWrite)
                {
                    // TODO: this is dangerous for lists/dictionaries if the handler changes the size of anything
                    object value = source.GetValue(sourceObj, null);
                    dest.SetValue(destinationObj, value, null);
                }
                else
                {
                    FieldInfo fdest = cfields.FirstOrDefault(k => k.Name == source.Name);
                    if (fdest != null &&
                        source.PropertyType == fdest.FieldType)
                    {
                        object value = source.GetValue(sourceObj, null);
                        fdest.SetValue(destinationObj, value);
                    }
                }
            }

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo source = fields[i];
                FieldInfo dest = cfields.FirstOrDefault(k => k.Name == source.Name);
                if (dest == null ||
                    source.FieldType != dest.FieldType)
                {
                    continue;
                }

                object value = source.GetValue(sourceObj);
                dest.SetValue(destinationObj, value);
            }
        }
    }
}
