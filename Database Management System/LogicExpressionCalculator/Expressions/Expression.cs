using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.LogicExpressionCalculator.Expressions
{
    public abstract class Expression
    {
        abstract public bool Evaluate();
    }
}
