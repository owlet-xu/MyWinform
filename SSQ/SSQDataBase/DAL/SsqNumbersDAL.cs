using System;
using System.Collections.Generic;
using System.Text;

using System.Data.OleDb;
using D2SPlatform.Core;
using SSQDataBase.Entity;

namespace SSQDataBase.DAL
{
    partial class SsqNumbersDAL : AccessDALBase<SsqNumbers, SsqNumbers.Field>
    {
        private static SsqNumbersDAL instance = new SsqNumbersDAL();

        private SsqNumbersDAL()
        {
            Init();
        }

        protected override void Init()
        {
            fieldList = new string[] { "ID", "SSQ_DATE", "NUM_RED_1", "NUM_RED_2", "NUM_RED_3", "NUM_RED_4", "NUM_RED_5", "NUM_RED_6", "NUM_BLUE_7", "QH"};
            propertyList = new string[] { "Id", "SsqDate", "NumRed1", "NumRed2", "NumRed3", "NumRed4", "NumRed5", "NumRed6", "NumBlue7", "Qh" };

            fieldTypeList = new OleDbType[] { OleDbType.VarChar, OleDbType.Date, OleDbType.VarChar, OleDbType.VarChar, OleDbType.VarChar, OleDbType.VarChar, OleDbType.VarChar, OleDbType.VarChar, OleDbType.VarChar, OleDbType.VarChar};
            commonDataTypeList = new Common.DataType[] { Common.DataType.String, Common.DataType.DateTime, Common.DataType.String, Common.DataType.String,  Common.DataType.String, Common.DataType.String, Common.DataType.String, Common.DataType.String, Common.DataType.String, Common.DataType.String };

            tableName = "SSQ_NUMBERS";
        }

        public static SsqNumbersDAL GetInstance()
        {
            return instance;
        }
    }
}
