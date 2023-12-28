using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Management_System.Logger;
using Database_Management_System.Validators.Constants;

namespace Database_Management_System.FileManagement
{
    public static class MetaHandler
    {
        public static void CreateFile(string fileName, ColumnInfo[] infos)
        {
            if (fileName == string.Empty || fileName == " ")
            {
                throw new ArgumentException(MessageLogger.EmptyFileName());
            }

            using (FileStream metaStream = new FileStream($"{Utility.metaFolderPath}{Utility.metaExtention}{fileName}.bin",
                   FileMode.Create, FileAccess.Write))
            {
                BinaryWriter writer = new BinaryWriter(metaStream);
                writer.Write(0);
                int rowSize = 0;
                foreach (var info in infos)
                    rowSize += info.GetDataSize();
                writer.Write(rowSize);
                writer.Write(infos.Length);
                foreach (ColumnInfo info in infos)
                    info.WriteColumn(writer);
            }

            FileStream fileStream = new FileStream($"{Utility.filesFolderPath}{fileName}.bin",
                                                   FileMode.Create, FileAccess.Write);
            fileStream.Close();
        }

        public static ColumnInfo[] ReadFile(string fileName, out int rowSize, out int rowCount)
        {
            if (fileName == string.Empty || fileName == " ")
            {
                throw new ArgumentException(MessageLogger.EmptyFileName());
            }

            FileStream stream = new FileStream($"{Utility.metaFolderPath}{Utility.metaExtention}{fileName}.bin",
                                               FileMode.Open, FileAccess.Read);

            BinaryReader reader = new BinaryReader(stream);
            rowCount = reader.ReadInt32();
            rowSize = reader.ReadInt32();
            ColumnInfo[] infos = new ColumnInfo[reader.ReadInt32()];
            for (int i = 0; i < infos.Length; ++i)
            {
                infos[i] = new ColumnInfo();
                infos[i].ReadColumn(reader);
            }

            stream.Close();

            return infos;
        }

        public static void UpdateRowCountOnInsert(string fileName)
        {
            if (fileName == string.Empty || fileName == " ")
            {
                throw new ArgumentException(MessageLogger.EmptyFileName());
            }

            using (FileStream stream = new FileStream(@$"{Utility.metaFolderPath}{Utility.metaExtention}{fileName}.bin", FileMode.Open, FileAccess.ReadWrite))
            {
                BinaryReader br = new BinaryReader(stream);
                BinaryWriter bw = new BinaryWriter(stream);

                var recordsCount = br.ReadInt32();

                br.BaseStream.Seek(0, SeekOrigin.Begin);
                bw.Write(++recordsCount);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }

        public static void UpdateRowCountOnDelete(string fileName)
        {
            using (FileStream stream = new FileStream(@$"{Utility.metaFolderPath}{Utility.metaExtention}{fileName}.bin", FileMode.Open, FileAccess.ReadWrite))
            {
                if (fileName == string.Empty || fileName == " ")
                {
                    throw new ArgumentException(MessageLogger.EmptyFileName());
                }

                BinaryReader br = new BinaryReader(stream);
                BinaryWriter bw = new BinaryWriter(stream);

                var recordsCount = br.ReadInt32();

                br.BaseStream.Seek(0, SeekOrigin.Begin);
                bw.Write(--recordsCount);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
