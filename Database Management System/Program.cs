using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;

string input = StringFormatter.ClearInput("Sample(Id:int, Name:string, BirthDate:date default “01.01.2022”)");
Create create = new Create(input);
create.execute();
TableInfo tableInfo = new TableInfo("Sample");
tableInfo.execute();