using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;
using Database_Management_System.Validators.Constants;


//string createinput = "CreateTable Sample(Id:int, Name:string, BirthDate:date default \"01.01.2022\")";
//Query query2 = QueryParser.CreateQuery(createinput);
//query2.execute();

//string input = "Insert INTO Sample (Id, Name) VALUES (1, \"Иван\")";
//Query query = QueryParser.CreateQuery(input);
//query.execute();

DataArray arr = new DataArray(new FileStream($"{Utility.filesFolderPath}Sample.bin", FileMode.Open, FileAccess.ReadWrite), 2, 0, "Sample");

var data = arr[0];

Console.WriteLine();