using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.LogicExpressionCalculator.Expressions
{
    public class BasicExpression : Expression
    {
        char op;
        string left;
        string right;

        public BasicExpression(char op, string left, string right)
        {
            this.op = op;
            this.left = StringFormatter.Trim(left, '\"');
            this.right = StringFormatter.Trim(right, '\"');
        }

        public override bool evaluate()
        {
            int result = StringFormatter.Compare(left, right);
            switch (op) 
            {
                case LogicOperators.lt: return result == -1;
                case LogicOperators.cLtoEq: return result == 0 || result == -1;
                case LogicOperators.mt: return result == 1;
                case LogicOperators.cMtoEq: return result == 0 || result == 1;
                case LogicOperators.cEq: return result == 0;
                case LogicOperators.cNEq: return result == 1 || result == -1;

                default: throw new Exception("Unknown operator!");
            }
        }
    }
}
