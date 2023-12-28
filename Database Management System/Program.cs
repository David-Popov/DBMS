using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.LogicExpressionCalculator;

//var q = QueryParser.CreateQuery("Delete FROM Sample WHERE Name <> \"David\"");
//q.Execute();

//var q = QueryParser.CreateQuery("Insert INTO Sample (Id, Name) VALUES (3, \"David\")");
//q.Execute();

DataArray data = new DataArray("Sample");
data.Print();
