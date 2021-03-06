﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PsHelloAzure.Models;
using PsHelloAzure.Services;

namespace PsHelloAzure.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseStore _courseStore;

        public CoursesController(CourseStore courseStore)
        {
            _courseStore = courseStore;
        }

        public IActionResult Index()
        {
            var model = _courseStore.GetAllCourses();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert()
        {
            var data = new SampleData().GetCourses();
            await _courseStore.InsertCourses(data);
            return RedirectToAction(nameof(Index));
        }
    }
}