using Database_Management_System.FileManagement;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Database_Management_System.DataStructures
{
    public class DataArray
    {
        private readonly BinaryReader _br;
        private readonly BinaryWriter _bw;
        private readonly Stream _stream;
        private readonly ColumnInfo[] Columns;

        private string FileName;
        private int RowSize;
        public int Offset = 0;
        public int Length;

        public DataArray(string fileName, Stream stream)
        {
            FileName = fileName;
            Columns = MetaHandler.ReadFile(FileName, out int rowSize, out int rowCount);
            RowSize = rowSize;
            Length = rowCount;

            _stream = stream;
            _br = new BinaryReader(_stream);
            _bw = new BinaryWriter(_stream);
            _stream.SetLength(Length * (Utility.sizeInt + Utility.sizeString + Utility.sizeDateTime));
        }

        private RowValues BytesToString(ColumnInfo[] metadata)
        {

            RowValues rowValues = new RowValues(metadata.Length);

            for (int i = 0; i < metadata.Length; i++)
            {
                switch (metadata[i].type)
                {
                    case "int":
                        rowValues.Data[i] = ConvertBytesToInt(_br.ReadBytes(Utility.sizeInt));
                        break;
                    case "string":
                        string stringValue = ConvertBytesToString(_br.ReadBytes(Utility.sizeString));
                        rowValues.Data[i] = StringFormatter.Substring(stringValue, 0, '\0');
                        break;
                    case "date":
                        string dateValue = ConvertBytesToString(_br.ReadBytes(Utility.sizeDateTime));
                        rowValues.Data[i] = StringFormatter.Substring(dateValue, 0, '\0');
                        break;
                }
            }
            return rowValues;
        }

        private string ConvertBytesToInt(byte[] bytesAsNum)
        {
            var result = BitConverter.ToInt32(bytesAsNum, 0);
            return result.ToString();
        }

        private string ConvertBytesToString(byte[] bytesAsString)
        {
            var result = Encoding.Default.GetString(bytesAsString, 0, Utility.sizeString);
            return result;
        }

        public RowValues this[int index]
        {
            get
            {
                _stream.Seek(Offset + index * RowSize, SeekOrigin.Begin);
                return BytesToString(Columns);
            }
        }

        public void Print()
        {
            int padding = 0;

            foreach (var col in Columns)
            {
                int len = col.getMaxPrintLen();
                padding = padding < len ? len : padding;
            }

            for (int i = 0; i < this.Length; i++)
            {
                var data = this[i].Data;

                for (int j = 0; j < data.Length; j++)
                {
                    padding = padding < data[j].Length ? data[j].Length : padding;
                }
            }

            Refresh();

            for (int i = 0; i < Columns.Length; i++)
            {
                double namePadding = Math.Ceiling(((double)(padding - Columns[i].name.Length)) / 2 + 1);
                if (i == 0)
                    Console.Write($"|{StringFormatter.FixedPrint(Columns[i].name, (int)namePadding)}|");
                else
                    Console.Write($"{StringFormatter.FixedPrint(Columns[i].name, (int)namePadding)}|");
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', (padding + 4) * Columns.Length + 1));

            for (int i = 0; i < this.Length; i++)
            {
                var data = this[i].Data;
                for (int j = 0; j < data.Length; j++)
                {
                    double namePadding = Math.Ceiling(((double)(padding - data[j].Length)) / 2 + 1);
                    if (j == 0)
                        Console.Write($"|{StringFormatter.FixedPrint(data[j], (int)namePadding)}|");
                    else
                        Console.Write($"{StringFormatter.FixedPrint(data[j], (int)namePadding)}|");
                }
                Console.WriteLine();
            }
        }


        public void DeleteRecord(int index)
        {
            long sizeBefore = index * RowSize;

            long offsetToDelete = sizeBefore;

            long sizeAfter = _stream.Length - offsetToDelete - RowSize;

            if (sizeAfter > 0)
            {
                byte[] buffer = new byte[sizeAfter];

                _stream.Seek(offsetToDelete + RowSize, SeekOrigin.Begin);

                _stream.Read(buffer, 0, buffer.Length);

                _stream.Seek(offsetToDelete, SeekOrigin.Begin);

                _stream.Write(buffer, 0, buffer.Length);
            }

            _stream.SetLength(_stream.Length - RowSize);

            --Length;

            MetaHandler.UpdateRowCountOnDelete(FileName);
        }

        public void Refresh()
        {
            _br.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        public class RowValues
        {
            public string[] Data { get; set; }

            public RowValues(int columnCount)
            {
                Data = new string[columnCount];
            }
        }
    }
}
