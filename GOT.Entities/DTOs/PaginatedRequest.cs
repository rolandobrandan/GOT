using System;
using System.Collections.Generic;
using System.Text;

namespace GOT.Entities.DTOs
{
    public class PaginatedRequest
    {
        public int PageNumber {  get; set; } = 1;

        public int PageSize { get; set; } = 10;

    }
}
