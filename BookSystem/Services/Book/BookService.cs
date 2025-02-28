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

        public static AuthorResponseDTO AuthorToResponseDTO(AuthorModel author) {
            var authorResponseDTO = new AuthorResponseDTO {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                CreatedAt = author.CreatedAt
            };

            return authorResponseDTO;
        }

        public static BookResponseDTO BookToResponseDTO (BookModel book) {
            var bookResponseDTO = new BookResponseDTO {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                Description = book.Description,
                Author = AuthorToResponseDTO(book.Author)
            };

            return bookResponseDTO;
        }

        public async Task<ResponseModel<List<BookResponseDTO>>> GetBooks() {
            var response = new ResponseModel<List<BookResponseDTO>>();
            try {
                var books = await _context.Books
                    .Where(b => b.IsActive)
                    .Include(b => b.Author)
                    .Select(b => BookToResponseDTO(b))
                    .ToListAsync();

                if (!books.Any()) {
                    response.Message = "No books found!";
                    return response;
                }

                response.Data = books;
                response.Message = "All books have been collected";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response; 
            }
        }

        public async Task<ResponseModel<BookResponseDTO>> GetBookById(Guid idBook) {
            var response = new ResponseModel<BookResponseDTO>();

            try {
                var book = await _context.Books
                    .Where(b => b.IsActive)
                    .Include(b => b.Author)
                    .FirstOrDefaultAsync(b => b.Id == idBook);

                if (book == null){
                    response.Message = "No book found!";
                    return response;
                }

                var bookResponse = BookToResponseDTO(book);

                response.Data = bookResponse;
                response.Message = "The book have been collected!";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookResponseDTO>>> GetBooksByAuthorId(Guid idAuthor) {
            var response = new ResponseModel<List<BookResponseDTO>>();

            try {
                var books = await _context.Books
                    .Where(b => b.IsActive && b.Author.Id == idAuthor)
                    .Include(b => b.Author)
                    .Select(b => BookToResponseDTO(b))
                    .ToListAsync();

                if (!books.Any()) {
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

        public async Task<ResponseModel<BookResponseDTO>> CreateBook(BookCreateDTO dto) {
            var response = new ResponseModel<BookResponseDTO>();

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

                var newBook = BookToResponseDTO(book);

                response.Data = newBook;
                response.Message = "Book created";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookResponseDTO>> UpdateBook(Guid id, BookUpdateDTO dto) {
            var response = new ResponseModel<BookResponseDTO>();

            try {
                var book = await _context.Books
                                        .Include(b => b.Author)
                                        .FirstOrDefaultAsync(b => b.IsActive && b.Id == id);
                if (book == null) {
                    response.Message = "No book found!";
                    return response;
                }

                if (string.IsNullOrEmpty(dto.Title) && string.IsNullOrEmpty(dto.Genre)
                    && string.IsNullOrEmpty(dto.Description) && dto.idAuthor == Guid.Empty) {

                    response.Message = "Nothing to update!";
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
                                        .FirstOrDefaultAsync(a => a.Id == dto.idAuthor);
                    if (author == null) {
                        response.Message = "Author not found!";
                        return response;
                    }
                    book.Author = author;
                }

                _context.Update(book);
                await _context.SaveChangesAsync();

                var bookUpdated = BookToResponseDTO(book);

                response.Message = "Book updated!";
                response.Data = bookUpdated;

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookResponseDTO>> DeleteBook(Guid id) {
            var response = new ResponseModel<BookResponseDTO>();

            try {
                var book = await _context.Books
                    .Where(b => b.IsActive)
                    .Include(b => b.Author)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null) {
                    response.Message = "No book found!";
                    return response;
                }

                book.IsActive = false;

                var bookDeleted = BookToResponseDTO(book);

                _context.Update(book);
                await _context.SaveChangesAsync();


                response.Message = "Book deleted";
                response.Data = bookDeleted;

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
