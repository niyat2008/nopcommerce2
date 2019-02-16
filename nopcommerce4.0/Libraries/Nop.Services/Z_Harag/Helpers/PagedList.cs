using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Services.Z_Harag.Helpers
{
    public class PagedList<T> where T : BaseEntity
    {
        public PagedList(IQueryable<T> source, int pageNumber, int pageSize,bool orederByDes)
        {
            if (orederByDes)
            {
                this.TotalItems = source.Count();
                this.PageNumber = pageNumber;
                this.PageSize = pageSize;
                this.List = source.Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .ToList();
            }
            else
            {
                this.TotalItems = source.Count();
                this.PageNumber = pageNumber;
                this.PageSize = pageSize;
                this.List = source.OrderBy(s => s.Id)
                                .Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .ToList();
            }
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public List<T> List { get; }
        public int TotalPages =>
              (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
        public bool HasPreviousPage => this.PageNumber > 1;
        public bool HasNextPage => this.PageNumber < this.TotalPages;
        public int NextPageNumber =>
               this.HasNextPage ? this.PageNumber + 1 : this.TotalPages;
        public int PreviousPageNumber =>
               this.HasPreviousPage ? this.PageNumber - 1 : 1;

        public PagingHeader GetHeader()
        {
            return new PagingHeader(
                 this.TotalItems, this.PageNumber,
                 this.PageSize, this.TotalPages);
        }
    }
}
