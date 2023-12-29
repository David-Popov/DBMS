using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.Algorithms;
using System.Collections;

while (true)
{
    try
    {
        string input = Console.ReadLine()!;
        var query = QueryParser.CreateQuery(input);
        query.Execute();
    }
    catch(Exception e) 
    { 
        Console.WriteLine(e.Message);
    }
}

//var q = QueryParser.CreateQuery("Delete FROM Sample WHERE BirthDate > \"01.01.1800\"");
//q.Execute();

//var q = QueryParser.CreateQuery("Insert INTO Sample (Id, Name) VALUES (3, \"David\")");
//q.Execute();

/*var create = QueryParser.CreateQuery("CreateTable SortTester (ID: int, Name: string, Surname:string)");
create.Execute();*/

/*var insert = QueryParser.CreateQuery("Insert INTO SortTester(ID, Name, Surname) VALUES (2, \"Maria\", \"Shopova\")");
insert.Execute();

var select = QueryParser.CreateQuery("Select DISTINCT ID FROM SortTester ORDER BY ID DESC");
select.Execute();*/

/*DataArray data = new DataArray("Sample");
data.PrintSelectedRecordsAndColumns([0,3],[0,1]);*/

/*var arr = new MyPair<int, int>[4];
arr[0] = new MyPair<int, int>(2, 1);
arr[1] = new MyPair<int, int>(3, 1);
arr[2] = new MyPair<int, int>(1, 1);
arr[3] = new MyPair<int, int>(4, 1);
MyPair<int, int>[] res = QuickSort.Sort(arr, 0, arr.Length - 1, false);

foreach (var item in res)
{
    Console.WriteLine(item.First + " " + item.Second);
}
*/
//int[] numbers = { 10, 7, 8, 9, 1, 5 };
//Console.WriteLine("Original array:");

//QuickSort.Sort(numbers, 0, numbers.Length - 1, true);

//Console.WriteLine("\nSorted array in ascending order:");

//foreach (var item in numbers)
//{
//    Console.WriteLine(item);
//}

/*var drop = QueryParser.CreateQuery("DropTable Sample");

var select = QueryParser.CreateQuery("Select Name, DateBirth FROM Sample WHERE Id <> 5");
select.Execute();*/
