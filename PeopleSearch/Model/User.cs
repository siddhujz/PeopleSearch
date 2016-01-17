using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Model
{
    public class User
    {
        public User()
        {

        }

        public int UserId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public byte[] UserPhoto { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Interests { get; set; }

        [MaxLength(50)]
        public string AddressLine1 { get; set; }

        [MaxLength(50)]
        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(20)]
        public string State { get; set; }

        [MaxLength(20)]
        public string Zipcode { get; set; }
    }
}
