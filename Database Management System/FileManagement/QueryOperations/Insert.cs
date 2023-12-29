using Database_Management_System.Logger;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System.Text;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Insert : Query
    {
        private string[] columns;
        private string[] values;

        public Insert(string src)
        {
            _tableName = StringFormatter.Substring(src, 0, '(');

            int columnsEnd = StringFormatter.IndexOf(src, ")");
            columns = StringFormatter.Split(StringFormatter.Substring(
                      src, _tableName.Length + 1, columnsEnd - 1), ',');
            values = StringFormatter.Split(StringFormatter.Substring(
                      src, columnsEnd + Queries.values.Length + 2,
                      StringFormatter.IndexOf(src, ")", columnsEnd
                      + Queries.values.Length + 1) - 1), ',');
        }

        private void WriteData(BinaryWriter bw, string value, string type, bool writeDefault = false, string defaultValue = "")
        {
            //value = StringFormatter.Trim(value, '\"');

            switch (type)
            {
                case "int":
                    if (writeDefault)
                    {
                        byte[] bytes = BitConverter.GetBytes(int.Parse(defaultValue));
                        bw.Write(bytes);
                    }
                    else
                    {
                        byte[] bytes = BitConverter.GetBytes(int.Parse(value));
                        bw.Write(bytes);
                    }
                    break;
                case "string":
                    if (writeDefault)
                    {
                        defaultValue = StringFormatter.Trim(defaultValue, '\"');
                        string input = StringFormatter.PadLeft(defaultValue, 255 - defaultValue.Length, '\0');
                        byte[] byteArray = Encoding.UTF8.GetBytes(input);
                        bw.Write(byteArray);
                    }
                    else
                    {
                        value = StringFormatter.Trim(value, '\"');
                        string input = StringFormatter.PadLeft(value, 255 - value.Length, '\0');
                        byte[] byteArray = Encoding.UTF8.GetBytes(input);
                        bw.Write(byteArray);
                    }
                    break;
                case "date":
                    if (writeDefault)
                    {
                        defaultValue = StringFormatter.Trim(defaultValue, '\"');
                        string input = StringFormatter.PadLeft(defaultValue, 255 - defaultValue.Length, '\0');
                        byte[] byteArray = Encoding.UTF8.GetBytes(input);
                        bw.Write(byteArray);
                    }
                    else
                    {
                        value = StringFormatter.Trim(value, '\"');
                        string input = StringFormatter.PadLeft(value, 255 - value.Length, '\0');
                        byte[] byteArray = Encoding.UTF8.GetBytes(input);
                        bw.Write(byteArray);
                    }
                    break;
            }
        }

        public override void Execute()
        {
            if (_tableName == string.Empty)
            {
                throw new ArgumentException(MessageLogger.EmptyFileName());
            }

            var metadata = MetaHandler.ReadFile(_tableName, out int rowSize, out int rowCount);

            using (FileStream fileStream = new FileStream(@$"{Utility.filesFolderPath}{_tableName}.bin", FileMode.Append, FileAccess.Write))
            {
                BinaryWriter bw = new BinaryWriter(fileStream);

                for (int i = 0; i < metadata.Length; i++)
                {
                    bool match = false;

                    for (int j = 0; j < columns.Length; j++)
                    {
                        if (metadata[i].name == columns[j])
                        {
                            match = true;
                            WriteData(bw, values[j], metadata[j].type);
                        }
                    }

                    if (!match)
                    {
                        WriteData(bw, string.Empty, metadata[i].type, true, metadata[i]._defaultValue);
                    }

                }

                MetaHandler.UpdateRowCountOnInsert(_tableName);

                Console.WriteLine($"Record successfully inserted.");
            }
        }
    }
}
