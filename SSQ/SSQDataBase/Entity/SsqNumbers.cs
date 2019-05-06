using System;
using System.Collections.Generic;
using System.Text;

using D2SPlatform.Core;

namespace SSQDataBase.Entity
{
    [Serializable]
    public partial class SsqNumbers : EntityBase
    {
        public enum Field { Id = 0, SsqDate = 1, NumRed1 = 2, NumRed2 = 3, NumRed3 = 4, NumRed4 = 5, NumRed5 = 6, NumRed6 = 7, NumBlue7 = 8, Qh = 9  }

		public SsqNumbers() { }

        protected string id;
        protected DateTime ssqDate;
        protected string numRed1;
        protected string numRed2;
        protected string numRed3;
        protected string numRed4;
        protected string numRed5;
        protected string numRed6;
        protected string numBlue7;
        protected string qh;
       
        public string Id
        {
            get { return id; }
            set { id = value; }
        }


        public DateTime SsqDate
        {
            get { return ssqDate; }
            set { ssqDate = value; }
        }

        public string NumRed1
        {
            get { return numRed1; }
            set { numRed1 = value; }
        }

        public string NumRed2
        {
            get { return numRed2; }
            set { numRed2 = value; }
        }

        public string NumRed3
        {
            get { return numRed3; }
            set { numRed3 = value; }
        }

        public string NumRed4
        {
            get { return numRed4; }
            set { numRed4 = value; }
        }

        public string NumRed5
        {
            get { return numRed5; }
            set { numRed5 = value; }
        }

        public string NumRed6
        {
            get { return numRed6; }
            set { numRed6 = value; }
        }

        public string NumBlue7
        {
            get { return numBlue7; }
            set { numBlue7 = value; }
        }

        public string Qh
        {
            get { return qh; }
            set { qh = value; }
        }

    }
}
