using AutoMapper;
using Library.Helper;
using Library.Models;
using Library.Models.Entites;
using Library.Models.RequsetDTO;
using Library.Repository.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/Author/{AuthorId}/Book")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly ErrorClass errorClass;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dBContext;
        private readonly IBookRepository bookRepository;

        public BooksController(ErrorClass errorClass, IMapper mapper, ApplicationDbContext dbContext, IBookRepository bookRepository)
        {
            this.errorClass = errorClass;
            this.mapper = mapper;
            this.dBContext = dbContext;
            this.bookRepository = bookRepository;
        }
        [HttpGet]
        public IActionResult AllAuthorBook(int AuthorId)
        {
            return Ok(bookRepository.AllAuthorBook(AuthorId));
        }
        [HttpGet("{BookId}")]
        public IActionResult GetBookById(int AuthorId, int BookId)
        {
            string ErrorCode = " ";
            var CurBook = bookRepository.GetBookById(AuthorId, BookId, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            return Ok(CurBook);
        }
        [HttpPost("{AuthorId}")]
        public IActionResult AddBook(int AuthorId, Book book)
        {
            string ErrorCode = " ";
            bookRepository.AddBook(AuthorId, book, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            return CreatedAtAction(nameof(GetBookById), new { AuthorId = AuthorId, BookId = book.BookID }, book);

        }
        [HttpDelete("{BookId}")]
        public IActionResult DeleteBook(int AuthorId, Book book)
        {
            string ErrorCode = " ";
            bookRepository.DeleteBook(AuthorId, book, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            return NoContent();
        }
    }
}
