using Database_Management_System.FileManagement;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.DataStructures
{
    public class DataArray
    {
        private readonly BinaryReader _br;
        private readonly BinaryWriter _bw;
        private readonly Stream _stream;

        private string _FileName { get; set; }
        private int _RowSize { get; set; }
        public int Offset;
        public int Count;
        public int Lenght;

        public DataArray(Stream stream, int lenght, int offset, string fileName)
        {
            Offset = offset;
            Lenght = lenght;
            _stream = stream;

            //_bw = new BinaryWriter(stream);
            _br = new BinaryReader(stream);
            _stream.SetLength(lenght * (Utility.sizeInt + Utility.sizeString + Utility.sizeDateTime));
            _FileName = fileName;

        }

        private RowValues BytesToString()
        {
            var metadata = MetaHandler.ReadFile(_FileName, out int rowSize, out int rowCount);
            _RowSize = rowSize;

            RowValues rowValues = new RowValues(metadata.Length);

            for (int i = 0; i < metadata.Length; i++)
            {
                switch (metadata[i].type)
                {
                    case "int":
                        rowValues.Data[i] = ConvertBytesToInt(_br.ReadBytes(Utility.sizeInt));
                        break;
                    case "string":
                        rowValues.Data[i] = ConvertBytesToString(_br.ReadBytes(Utility.sizeString));
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
                _stream.Seek(Offset + index * _RowSize, SeekOrigin.Begin);
                return BytesToString();
            }
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
