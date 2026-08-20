using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_iti.ViewModels
{
    public class AccountViewModels
    {
        public string Fname{ get; set; }
        public string Lname { get; set; }
        public String FullName => $"{Fname} {Lname}";
        public string EmailAddress{ get; set; }

        public bool IsActive { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }


}
