using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class RegisterNewUserViewModel
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }



        [MaxLength(100, ErrorMessage = "The field {0} can't contain more than {1} characters")]
        public string Address { get; set; }



        [MaxLength(20, ErrorMessage = "The field {0} can't contain more than {1} characters")]
        public string PhoneNumber { get; set; }



        [Display(Name = "City"), Range(1, int.MaxValue, ErrorMessage = "Yous must select a city")]
        public int CityId { get; set; }


        public IEnumerable<SelectListItem> Cities { get; set; }



        [Display(Name = "Country"), Range(1, int.MaxValue, ErrorMessage = "Yous must select a country")]
        public int CountryId { get; set; }


        public IEnumerable<SelectListItem> Countries { get; set; } //este SelectListItem é para as comboBox



        [Required, DataType(DataType.EmailAddress)]
        public string Username { get; set; }



        [Required, MinLength(6)]
        public string Password { get; set; }



        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
