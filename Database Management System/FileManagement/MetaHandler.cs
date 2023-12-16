using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Database_Management_System.Validators.Constants;

namespace Database_Management_System.FileManagement
{
    public static class MetaHandler
    {
        public static void CreateFile(string fileName, ColumnInfo[] infos)
        {
            using (FileStream stream = new FileStream($"{Utility.metaExtention}{fileName}.bin", FileMode.Create, FileAccess.Write))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(infos.Length);
                foreach (ColumnInfo info in infos)
                    info.WriteColumn(writer);
            }
        }

        public static ColumnInfo[] ReadFile(string fileName) 
        {
            FileStream stream = new FileStream($"{Utility.metaExtention}{fileName}.bin", FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            ColumnInfo[] infos = new ColumnInfo[reader.ReadInt32()];
            for (int i = 0; i < infos.Length; ++i)
            {
                infos[i] = new ColumnInfo();
                infos[i].ReadColumn(reader);

            }

            return infos;
        }
    }
}
