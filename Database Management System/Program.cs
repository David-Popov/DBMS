using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;
using Database_Management_System.Validators.Constants;


//var createQuery = QueryParser.CreateQuery("CreateTable Sample(Id:int, Name:string, BirthDate:date default \"01.01.2022\")");

//createQuery.execute();

var query = QueryParser.CreateQuery("Insert INTO Sample (Id, Name) VALUES (1, \"Gosho\")");

query.execute();

var data = new DataArray("Sample", new FileStream(@$"{Utility.filesFolderPath}Sample.bin", FileMode.Open, FileAccess.ReadWrite));

data.Print();

