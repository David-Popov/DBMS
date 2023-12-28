using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Database_Management_System.Logger
{
    public static class MessageLogger
    {
        public static void WrongOperation(string operation) => Console.WriteLine($"{operation} operation is not supported!");
        public static void NonExistingType() => Console.WriteLine("You have entered a type which is not supported!");
        public static void NonExistingColumn(string columnType) => Console.WriteLine($"Column of type {columnType} do not exist!");
        public static void NonExistingValue(string columnName,string columnValue) => Console.WriteLine($"{columnName} do not have record with value: {columnValue}!");
        public static void NonExistingTable(string tableName) => Console.WriteLine($"Table with name {tableName} do not exist");
        public static void NonExistingMetaTableFile(string tableName) => Console.WriteLine($"Meta file for {tableName} with name {Utility.metaExtention}{tableName} do not exist");
        public static void AppError(string message) => Console.WriteLine(message);
        public static void NotDeletedRecordMessage() => Console.WriteLine("Record was not deleted successfuly!");
        public static void NotInsertedRecordMessage() => Console.WriteLine("Record was not inserted successfuly!");
        public static void DeletedRecordMessage() => Console.WriteLine("Record was deleted successfuly!");
        public static void InsertedRecordMessage() => Console.WriteLine("Record was inserted successfuly!");
        public static void RecordNotFoundByGivenConditions() => Console.WriteLine("There is no record found by the given conditions!");
        public static void DefaultTypeError() => Console.WriteLine("Default was written wrong! Should be => \"default\".");


    }
}
