using Database_Management_System.DataStructures;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using Database_Management_System.LogicExpressionCalculator.Expressions;
using Database_Management_System.Algorithms;
using System;
using System.Globalization;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Select : Query
    {
        private string[] columns;
        private string[]? orderByColumns;
        private bool[]? orderStyle;
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
            {
                orderByColumns = null;
                orderStyle = null;
            }

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
            _tableName = StringFormatter.Substring(src, fromStart + Queries.from.Length + 1, ' ');
            if (hasOrderBy)
            {
                var cols = StringFormatter.Split(StringFormatter.Substring(src, orderbyStart + Queries.orderby.Length + 1), ',');
                orderByColumns = new string[cols.Length];
                orderStyle = new bool[cols.Length];
                for (int i = 0; i < cols.Length; ++i)
                {
                    var splitted = StringFormatter.Split(cols[i]);
                    if (splitted.Length == 1)
                    {
                        orderByColumns![i] = splitted[0];
                        orderStyle![i] = false;
                    }
                    else
                    {
                        orderByColumns![i] = splitted[0];
                        orderStyle![i] = splitted[1] != Queries.ASC;
                    }
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

        private MyList<int> OrderByDate(DataArray data, MyList<int> rows, int[] colIndexes,
                                        MyList<MyPair<int, int>> ranges, int columnIndex)
        {
            var pairs = new MyPair<DateTime, int>[rows.Length];
            for (int j = 0; j < rows.Length; ++j)
                pairs[j] = MyPair<DateTime, int>.MakePair(DateTime.ParseExact(data[rows[j]][colIndexes[columnIndex]], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), rows[j]);

            for (int j = 0; j < ranges.Length; ++j)
                QuickSort.Sort<MyPair<DateTime, int>>(pairs, ranges[j].First, ranges[j].Second, orderStyle![columnIndex]);

            ranges.Clear();
            for (int j = 0; j < rows.Length - 1; ++j)
            {
                int start = j;
                while (pairs[j].First == pairs[j + 1].First)
                {
                    ++j;

                    if (j >= rows.Length - 1)
                        break;
                }

                ranges.Add(MyPair<int, int>.MakePair(start, j));
            }

            for (int j = 0; j < rows.Length; ++j)
                rows[j] = pairs[j].Second;

            return rows;
        }

        private MyList<int> OrderByInt(DataArray data, MyList<int> rows, int[] colIndexes,
                                       MyList<MyPair<int, int>> ranges, int columnIndex)
        {
            var pairs = new MyPair<int, int>[rows.Length];
            for (int j = 0; j < rows.Length; ++j)
                pairs[j] = MyPair<int, int>.MakePair(int.Parse(data[rows[j]][colIndexes[columnIndex]]), rows[j]);

            for (int j = 0; j < ranges.Length; ++j)
                QuickSort.Sort<MyPair<int, int>>(pairs, ranges[j].First, ranges[j].Second, orderStyle![columnIndex]);

            ranges.Clear();
            for (int j = 0; j < rows.Length - 1; ++j)
            {
                int start = j;
                while (pairs[j].First == pairs[j + 1].First)
                {
                    ++j;

                    if (j >= rows.Length - 1)
                        break;
                }

                ranges.Add(MyPair<int, int>.MakePair(start, j));
            }

            for (int j = 0; j < rows.Length; ++j)
                rows[j] = pairs[j].Second;

            return rows;
        }

        private MyList<int> OrderByString(DataArray data, MyList<int> rows, int[] colIndexes,
                                          MyList<MyPair<int, int>> ranges, int columnIndex)
        {
            var pairs = new MyPair<string, int>[rows.Length];
            for (int j = 0; j < rows.Length; ++j)
                pairs[j] = MyPair<string, int>.MakePair(data[rows[j]][colIndexes[columnIndex]], rows[j]);

            for (int j = 0; j < ranges.Length; ++j)
                QuickSort.Sort<MyPair<string, int>>(pairs, ranges[j].First, ranges[j].Second, orderStyle![columnIndex]);

            ranges.Clear();
            for (int j = 0; j < rows.Length - 1; ++j)
            {
                int start = j;
                while (pairs[j].First == pairs[j + 1].First)
                {
                    ++j;

                    if (j >= rows.Length - 1)
                        break;
                }

                ranges.Add(MyPair<int, int>.MakePair(start, j));
            }

            for (int j = 0; j < rows.Length; ++j)
                rows[j] = pairs[j].Second;

            return rows;
        }

        private MyList<int> OrderBy(DataArray data, MyList<int> rows)
        {
            if (rows.Length == 0)
                return rows;

            int[] colIndexes = data.GetColumnIndexes(orderByColumns!);
            string[] colTypes = data.GetColumnTypes(colIndexes);
            MyList<MyPair<int, int>> ranges = new MyList<MyPair<int, int>>();
            ranges.Add(MyPair<int, int>.MakePair(0, rows.Length - 1));
            for (int i = 0; i < colIndexes!.Length; ++i)
            {
                switch (colTypes[i])
                {
                    case Utility.typeInt: rows = OrderByInt(data, rows, colIndexes, ranges, i); break;
                    case Utility.typeString: rows = OrderByString(data, rows, colIndexes, ranges, i); break;
                    case Utility.typeDateTime: rows = OrderByDate(data, rows, colIndexes, ranges, i); break;
                }
            }

            return rows;
        }

        private MyList<int> Distinct(DataArray data, MyList<int> rows, int[] colIndexes)
        {
            if (rows.Length == 0)
                return rows;

            MyDictionary<string, int> counter = new MyDictionary<string, int>();
            for (int i = 0; i < rows.Length; ++i)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < colIndexes.Length; ++j)
                    sb.ConCat(data[rows[i]][colIndexes[j]]);

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
            using (DataArray data = new DataArray(_tableName))
            {
                MyList<int> rows = new MyList<int>();

                int[] colIndexes;
                if (columns[0] == "*")
                {
                    colIndexes = new int[data.GetColumnsCount()];
                    for (int i = 0; i < colIndexes.Length; ++i)
                        colIndexes[i] = i;
                }
                else
                    colIndexes = data.GetColumnIndexes(columns);

                if (hasWhere)
                    rows = Where(data, rows);
                else
                {
                    for (int i = 0; i < data.Length; ++i)
                        rows.Add(i);
                }

                if (hasDistinct)
                    rows = Distinct(data, rows, colIndexes);

                if (hasOrderBy)
                    rows = OrderBy(data, rows);

                data.PrintSelectedRecordsAndColumns(rows.ToArray(), colIndexes);

                Console.WriteLine($"Returned {rows.Length} records.");
            }
        }
    }
}
