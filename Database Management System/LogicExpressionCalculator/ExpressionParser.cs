using Database_Management_System.DataStructures;
using Database_Management_System.Logger;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;

namespace Database_Management_System.LogicExpressionCalculator
{
    public static class ExpressionParser
    {
        //Idea
        private static class Formatter
        {
            //remove every space from the source string
            private static string SpaceRemover(string src)
            {
                if (StringFormatter.IsNullOrEmpty(src))
                {
                    throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < src.Length; ++i)
                {
                    if (src[i] != ' ')
                        sb.ConCat(src[i]);
                }

                return sb.C_str;
            }

            //Create a string from a reverse polish notation to regular expression with brackets - '(' ')' according to the given operations priority
            private static string bracketFormatter(string src)
            {
                if (StringFormatter.IsNullOrEmpty(src))
                {
                    throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
                }

                MyStack<string> vars = new MyStack<string>();

                int index = 1;
                while (index <= src.Length)
                {
                    if (LogicOperators.isOperator(src[index]))
                    {
                        char op = src[index];
                        ++index;

                        StringBuilder rhs = new StringBuilder(vars.Pop());
                        StringBuilder lhs = new StringBuilder(vars.Pop());

                        StringBuilder newString = "(" + lhs.ConCat(op) + rhs.ConCat(')');
                        vars.Push(newString.C_str);
                    }
                    else
                    {
                        StringBuilder temp = new StringBuilder();
                        while (src[index] != '|')
                            temp.ConCat(src[index++]);

                        vars.Push(temp.C_str);
                    }

                    ++index;
                }

                return vars.Pop();
            }

            //create reverse polish notation from source string expression where | is separator between operators and operands
            private static void RPN(string src, StringBuilder res, MyStack<char> operations, ref int i, ref bool firstAppend)
            {
                if (StringFormatter.IsNullOrEmpty(src))
                {
                    throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
                }

                if (LogicOperators.isOperator(src[i]))
                {
                    res.ConCat('|');
                    if (operations.IsEmpty() || firstAppend)
                    {
                        operations.Push(src[i]);
                        firstAppend = false;
                    }
                    else if (LogicOperators.Precedent(src[i]) >= LogicOperators.Precedent(operations.Peek()))
                        operations.Push(src[i]);
                    else
                    {
                        while (!operations.IsEmpty())
                        {
                            if (LogicOperators.Precedent(src[i]) <= LogicOperators.Precedent(operations.Peek()))
                            {
                                res.ConCat(operations.Pop());
                                res.ConCat('|');
                            }
                            else
                                break;
                        }
                        operations.Push(src[i]);
                    }
                }

                else
                {
                    if (src[i] != '(' && src[i] != ')')
                        res.ConCat(src[i]);
                }
            }

            //if there is not brackets makes reverse polish notation else just concatinates the string
            public static string Format(string src)
            {
                if (StringFormatter.IsNullOrEmpty(src))
                {
                    throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
                }

                StringBuilder res = new StringBuilder("|");
                MyStack<char> operations = new MyStack<char>();

                for (int i = 0; i < src.Length; ++i)
                {
                    if (src[i] == '(')
                    {
                        bool firstOp = true;
                        while (src[++i] != ')')
                            RPN(src, res, operations, ref i, ref firstOp);
                        while (!operations.IsEmpty())
                        {
                            res.ConCat('|');
                            res.ConCat(operations.Pop());
                        }
                    }
                    else
                    {
                        bool firstOp = false;
                        RPN(src, res, operations, ref i, ref firstOp);
                    }
                }

                res.ConCat('|');
                while (!operations.IsEmpty())
                {
                    res.ConCat(operations.Pop());
                    if (!operations.IsEmpty())
                        res.ConCat('|');
                }

                return bracketFormatter(SpaceRemover(res.C_str));
            }
        }

        //Gets expressions after where and process them so can create parsable expression for validation later
        public static string GetParsableExpression(string src)
        {
            if (StringFormatter.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            string[] operators = { LogicOperators.nEq, LogicOperators.or, LogicOperators.not,
                                   LogicOperators.and, LogicOperators.ltoEq, LogicOperators.mtoEq };
            foreach (var op in operators)
                src = StringFormatter.ReplaceAll(src, op, LogicOperators.GetReplacement(op));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < src.Length; ++i)
            {
                if (src[i] == LogicOperators.cNot)
                {
                    ++i;
                    while (!LogicOperators.isLogicExpressionOperator(src[i]))
                        sb.ConCat(src[i++]);

                    sb.ConCat(LogicOperators.GetOpposite(src[i]));
                    continue;
                }

                sb.ConCat(src[i]);
            }

            return Formatter.Format(sb.C_str);
        }

        //Gets expressions and extracts every column name from it
        public static string[] ExtractColumnNames(string src)
        {
            if (StringFormatter.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            MyList<string> res = new MyList<string>();
            for (int i = 0; i < src.Length; ++i)
            {
                StringBuilder s = new StringBuilder();
                bool firstIter = true;

                while (!Utility.isSpecialCharacter(src[i]))
                {
                    if (firstIter && Utility.isDigit(src[i]))
                        break;

                    s.ConCat(src[i++]);
                    firstIter = false;
                }

                if (src[i] == '\"')
                {
                    ++i;
                    while (src[i] != '\"')
                    {
                        ++i;
                        if (i >= src.Length)
                            break;
                    }

                }

                if (s.Length != 0)
                    res.Add(s.C_str);
            }

            return res.ToArray();
        }

        //Creates evaluatable expression from source string
        public static Expression? ParseExpression(string src)
        {
            if (src.Length == 0)
                return null;

            src = StringFormatter.Substring(src, 1, src.Length - 2);
            MyStack<char> brakets = new MyStack<char>();
            for (int i = 0; i < src.Length; ++i)
            {
                if (src[i] == '(')
                    brakets.Push(src[i]);
                else if (src[i] == ')')
                    brakets.Pop();
                else if (LogicOperators.isLogicExpressionOperator(src[i]) && brakets.IsEmpty())
                    return new BasicExpression(src[i],
                               StringFormatter.Substring(src, 0, i - 1),
                               StringFormatter.Substring(src, i + 1, src.Length - 1));
                else if (LogicOperators.isLogicOperator(src[i]) && brakets.IsEmpty())
                    return new BinaryExpression(src[i],
                               ParseExpression(StringFormatter.Substring(src, 0, i - 1))!,
                               ParseExpression(StringFormatter.Substring(src, i + 1, src.Length - 1))!);
            }

            return null;
        }

        //WHERE Name < David AND Id > 5
        //((Name<David)&(Id>5))
        //(Name<David)&(Id>5)
        //(Name<David)&(Id>5)
        //Name<David
    }
}
