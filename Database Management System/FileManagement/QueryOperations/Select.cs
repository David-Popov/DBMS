using Database_Management_System.DataStructures;
using Database_Management_System.LogicExpressionCalculator;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Select : Query
    {
        private string[]? columns;
        private MyPair<string, string>[]? orderByColumns;
        private string? expr;
        private bool hasWhere;
        private bool hasDistinct;
        private bool hasOrderBy;

        //Select Name, DateBirth FROM Sample WHERE Id <> 5 AND DateBirth > “01.01.2000” 
        public Select(string src) 
        {
            int fromStart = StringFormatter.IndexOf(src, Queries.from);
            int start = 0;
            if (StringFormatter.IndexOf(src, Queries.distinct) != -1)
            {
                start += Queries.distinct.Length;
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
            }
            else
                expr = null;

            columns = StringFormatter.Split(StringFormatter.Substring(src, 0, fromStart - 2), ',');
            _tableName = StringFormatter.Substring(src, fromStart + Queries.from.Length + 1, whereStart - 2);
            if(hasOrderBy)
            {
                var cols = StringFormatter.Split(StringFormatter.Substring(src, orderbyStart + Queries.orderby.Length), ',');
                orderByColumns = new MyPair<string, string>[cols.Length];
                for (int i = 0; i < cols.Length; ++i)
                {
                    var splitted = StringFormatter.Split(cols[i]);
                    orderByColumns[i].First = splitted[0];
                    if (splitted.Length != 1)
                        orderByColumns[i].Second = splitted[1];
                }
            }
        }

        private void OrderBy()
        {

        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
