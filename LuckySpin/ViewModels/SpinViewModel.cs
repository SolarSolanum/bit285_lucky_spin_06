using System;
using System.Linq;
using LuckySpin.Models;
using System.ComponentModel.DataAnnotations;

namespace LuckySpin.ViewModels
{
	public class SpinViewModel
	{
        Random random = new Random();
        private int[] numbers; //a spin array;

        public SpinViewModel() // Constructor picks the numbers when a Spin is created
        {
            numbers = new int[] { random.Next(1,10), random.Next(1,10), random.Next(1,10) };
        }

        //Spin Properties
        public int[] Numbers //Read only - the spin numbers are set in the constructor
        {
            get { return numbers; }
        }

        public bool IsWinning //Read only - true if Player's Luck is one of the numbers
        {
            get { return numbers.Contains(PlayerLuck); }
        }
        
        public int PlayerLuck { get; set; }
        //TODO: Add the remaining Spin ViewModel properties

        public string FirstName { get; set; }
        public string CurrentBalance { get; set; }
    }
}

