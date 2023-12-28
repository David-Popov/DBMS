using Database_Management_System.DataStructures;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Select : Query
    {
        private string[] columns;
        private MyPair<string, string>? orderByColumn;
        private string? expr;
        private string[]? exprColNames;
        private bool hasWhere;
        private bool hasDistinct;
        private bool hasOrderBy;
 
        public Select(string src) 
        {
            int fromStart = StringFormatter.IndexOf(src, Queries.from);
            int start = 0;
            if (StringFormatter.IndexOf(src, Queries.distinct) != -1)
            {
                start += Queries.distinct.Length + 1;
                hasDistinct = true;
            }

            int orderbyStart = StringFormatter.IndexOf(src, Queries.orderby);
            int end = src.Length - 1;
            if (orderbyStart != -1)
            {
                end = orderbyStart - 1;
                hasOrderBy = true;
            }
            else
                orderByColumn = null;

            int whereStart = StringFormatter.IndexOf(src, Queries.where);
            if (whereStart != -1)
            {
                hasWhere = true;
                expr = ExpressionParser.GetParsableExpression(StringFormatter.Substring(src, whereStart + Queries.where.Length, end));
                exprColNames = ExpressionParser.ExtractColumnNames(expr);
            }
            else
            {
                expr = null;
                exprColNames = null;
            }

            columns = StringFormatter.Split(StringFormatter.Substring(src, start, fromStart - 2), ',');
            _tableName = StringFormatter.Substring(src, fromStart + Queries.from.Length + 1, whereStart - 2);
            if (hasOrderBy)
            {
                var cols = StringFormatter.Substring(src, orderbyStart + Queries.orderby.Length);
                var splitted = StringFormatter.Split(cols);
                if (splitted.Length == 1)
                    orderByColumn = new MyPair<string, string>(splitted[0], Queries.ASC);
                orderByColumn = new MyPair<string, string>(splitted[0], splitted[1]);
            }
        }

        private MyList<int> Where(DataArray data, MyList<int> rows)
        {
            int[] colIndexes = data.GetColumnIndexes(exprColNames!);
            for (int i = 0; i < data.Length; ++i)
            {
                string exprCopy = "";
                for (int j = 0; j < colIndexes.Length; ++j)
                    exprCopy = StringFormatter.ReplaceAll(expr!, exprColNames![j], data[i][colIndexes[j]]);

                Expression expression = ExpressionParser.ParseExpression(exprCopy)!;
                if (expression.Evaluate())
                    rows.Add(i);
            }

            return rows;
        }

        private void OrderBy()
        {

        }

        private MyList<int> Distinct(DataArray data, MyList<int> rows)
        {
            int[] colIndexes = data.GetColumnIndexes(columns);
            for (int i = 0; i < colIndexes.Length; ++i)
            {
                Dictionary<string, MyPair<int, int>> counter = new Dictionary<string, MyPair<int, int>>();
                for (int j = 0; j < rows.Length; ++j)
                {
                    var key = data[rows[j]][colIndexes[i]];
                    if (!counter.ContainsKey(key))
                        counter.Add(key, MyPair<int, int>.MakePair(rows[j], 1));
                    else
                        ++counter[key].Second;
                }

                foreach (var item in counter)
                {
                    var count = item.Value;
                    while (count.Second > 1)
                    {
                        rows.Remove(count.First);
                        --count.Second;
                    }
                }
            }

            return rows;
        }

        public override void Execute()
        {
            DataArray data = new DataArray(_tableName);
            MyList<int> rows = new MyList<int>();
            if (hasWhere)
                Where(data, rows);

            if (hasDistinct)
                Distinct(data, rows);

            //if (hasOrderBy)

            data.Print(rows.ToArray());
        }
    }
}
