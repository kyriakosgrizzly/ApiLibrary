using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PagingParameters
    {
        private const int MAX_PAGE_SIZE = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value < MAX_PAGE_SIZE) ? value : MAX_PAGE_SIZE; }
        }
    }
}
