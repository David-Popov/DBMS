using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Validators.Constants
{
    public static class LogicOperators
    {
        public const string nEq = "<>";
        public const string mtoEq = ">=";
        public const string ltoEq = "<=";
        public const string and = "AND";
        public const string not = "NOT";
        public const string or = "OR";

        public const char lt = '<';
        public const char mt = '>';
        public const char cMtoEq = '}';
        public const char cLtoEq = '{';
        public const char cEq = '=';
        public const char cAnd = '&';
        public const char cOr = '^';
        public const char cNot = '!';
        public const char cNEq = '%';

        public static char GetReplacement(string op)
        {
            switch (op)
            {
                case LogicOperators.nEq: return cNEq;
                case LogicOperators.and: return cAnd;
                case LogicOperators.not: return cNot;
                case LogicOperators.or: return cOr;
                case LogicOperators.ltoEq: return cLtoEq;
                case LogicOperators.mtoEq: return cMtoEq;

                default: throw new Exception("Invalid logic operator");
            }
        }

        public static bool isLogicOperator(char c)
        {
            if (c == cAnd || c == cOr)
                return true;
            return false;
        }

        public static bool isLogicExpressionOperator(char c)
        {
            if (c == cEq || c == lt || c == mt || c == cMtoEq || c == cLtoEq || c == cNEq)
                return true;
            return false;
        }

        public static int Precedent(char c)
        {
            switch (c)
            {
                case LogicOperators.cAnd:
                case LogicOperators.cOr: return 1;

                default: return 2;
            }
        }

        public static bool isOperator(char c)
        {
            return isLogicOperator(c) | isLogicExpressionOperator(c);
        }

        public static char GetOpposite(char c)
        {
            switch (c)
            {
                case LogicOperators.lt: return LogicOperators.cMtoEq;
                case LogicOperators.mt: return LogicOperators.cLtoEq;
                case LogicOperators.cLtoEq: return LogicOperators.mt;
                case LogicOperators.cMtoEq: return LogicOperators.lt;
                case LogicOperators.cEq: return LogicOperators.cNEq;
                case LogicOperators.cNEq: return LogicOperators.cEq;

                default: throw new Exception("Operator opposite not found");
            }
        }
    }
}
