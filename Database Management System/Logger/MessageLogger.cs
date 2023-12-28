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
        public static string WrongOperation() => "Operation is not supported!";
        public static string NonExistingType() => "You have entered a type which is not supported!";
        public static string NonExistingColumn(string columnType) => $"Column of type {columnType} do not exist!";
        public static string NonExistingValue(string columnName,string columnValue) => $"{columnName} do not have record with value: {columnValue}!";
        public static string NonExistingTable() => "Table with that name do not exist";
        public static string NonExistingMetaTableFile(string tableName) => $"Meta file for {tableName} with name {Utility.metaExtention}{tableName} do not exist";
        public static string AppError(string message) => message;
        public static string NotDeletedRecordMessage() => "Record was not deleted successfuly!";
        public static string NotInsertedRecordMessage() => "Record was not inserted successfuly!";
        public static string DeletedRecordMessage() => "Record was deleted successfuly!";
        public static string InsertedRecordMessage() => "Record was inserted successfuly!";
        public static string RecordNotFoundByGivenConditions() => "There is no record found by the given conditions!";
        public static string DefaultTypeError() => "Default was written wrong! Should be => \"default\".";
        public static string NullOrEmptyString() => "Empty string!";
        public static string WrongStartPosition() => "Invalid start position!";
        public static string StartIdxGreaterThanEndIdx() => $"Start index was greater than end index!";
        public static string UnknownOperator() => "Unknown operator!";
        public static string EmptyFileName() => "Empty file name!!";


    }
}
