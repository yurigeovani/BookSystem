using BookSystem.Data;
using BookSystem.DTO;
using BookSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Services.Book {
    public class BookService : IBookInterface {
        public readonly AppDbContext _context;

        public BookService(AppDbContext context) {
            _context = context;
        }

        public async Task<ResponseModel<List<BookModel>>> GetBooks() {
            var response = new ResponseModel<List<BookModel>>();
            try {
                var books = await _context.Books
                    .Where(b => b.IsActive)
                    .ToListAsync();
                
                response.Data = books;
                response.Message = "All books have been collected";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response; 
            }
        }

        public async Task<ResponseModel<BookModel>> GetBookById(Guid idBook) {
            var response = new ResponseModel<BookModel>();
            try {
                var book = await _context.Books
                    .Where(b => b.IsActive)
                    .FirstOrDefaultAsync(b => b.Id == idBook);

                response.Data = book;

                if (book == null){
                    response.Message = "No book found!";
                    return response;
                }

                response.Message = "The book have been collected!";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> GetBooksByAuthorId(Guid idAuthor) {
            var response = new ResponseModel<List<BookModel>>();

            try {
                //TOFIX
                var books = await _context.Books
                    .Where(b => b.IsActive)
                    .Where(b => b.Author.Id == idAuthor)
                    .ToListAsync();

                if (books == null) {
                    response.Message = "No books found!";
                    return response;
                }

                response.Message = "Books found";
                response.Data = books;

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookModel>> CreateBook(BookCreateDTO dto) {
            var response = new ResponseModel<BookModel>();

            try {
                var author = await _context.Authors
                                .Where(a => a.IsActive)
                                .FirstOrDefaultAsync(a => a.Id == dto.idAuthor);
                
                if (author == null) {
                    response.Message = "Author not found!";
                    return response;
                }

                var book = new BookModel() {
                    Title = dto.Title,
                    Genre = dto.Genre,
                    Description = dto.Description,
                    Author = author
                };

                _context.Add(book);
                await _context.SaveChangesAsync();

                response.Data = book;
                response.Message = "Book created";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookModel>> UpdateBook(Guid id, BookUpdateDTO dto) {
            var response = new ResponseModel<BookModel>();

            try {
                var book = await _context.Books
                                        .Where(b => b.IsActive)
                                        .Where(b => b.Id == id)
                                        .FirstOrDefaultAsync();
                if (book == null) {
                    response.Message = "No book found!";
                    return response;
                }

                if (!string.IsNullOrEmpty(dto.Title))
                    book.Title = dto.Title;
                if (!string.IsNullOrEmpty(dto.Genre))
                    book.Genre = dto.Genre;
                if (!string.IsNullOrEmpty(dto.Description))
                    book.Description = dto.Description;
                if (dto.idAuthor != Guid.Empty) {
                    var author = await _context.Authors
                                        .Where(a => a.Id == dto.idAuthor)
                                        .FirstOrDefaultAsync();
                    if (author == null) {
                        response.Message = "Author not found!";
                        return response;
                    }
                    book.Author = author;
                }

                _context.Update(book);
                await _context.SaveChangesAsync();

                response.Message = "Book updated!";
                response.Data = book;

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookModel>> DeleteBook(Guid id) {
            var response = new ResponseModel<BookModel>();

            try {
                var book = await _context.Books
                    .Where(b => b.IsActive)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null) {
                    response.Message = "No book found!";
                    return response;
                }

                book.IsActive = false;

                _context.Update(book);
                await _context.SaveChangesAsync();

                response.Message = "Book deleted";
                response.Data = book;

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
