using AutoMapper;
using Library.Models;
using Library.Models.Entites;
using Library.Models.RequsetDTO;
using Library.Models.ResponseDTO;
using Library.Repository.InterFace;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AuthorRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Authors AddAuthor(AuthorAddRequsetDTO Newauthors, out string ErrorCode)
        {
            ErrorCode = " ";
            if (dbContext.authors.Any(a => a.Name.Equals(Newauthors.Name)))
            {
                ErrorCode = "At002";
                return null;
            }
            //Mapping
            var Currentauthors = mapper.Map<Authors>(Newauthors);
            dbContext.authors.Add(Currentauthors);
            SaveChanges();
            return Currentauthors;
        }
        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
        public PagingResponseDTO<AuthorResponseDTO> GetAll(PagingDTO paging, IUrlHelper Url)
        {
            var AuthorQuary = dbContext.authors.AsQueryable();
            AuthorQuary = dbContext.authors.Where(a => a.IsDeleted == false);

            var ResponseDTO = AuthorQuary.Select(x => mapper.Map<AuthorResponseDTO>(x));
            var PagedResponse = new PagingResponseDTO<AuthorResponseDTO>(ResponseDTO, paging);
            if (PagedResponse.paging.HasNextPage)
                PagedResponse.paging.NextPageUrl = Url.Link("GetAll", new { paging.rowCount, PagingNumber = paging.PagingNumber + 1 });

            if (PagedResponse.paging.HasPrevPage)
                PagedResponse.paging.PrevPageUrl = Url.Link("GetAll", new { paging.rowCount, PagingNumber = paging.PagingNumber - 1 });
            return PagedResponse;
        }

        public AuthorResponseDTO GetAuthorById(int id, out string ErrorCode)
        {
            var CurrentAuthor = dbContext.authors.Where(a => a.AuthorID == id && a.IsDeleted == false).FirstOrDefault();
            ErrorCode = " ";
            if (CurrentAuthor == null)
            {
                ErrorCode = "At001";
                return null;

            }
            //Mapping Domain Model To Response Model

            return mapper.Map<AuthorResponseDTO>(CurrentAuthor);
        }

        public void UpdateAuthor(int id, AuthorUpdateRequsetDTO authorUpdateRequsetDTO, out string ErrorCode)
        {
            ErrorCode = " ";
            var CurrentAuthor = dbContext.authors.Where(a => a.AuthorID == id).SingleOrDefault();
            if (CurrentAuthor == null)
            {
                ErrorCode = "At003";
                return;
            }
            //Mapper
            mapper.Map(authorUpdateRequsetDTO, CurrentAuthor);
            SaveChanges();
        }

        public void DeletedAuthor(int id, out string ErrorCode)
        {
            ErrorCode = " ";
            var CurrentAuthor = dbContext.authors.Where(a => a.AuthorID == id).SingleOrDefault();
            if (CurrentAuthor == null)
            {
                ErrorCode = "At004";
                return;
            }
            CurrentAuthor.IsDeleted = true;
            SaveChanges();
        }
    }
}
