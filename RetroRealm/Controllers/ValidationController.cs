﻿using Microsoft.AspNetCore.Mvc;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class ValidationController : Controller
    {
        private readonly GameModelContext _gameContext;

        public ValidationController(GameModelContext gameContext)
        {
            _gameContext = gameContext;
        }

        public JsonResult CustomerEmailExists(string Email, int CustomerModelId)
        {
            CustomerModel? custWithEmail = _gameContext.Customers.FirstOrDefault(c => c.Email == Email);

            if(custWithEmail.CustomerModelId != CustomerModelId)
            {
                return Json("Email already exists");
            }

            return Json(true);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
