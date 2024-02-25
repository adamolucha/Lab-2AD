using LibApp.Data;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using LibApp.Dtos;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    
        public class BooksController : ControllerBase
        {
            public BooksController(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            // GET: api/<BooksController>
            [HttpGet]
            public IActionResult GetBooks()
            {
                return Ok(_context.Books
                            .Include(b => b.Genre)
                                                          .ToList()
                                                                             .Select(_mapper.Map<Book, BookDto>));
            }

            // GET api/<BooksController>/5
            [HttpGet("{id}")]
            public IActionResult GetBook(int id)
            {
                var book = _context.Books.SingleOrDefault(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<BookDto>(book));
            }

            // POST api/<BooksController>
            [HttpPost]
            public IActionResult CreateBook(BookDto bookDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _context.Books.Add(_mapper.Map<Book>(bookDto));
                _context.SaveChanges();
                return CreatedAtRoute(nameof(GetBook), new { id = bookDto.Id }, bookDto);
            }

            // PUT api/<BooksController>/5
            [HttpPut("{id}")]
            public IActionResult UpdateBook(int id, BookDto bookDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);
                if (bookInDb == null)
                {
                    return NotFound();
                }

                _mapper.Map(bookDto, bookInDb);
                _context.SaveChanges();
                return NoContent();
            }

            // DELETE api/<BooksController>/5
            [HttpDelete("{id}")]
            public IActionResult DeleteBook(int id)
            {
                var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);
                if (bookInDb == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(bookInDb);
                _context.SaveChanges();
                return NoContent();
            }
        private ApplicationDbContext _context;
        private IMapper _mapper;
    }
    }
