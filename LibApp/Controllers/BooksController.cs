﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using LibApp.Data;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _context.Genres.ToList()
            };

            return View("BookForm", viewModel);
        }
        public ActionResult DateAction(Book model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("SuccessAction");
            }
            return View(model);
        }

        public IActionResult Index()
        {

            var books = _context.Books
                .Include(b => b.Genre)
                .ToList();

            return View(books);
        }


        public ActionResult Details(int id)
        {
            var book = _context.Books
                .Include(b => b.Genre)
                .SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return Content("Book not found");
            }

            return View(book);
        }

        public IActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View("BookForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _context.Books.Add(book);
            }
            else
            {
                var bookInDb = _context.Books.SingleOrDefault(b => b.Id == book.Id);
                bookInDb.Title = book.Title;
                bookInDb.GenreId = book.GenreId;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.NumberInStock = book.NumberInStock;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }
    }
}