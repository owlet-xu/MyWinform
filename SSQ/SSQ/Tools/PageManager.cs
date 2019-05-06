using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSQ.Tools
{
    public class PageManager
    {
        private int page = 0;//当前页
        private int size = 0;//分页尺寸
        private int recordCount = 0;//总条数
        private int pageCount = 0;//总页数
        private int pageBefore = 0;//上一页
        private int pageNext = 0;//下一页

        public PageManager(int page, int size, int pageCount, int recordCount)
        {
            this.page = page;
            this.size = size;
            this.pageCount = pageCount;
            this.recordCount = recordCount;
            this.pageBefore = page - 1;
            this.pageNext = page + 1;
        }


        public Boolean NextPage()
        {
            if (pageCount <= 0) return false;
            if (page == pageCount) return false;
            if (recordCount < 0 || pageCount < 0) return false;
            if (pageNext > pageCount) return false;
            page = pageNext;
            ++pageNext;
            ++pageBefore;
            return true;
        }
        public Boolean BeforePage()
        {
            if (pageCount <= 0) return false;
            if (page == 1) return false;
            if (recordCount < 0 || pageCount < 0) return false;
            if (pageBefore <= 0) return false;
            page = pageBefore;
            --pageNext;
            --pageBefore;
            return true;
        }
        public Boolean LastPage()
        {
            if (pageCount <= 0) return false;
            if (page == pageCount) return false;
            page = pageCount;
            pageNext = pageCount + 1;
            pageBefore = pageCount - 1;
            return true;
        }
        public Boolean FirstPage()
        {
            if (pageCount <= 0) return false;
            if (page == 1) return false;
            page = 1;
            pageNext = 2;
            pageBefore = 0;
            return true;
        }

        public int GetPage()
        {
            return page;
        }
        public int GetSize()
        {
            return size;
        }
        public int GetPageCount()
        {
            return pageCount;
        }
        public int GetRecordCount()
        {
            return recordCount;
        }
        public int GetPageBefore()
        {
            return pageBefore;
        }
        public int GetPageNext()
        {
            return pageNext;
        }

        public int GetCount()
        {
            int count = 0;
            if (page == pageCount)
            {
                count = recordCount;
            }
            count = page * size;
            return count;
        }

        public int GetPosition()
        {
            int count = 0;
            if (page > pageCount)
            {
                count = 0;
            }
            count = (page - 1) * size;
            return count;
        }

        public void SetPage(int page)
        {
            if (page > pageCount || page <= 0) return;
            this.page = page;
            this.pageBefore = page - 1;
            this.pageNext = page + 1;
        }

        public void SetPageCountReccordCount(int pageCount, int recordCount)
        {
            this.pageCount = pageCount;
            this.recordCount = recordCount;
        }
    }
}
