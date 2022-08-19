using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Dapper.Models
{
    public class EmpModel
    {
        [Display(Name = "Id")]
        public int Empid { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string Address { get; set; }
        public int cityId { get; set; }
        
        public string cityName { get; set; }
        public string Gender { get; set; }
        public bool CSharp { get; set; }
        public bool Java { get; set; }
        public bool Python { get; set; }
        public string Search { get; set; }
    }

    public class CityModel
    {
        public int cityId { get; set; }
        public string cityName { get; set; }
    }
}