﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.Models.Mapper;
using Practice.Services;

namespace Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PairController : Controller
    {
        private readonly IDBService dbService;

        public PairController(IDBService dbService)
        {
            this.dbService = dbService;
        }

        public IActionResult Index()
        {
            return View(dbService.getPairWithIncludesToList());
        }

        [Route("/Pair/{id}")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pair = dbService.searchPairByID(id);

            if (pair != null)
            {
                dbService.removePairFromDB(pair);
                dbService.saveChengesInDB();
            }

            return View("Index", dbService.getPairWithIncludesToList());
        }
    }
}
