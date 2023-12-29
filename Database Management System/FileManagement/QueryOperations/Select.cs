using Database_Management_System.DataStructures;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Select : Query
    {
        private string[] columns;
        private MyPair<string, string>[]? orderByColumns;
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
                orderByColumns = null;

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
                var cols = StringFormatter.Substring(src, orderbyStart + Queries.orderby.Length, ',');
                for (int i = 0; i < cols.Length; ++i)
                {
                    var splitted = StringFormatter.Split(cols);
                    if (splitted.Length == 1)
                        orderByColumns![i] = new MyPair<string, string>(splitted[0], Queries.ASC);
                    orderByColumns![i] = new MyPair<string, string>(splitted[0], splitted[1]);
                }
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

        private MyList<int> OrderBy(DataArray data, MyList<int> rows)
        {
            //Sort by first and keep ranges to sort by second, third, etc.
            return rows;
        }

        private MyList<int> Distinct(DataArray data, MyList<int> rows)
        {
            int[] colIndexes = data.GetColumnIndexes(columns);
            MyDictionary<string, int> counter = new MyDictionary<string, int>();
            for (int i = 0; i < rows.Length; ++i)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < colIndexes.Length; ++j)
                    sb.ConCat(data[rows[i]][j]);

                string key = sb.C_str;
                if (!counter.HasKey(key))
                    counter.Add(key, rows[i]);
            }

            MyList<int> distinctRows = new MyList<int>();
            foreach (var item in counter)
                distinctRows.Add(item.Value);

            return distinctRows;
        }

        public override void Execute()
        {
            DataArray data = new DataArray(_tableName);
            MyList<int> rows = new MyList<int>();
            if (hasWhere)
                rows = Where(data, rows);

            if (hasDistinct)
                rows = Distinct(data, rows);

            if (hasOrderBy)
                rows = OrderBy(data, rows);

            data.Print(rows.ToArray());
        }
    }
}
