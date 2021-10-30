using Library.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository.InterFace
{
    public interface IBookRepository
    {
        List<Book> AllAuthorBook(int AuthorId);
        Book GetBookById(int AuthorId, int BookId,out string ErrorCode);
        Book AddBook(int AuthorId, Book book, out string ErrorCode);
        void DeleteBook(int AuthorId, Book book, out string ErrorCode);
    }
}
