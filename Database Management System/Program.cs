using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;

string input = StringFormatter.ClearInput("Sample(Id:int, Name:string, BirthDate:date default \"01.01.2022\")");
Drop drop = new Drop("ABV");
drop.execute();