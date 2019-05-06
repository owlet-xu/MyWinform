using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSQDataBase.DAL;
using SSQDataBase.Entity;
using D2SPlatform.Core;

namespace SSQDataBase.COM
{
    public class SsqNumbersManager
    {
        private static SsqNumbersManager instance = new SsqNumbersManager();
        private SsqNumbersManager() { }
        public static SsqNumbersManager GetInstance()
        {
            return instance;
        }

        public SsqNumbers GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            List<SsqNumbers> list = SsqNumbersDAL.GetInstance().Select(null, new SsqNumbers.Field[] { SsqNumbers.Field.Id }, new object[] { id });
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        //通过期号获取
        public SsqNumbers GetByQh(string qh)
        {
            if (string.IsNullOrEmpty(qh)) return null;

            List<SsqNumbers> list = SsqNumbersDAL.GetInstance().Select(null, new SsqNumbers.Field[] { SsqNumbers.Field.Qh }, new object[] { qh });
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public void Add(SsqNumbers item)
        {
            if (string.IsNullOrEmpty(item.Id))
            {
                item.Id = Common.GetId();
            }
            SsqNumbersDAL.GetInstance().Insert(item);
        }

        public void Update(SsqNumbers item)
        {
            SsqNumbersDAL.GetInstance().Update(item, new SsqNumbers.Field[] { SsqNumbers.Field.NumBlue7, SsqNumbers.Field .NumRed1,
            SsqNumbers.Field.NumRed2,SsqNumbers.Field.NumRed3,SsqNumbers.Field.NumRed4,SsqNumbers.Field.NumRed5,SsqNumbers.Field.NumRed6,
            SsqNumbers.Field.Qh,SsqNumbers.Field.SsqDate}, new SsqNumbers.Field[] { SsqNumbers.Field.Id });
        }

        public void Save(List<SsqNumbers> items)
        {
            foreach (SsqNumbers item in items)
            {
                if (string.IsNullOrEmpty(item.Qh))
                {
                    continue;
                }
                if(GetByQh(item.Qh) != null)
                {
                    Update(item);
                }
                else
                {
                    Add(item);
                }
            }
        }

        public List<SsqNumbers> GetList(string start,string end)
        {
            List<QueryCondition> qcList = new List<QueryCondition>();
            QueryCondition qc;

            if (!string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
            {
                qc = new QueryCondition();
                qc.Type = QueryCondition.ConditionType.Range;
                qc.FieldIndex = (int)SsqNumbers.Field.SsqDate;
                if (!string.IsNullOrEmpty(start))
                {
                    qc.LowerEqual = true;
                    qc.LowerBound = Common.ToDateTime(start);
                }
                if (!string.IsNullOrEmpty(end))
                {
                    qc.UpperEqual = true;
                    qc.UpperBound = Common.ToDateTime(end);
                }
                qcList.Add(qc);
            }
            return SsqNumbersDAL.GetInstance().Select(null, qcList, new SsqNumbers.Field[] { SsqNumbers .Field.SsqDate},false);
        }


        //public List<SsqNumbers> FindList(int page, int size, out int pageCount, out int recordCount)
        //{
        //    List<QueryCondition> queryList = new List<QueryCondition>();

        //    return SsqNumbersDAL.GetInstance().Select(page, size, out pageCount, out recordCount, null, queryList, true, new SsqNumbers.Field[] { SsqNumbers.Field.SsqDate }, false);
        //}

    }
}
