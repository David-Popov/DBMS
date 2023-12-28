using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.LogicExpressionCalculator.Expressions
{
    public class BinaryExpression : Expression
    {
        char op;
        Expression left;
        Expression right;

        public BinaryExpression(char op, Expression left, Expression right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        override public bool Evaluate()
        {
            switch (op) 
            { 
                case LogicOperators.cAnd: return left.Evaluate() && right.Evaluate();
                case LogicOperators.cOr: return left.Evaluate() || right.Evaluate();
            }

            return false;
        }
    }
}
