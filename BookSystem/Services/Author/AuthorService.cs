using BookSystem.Data;
using BookSystem.DTO;
using BookSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Services.Author {
    public class AuthorService : IAuthorInterface {
        public readonly AppDbContext _context;

        public AuthorService(AppDbContext context) {
            _context = context;
        }

        public async Task<ResponseModel<AuthorModel>> GetAuthorByBookId(Guid idBook) {
            var response = new ResponseModel<AuthorModel>();
            try {
                var author = await _context.Books
                                        .Where(b => b.Id == idBook)
                                        .Select(b => b.Author)
                                        .FirstOrDefaultAsync();

                response.Data = author;
                if (author == null) {
                    response.Message = "No authors found!";
                    return response;
                }
                response.Message = "The author have been collected.";

                return response;
            } catch (Exception e){
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorModel>> GetAuthorById(Guid idAuthor) {
            var response = new ResponseModel<AuthorModel>();
            try {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == idAuthor);
                
                response.Data = author;
                if (author == null) {
                    response.Message = "No authors found!";
                    return response;
                }
                response.Message = "The author have been collected.";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> GetAuthors() {
            var response = new ResponseModel<List<AuthorModel>>();
            try {
                var authors = await _context.Authors.ToListAsync();
                response.Data = authors;
                response.Message = "All authors have been collected.";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorModel>> CreateAuthor(AuthorCreateDTO dto) {
            var response = new ResponseModel<AuthorModel>();
            try {
                var author = new AuthorModel() {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
                //var author = new AuthorModel(dto);
                _context.Add(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
                response.Message = "Author created successfully!";
                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorModel>> UpdateAuthor(Guid id, AuthorUpdateDTO dto) {
            var response = new ResponseModel<AuthorModel>();
            try {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

                if (author == null) {
                    response.Message = "No author found!";
                    return response;
                }

                if (dto.FirstName == null || dto.FirstName == "")
                    author.LastName = dto.LastName;

                if (dto.LastName == null || dto.LastName == "")
                    author.FirstName = dto.FirstName;

                _context.Update(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
                response.Message = "Author updated successfully!";
                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public Task<ResponseModel<AuthorModel>> DeleteAuthor(Guid id) {
            throw new NotImplementedException();
        }
    }
}
