using System.ComponentModel.DataAnnotations;
using BankApplication.Data.Validators;
using BankApplication.Data.Models;

namespace BankApplication.Data.DTOs
{
    public class AccountDTO
    {
        [IdNotSend(ErrorMessage = "You cannot input an Id of an account")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to enter a Name")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to enter balance")]
        [StringLength(200)]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "You have to enter a type")]
        public AccountType Type { get; set; }

        [Required(ErrorMessage = "You have to enter if its active")]
        public bool IsActive { get; set; }


        [Required(ErrorMessage = "You have to add an Client")]
        public int ClientId { get; set; }


        public virtual ClientDTO Exam { get; set; }
    }
}
