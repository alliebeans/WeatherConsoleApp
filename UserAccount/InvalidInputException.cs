using System;

namespace WeatherApp.UserAccount
{
    public enum InvalidInputException
    {
        NotAsciiLetterOrDigit,
        ExceedsMaxLength
    }
}