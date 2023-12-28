using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Database_Management_System.String;
using Database_Management_System.Validators.Constants;

namespace Database_Management_System.FileManagement
{
    public class ColumnInfo
    {
        public string name;
        public string type;
        public string _defaultValue;

        private bool _hasDefault;

        public int GetMaxPrintLen()
        {
            int len = name.Length;
            len = len < type.Length ? type.Length : len;
            if(_hasDefault)
                len = (len < _defaultValue.Length) ? _defaultValue.Length : len;

            len = len < 13 ? 13 : len;

            return len;
        }

        public int GetDataSize()
        {
            switch(type)
            {
                case Utility.typeString: return Utility.sizeString;
                case Utility.typeInt: return Utility.sizeInt;
                case Utility.typeDateTime: return Utility.sizeDateTime;

                default:
                    throw new Exception("Invalid typing");
            }
        }

        public ColumnInfo()
        {
            this.name = string.Empty;
            this.type = string.Empty;
            _hasDefault = false;
            _defaultValue = string.Empty;
        }

        public ColumnInfo(string name, string type, bool hasDefault, string defaultValue = "")
        {
            this.name = name;
            this.type = type;
            _hasDefault = hasDefault;
            _defaultValue = defaultValue;
        }

        public void WriteColumn(BinaryWriter writer)
        {
            writer.Write(name);
            writer.Write(type);
            writer.Write(_hasDefault);
            if (_hasDefault)
                writer.Write(_defaultValue);
        }

        public void ReadColumn(BinaryReader reader)
        {
            name = reader.ReadString();
            type = reader.ReadString();
            _hasDefault = reader.ReadBoolean();
            if (_hasDefault)
                _defaultValue = reader.ReadString();
        }

        public string ToString(int padding)
        {
            double namePadding = Math.Ceiling(((double)(padding - name.Length)) / 2 + 1);
            double typePadding = Math.Ceiling(((double)(padding - type.Length)) / 2 + 1);
            double defaultPadding = Math.Ceiling(((double)(padding - _defaultValue.Length)) / 2 + 1);
            double nonePadding = Math.Ceiling((double)(padding / 2) + 1);

            return $"|{StringFormatter.FixedPrint(name, (int)namePadding)}|"
                       + $"{StringFormatter.FixedPrint(type, (int)typePadding)}|"
                       + (_hasDefault ? $"{StringFormatter.FixedPrint(_defaultValue, (int)defaultPadding)}|"
                       : (StringFormatter.FixedPrint("-", (int)nonePadding) + "|"));
        }
    }
}
