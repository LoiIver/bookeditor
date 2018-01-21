﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookEditor.Data.Repositories;

namespace BookEditor.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBookRepository _bookRepository;

		public HomeController(IBookRepository repository)
		{
			_bookRepository = repository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}