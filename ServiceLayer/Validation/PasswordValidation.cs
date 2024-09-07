using System.Text.RegularExpressions;

namespace ServiceLayer.Validation;
public static class PasswordValidation
{


    public static void ValidateRegistrationPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {

            throw new ArgumentNullException(nameof(password));
        }

        if (password.Length < 5)
        {
            throw new Exception();
        }
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            throw new Exception();
        }
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            throw new Exception();
        }
        if (!Regex.IsMatch(password, @"\d"))
        {
            throw new Exception();
        }
        if (!Regex.IsMatch(password, @"[^\w]"))
        {
            throw new Exception();
        }
    }





}

