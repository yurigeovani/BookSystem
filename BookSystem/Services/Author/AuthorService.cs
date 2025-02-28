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

        public static AuthorResponseDTO AuthorToResponseDTO(AuthorModel author) {
            var authorResponseDTO = new AuthorResponseDTO {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                CreatedAt = author.CreatedAt
            };

            return authorResponseDTO;
        }

        public async Task<ResponseModel<AuthorResponseDTO>> GetAuthorByBookId(Guid idBook) {
            var response = new ResponseModel<AuthorResponseDTO>();
            try {
                var author = await _context.Books
                                        .Where(b => b.Id == idBook)
                                        .Select(b => b.Author)
                                        .FirstOrDefaultAsync(a => a.IsActive);

                if (author == null) {
                    response.Message = "No authors found!";
                    return response;
                }

                var authorResponse = AuthorToResponseDTO(author);

                response.Data = authorResponse;
                response.Message = "The author have been collected.";

                return response;
            } catch (Exception e){
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorResponseDTO>> GetAuthorById(Guid idAuthor) {
            var response = new ResponseModel<AuthorResponseDTO>();
            try {
                var author = await _context.Authors
                    .Where(a => a.IsActive)
                    .FirstOrDefaultAsync(a => a.Id == idAuthor);
                
                if (author == null) {
                    response.Message = "No authors found!";
                    return response;
                }

                var authorResponse = AuthorToResponseDTO(author);

                response.Data = authorResponse;
                response.Message = "The author have been collected.";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorResponseDTO>>> GetAuthors() {
            var response = new ResponseModel<List<AuthorResponseDTO>>();
            try {
                var authors = await _context.Authors
                    .Where(a => a.IsActive)
                    .Select(a => AuthorToResponseDTO(a))
                    .ToListAsync();

                response.Data = authors;
                response.Message = "All authors have been collected.";

                return response;
            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorResponseDTO>> CreateAuthor(AuthorCreateDTO dto) {
            var response = new ResponseModel<AuthorResponseDTO>();

            try {
                var author = new AuthorModel() {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

                _context.Add(author);
                await _context.SaveChangesAsync();

                var authorResponse = AuthorToResponseDTO(author);

                response.Data = authorResponse;
                    //await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
                response.Message = "Author created successfully!";
                return response;

            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorResponseDTO>> UpdateAuthor(Guid id, AuthorUpdateDTO dto) {
            var response = new ResponseModel<AuthorResponseDTO>();
            try {
                var author = await _context.Authors
                    .Where(a => a.IsActive)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (author == null) {
                    response.Message = "No author found!";
                    return response;
                }

                if (string.IsNullOrEmpty(dto.FirstName) && string.IsNullOrEmpty(dto.LastName)) {
                    response.Message = "Nothing to update!";
                    return response;
                }

                if (!string.IsNullOrEmpty(dto.FirstName))
                    author.FirstName = dto.FirstName;

                if(!string.IsNullOrEmpty(dto.LastName))
                    author.LastName = dto.LastName;

                _context.Update(author);
                await _context.SaveChangesAsync();

                var authorResponse = AuthorToResponseDTO(author);

                response.Data = authorResponse;
                    //await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
                response.Message = "Author updated successfully!";
                return response;

            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorResponseDTO>> DeleteAuthor(Guid id) {
            var response = new ResponseModel<AuthorResponseDTO>();

            try {
                var author = await _context.Authors
                    .Where(a => a.IsActive)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (author == null) {
                    response.Message = "No author found!";
                    return response;
                }

                var authorResponse = AuthorToResponseDTO(author);

                author.IsActive = false;
                _context.Update(author);
                await _context.SaveChangesAsync();

                response.Data = authorResponse;
                response.Message = "Author deleted!";
                return response;

            } catch (Exception e) {
                response.Message = e.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
