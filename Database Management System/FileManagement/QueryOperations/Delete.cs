using Database_Management_System.String;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Delete : Query
    {
        private LogicExpressionCalculator.Expressions.Expression expr;

        public Delete(string src) 
        {
            _tableName = StringFormatter.Substring(src, 0, ' ');
            expr = ExpressionParser.ParseExpression(
                   ExpressionParser.GetParsableExpression(
                   StringFormatter.Substring(src, _tableName.Length + Queries.where.Length + 2)))!;
        }

        public override void execute()
        {
            
        }
    }
}
