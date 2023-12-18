using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.LogicExpressionCalculator.Expressions
{
    public class UnaryExpression : Expression
    {
        char op;
        Expression expr;

        public UnaryExpression(char op, Expression expr)
        {
            this.op = op;
            this.expr = expr;
        }

        public override bool evaluate()
        {
            return !expr.evaluate();
        }
    }
}
