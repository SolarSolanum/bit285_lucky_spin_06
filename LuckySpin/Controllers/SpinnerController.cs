using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;
using LuckySpin.Services;
using System.Globalization;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private RepositoryService repoService;

        /***
         *  Constructor -  with RepositoryService DIJ
         **/
        public SpinnerController(RepositoryService repoService)
        {
            this.repoService = repoService;
        }

        /***
         * Index Action
         **/
        [HttpGet]
        public IActionResult Index()
        {
                return View(); //Sends the empty Index form
        }
        
        [HttpPost]
        public IActionResult Index(Player player) //: Update Index() to receive form data as IndexViewModel
        {
            if (!ModelState.IsValid) { return View(); } //Check for missing data

            //: Complete adding Player data to store in the repoService
            repoService.Player = new Player
            {
                FirstName = player.FirstName,
                LuckNumber = player.LuckNumber,
                Balance = player.Balance,
                PlayerId = player.PlayerId
            };

            return RedirectToAction("Spin");
        }

        /***
         * Spin Action
         **/

        public IActionResult Spin() //Start a Spin WITHOUT data
        {
            //CHARGE 
            // : Load Player balance from the repoService


            //: Charge $0.50 to spin
            repoService.Player.Balance -= 0.50m;
            decimal balance = repoService.Player.Balance;


            //SPIN
            //: Complete adding data to a new SpinViewModel to gather items for the View
            SpinViewModel spinVM = new SpinViewModel
            {
                CurrentBalance = string.Format(new CultureInfo("en-SG", false), "{0:C2}", balance),
                FirstName = repoService.Player.FirstName,
                PlayerLuck = repoService.Player.LuckNumber
            };

            //GAMEPLAY
            //: Check the Balance to see if the game is over
            if (balance < 0.50m)
            {
                return RedirectToAction("LuckList");
            }
            //: Pay $1.00 if Winning
            if (spinVM.IsWinning)
            {
                repoService.Player.Balance += 1.00m;
                spinVM.CurrentBalance = string.Format(new CultureInfo("en-SG", false), "{0:C2}", repoService.Player.Balance);
            }


            //UPDATE DATA STORE
            //TODO: Save balance to repoService
            Spin sp = new Spin
            {
                player = repoService.Player,
                isWinning = spinVM.IsWinning
            };
            repoService.AddSpin(sp);

            //TODO: Use the repoService to add a spin to the repository


            return View("Spin", spinVM);
        }

        //TODO: BONUS:  the LuckList
        /***
         * LuckList Action
         **/
        [HttpGet]
        public IActionResult LuckList()
        {
            return new ContentResult { Content = "<h1>Luck's Run Out</h1>", ContentType = "text/html" };
        }

    }
}

