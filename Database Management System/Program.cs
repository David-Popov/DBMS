using Database_Management_System.String;
using Database_Management_System.DataStructures;
using Database_Management_System.FileManagement;
using Database_Management_System.FileManagement.QueryOperations;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.LogicExpressionCalculator;

/*var q = QueryParser.CreateQuery("Delete FROM Sample WHERE Id <> 2");
q.execute();*/

DataArray data = new DataArray("Sample");
data.Print();
