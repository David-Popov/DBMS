using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Database_Management_System.String;

namespace Database_Management_System.FileManagement
{
    public class ColumnInfo
    {
        private string name;
        private string type;
        private string _defaultValue;

        private bool _hasDefault;

        public int getMaxLen()
        {
            int len = name.Length;
            len = len < type.Length ? type.Length : len;
            if(_hasDefault)
                len = (len < _defaultValue.Length) ? _defaultValue.Length : len;

            len = len < 13 ? 13 : len;

            return len;
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
            //TODO
            double namePadding = Math.Ceiling(((double)(padding - name.Length)) / 2);
            double typePadding = Math.Ceiling(((double)(padding - type.Length)) / 2);
            double defaultPadding = Math.Ceiling(((double)(padding - _defaultValue.Length)) / 2);
            double nonePadding = Math.Ceiling(((double)(padding / 2)));

            return $"|{StringFormatter.PadRight(StringFormatter.PadLeft(
                       name, (int)namePadding), (int)namePadding)}|"
                       + $"{StringFormatter.PadRight(StringFormatter.PadLeft(
                            type, (int)typePadding), (int)typePadding)}|"
                       + (_hasDefault ? $"{StringFormatter.PadRight(StringFormatter.PadLeft(
                          _defaultValue, (int)defaultPadding), (int)defaultPadding)}|" :
                          (StringFormatter.PadRight(StringFormatter.PadLeft("-", (int)nonePadding), (int)nonePadding)) + "|");
        }
    }
}
