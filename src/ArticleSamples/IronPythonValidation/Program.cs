using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPythonValidation.Model;
using IronPythonValidation.Validation;

namespace IronPythonValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string response = "";
                while (response.ToLower() != "exit")
                {                    

                    User user = new User();

                    Console.Write("Please enter a first name: ");
                    user.FirstName = Console.ReadLine();

                    Console.Write("Please enter a last name: ");
                    user.LastName = Console.ReadLine();

                    Console.Write("Please enter a password: ");
                    user.Password = Console.ReadLine();

                    Console.Write("Please enter a birthday: ");
                    user.Birthday = DateTime.Parse(Console.ReadLine());
       
                    user.Runner = new XmlValidationRunner("ValidationRules.xml");

                    if (!user.IsValid())
                    {
                        Console.WriteLine("Errors:");
                        foreach (string propName in user.PropertiesWithErrors.Keys)
                        {
                            Console.WriteLine("\t{0} is invalid ({1}).", propName, user.PropertiesWithErrors[propName]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("User is valid.");
                    }

                    Console.Write("\r\nPress any key to continue, or type quit to exit: ");
                    response = Console.ReadLine().ToLower();
                    Console.WriteLine();
                }             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }        
    }
}
