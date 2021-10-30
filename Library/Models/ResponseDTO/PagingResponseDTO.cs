using Library.Helper;
using Library.Models.RequsetDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ResponseDTO
{
    public class PagingResponseDTO<T>
    {
        public PagingResponseDTO(IQueryable<T> Quary, PagingDTO ClientPaging)
        {
            paging = new pagingDetailes();
            paging.TotalRow = Quary.Count();
            paging.TotalPaging = (int)Math.Ceiling((double)paging.TotalRow / ClientPaging.rowCount);
            paging.CurPage = ClientPaging.PagingNumber;
            paging.HasNextPage = paging.CurPage < paging.TotalPaging;
            paging.HasPrevPage = paging.CurPage > 1;

            Data = Quary.Skip((ClientPaging.PagingNumber - 1) * ClientPaging.rowCount).Take(ClientPaging.rowCount).ToList();
        }

        public pagingDetailes paging { get; set; }
        public List<T> Data { get; set; }
    }
}
