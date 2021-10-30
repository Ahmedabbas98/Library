using Library.Models.Entites;
using Library.Models.RequsetDTO;
using Library.Models.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository.InterFace
{
    public interface IAuthorRepository
    {
        PagingResponseDTO<AuthorResponseDTO> GetAll(PagingDTO paging, IUrlHelper Url);
        AuthorResponseDTO GetAuthorById(int id, out string ErrorCode);
        Authors AddAuthor(AuthorAddRequsetDTO Newauthors, out string ErrorCode);
        void UpdateAuthor(int id, AuthorUpdateRequsetDTO authorUpdateRequsetDTO, out string ErrorCode);
        void DeletedAuthor(int id, out string ErrorCode);
    }
}
