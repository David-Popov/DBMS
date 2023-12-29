using Database_Management_System.String;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Management_System.DataStructures;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Delete : Query
    {
        private string expr;
        private string[] colNames;

        public Delete(string src)
        {
            _tableName = StringFormatter.Substring(src, 0, ' ');
            expr = ExpressionParser.GetParsableExpression(
                   StringFormatter.Substring(src, _tableName.Length + Queries.where.Length + 2));
            colNames = ExpressionParser.ExtractColumnNames(expr);
        }

        public override void Execute()
        {
            using (DataArray data = new DataArray(_tableName))
            {
                int[] colIndexes = data.GetColumnIndexes(colNames);
                int deletedRecords = 0;

                for (int i = 0; i < data.Length; ++i)
                {
                    string exprCopy = "";
                    for (int j = 0; j < colIndexes.Length; ++j)
                        exprCopy = StringFormatter.ReplaceAll(expr, colNames[j], data[i][colIndexes[j]]);

                    Expression expression = ExpressionParser.ParseExpression(exprCopy)!;
                    if (expression.Evaluate())
                    {
                        data.DeleteRecord(i);
                        --i;
                        ++deletedRecords;
                    }
                }

                Console.WriteLine($"Successfully deleted {deletedRecords} records.");
            }
        }
    }
}
