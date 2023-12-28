using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.Algorythms;
using System.Collections;

//var q = QueryParser.CreateQuery("Delete FROM Sample WHERE BirthDate > \"01.01.1800\"");
//q.Execute();

//var q = QueryParser.CreateQuery("Insert INTO Sample (Id, Name) VALUES (3, \"David\")");
//q.Execute();

DataArray data = new DataArray("Sample");
data.Print([0, 1]);

var dict = new MyDictionary<string, string>();
dict.Add("banana", "1.23");
dict.Add("apple", "1.25");
dict.Add("apple", "1.21");
dict.Add("peach", "1.22");

foreach (var item in dict)
{
    Console.WriteLine($"{item.Key} - {item.Value}");
}

/*var drop = QueryParser.CreateQuery("DropTable Sample");

var select = QueryParser.CreateQuery("Select Name, DateBirth FROM Sample WHERE Id <> 5");
select.Execute();*/
