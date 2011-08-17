using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPythonValidation.Validation;

namespace IronPythonValidation.Model
{
    public class User : ValidationBase
    {

        public int Id { get; set; }

        [Validation("first_name_length")]        
        public string FirstName { get; set; }

        [Validation("last_name_length")]
        public string LastName { get; set; }

        [Validation("password_contains_digits")]
        public string Password { get; set; }

        [Validation("is_valid_birthdate")]
        public DateTime Birthday { get; set; }

    }
}
