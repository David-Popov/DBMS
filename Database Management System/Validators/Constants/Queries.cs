﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Validators.Constants
{
    public static class Queries
    {
        public const string create = "CreateTable";
        public const string info = "TableInfo";
        public const string drop = "DropTable";
        public const string list = "ListTables";
        public const string insert = "Insert INTO";
        public const string delete = "Delete";
        public const string from = "FROM";
        public const string where = "WHERE";
        public const string distinct = "DISTINCT";
        public const string orderby = "ORDER BY";

        public static bool isQuery(string src)
        {
            if (src != create && src != info && src != drop 
                && src != list && src != insert && src != delete) 
                return false;

            return true;
        }
    }
}