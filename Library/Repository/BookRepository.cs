using AutoMapper;
using Library.Models;
using Library.Models.Entites;
using Library.Repository.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Book AddBook(int AuthorId, Book book, out string ErrorCode)
        {
            ErrorCode = "";
            if (!dbContext.authors.Any(x => x.AuthorID == AuthorId))
            {
                ErrorCode = "B002";
                return null;
            }
            if (AuthorId != book.AuthorID)
            {
                ErrorCode = "B003";
                return null;
            }
            if (!dbContext.books.Any(x => x.AuthorID == AuthorId && x.BookID == book.BookID))
            {
                ErrorCode = "B004";
                return null;
            }
            dbContext.books.Add(book);
            dbContext.SaveChanges();
            return new Book();
        }

        public List<Book> AllAuthorBook(int AuthorId)
        {
            return new List<Book>(dbContext.books.Where(x => x.AuthorID == AuthorId).ToList());
        }

        public void DeleteBook(int AuthorId, Book book, out string ErrorCode)
        {
            ErrorCode = "";
            if (!dbContext.authors.Any(x => x.AuthorID == AuthorId))
            {
                ErrorCode = "B002";
                return;
            }
            var CurBook = dbContext.books.Where(x => x.AuthorID == AuthorId && x.BookID == book.BookID).SingleOrDefault();
            if (CurBook == null)
            {
                ErrorCode = "B003";
                return;
            }
            dbContext.books.Remove(CurBook);
            dbContext.SaveChanges();
        }

        public Book GetBookById(int AuthorId, int BookId, out string ErrorCode)
        {
            ErrorCode = "";
            var CurBook = dbContext.books.Where(x => x.BookID == BookId && x.AuthorID == AuthorId).SingleOrDefault();
            if (CurBook == null)
            {
                ErrorCode = "B001";
                return null;
            }
            return CurBook;
        }

    }
}
