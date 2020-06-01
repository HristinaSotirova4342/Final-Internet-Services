using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BankApplication.Data.Validators;
using BankApplication.Data.Models;

namespace BankApplication.Data.DTOs
{
    public class ClientDTO
    {
        [IdNotSend(ErrorMessage = "You cannot input an Id of a client")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to enter a Name")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to enter a Phone number")]
        [StringLength(400)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "You have to enter email")]
        [StringLength(200)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "You have to enter a type")]
        public ClientType Type { get; set; }
        

        [Required(ErrorMessage = "You have to add an Address.")]
        public int AddressId { get; set; }
        
        public virtual AddressDTO Address { get; set; }
        public virtual IEnumerable<AccountDTO> Accounts { get; set; }

    }
}
