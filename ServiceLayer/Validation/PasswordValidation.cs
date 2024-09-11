using System.Text.RegularExpressions;

namespace ServiceLayer.Validation;
public static class PasswordValidation
{


    public static void ValidateRegistrationPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {

            throw new ArgumentNullException(nameof(password), "password cant be empty");
        }

        if (password.Length < 5)
        {
            throw new InvalidPasswordException("Password should be at least 5 characters long");
        }
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            throw new InvalidPasswordException("Password should consist of latin letters");
        }
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            throw new InvalidPasswordException("Password should contain at least 1 uppercase letter");
        }
        if (!Regex.IsMatch(password, @"\d"))
        {
            throw new InvalidPasswordException("Password should contain at least 1 digit");
        }
        if (!Regex.IsMatch(password, @"[^\w]"))
        {
            throw new InvalidPasswordException("password should contain at least 1 symbol");
        }
    }





}

