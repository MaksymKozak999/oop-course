using System;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    public static void ValidateName(string value,string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
            throw new ArgumentException("Ім'я не може бути порожнім або довшим за 100 символів.", nameof(fieldName));
    }
    public static void ValidatePhone(string phone)
    {
        if (phone == null || phone.Length != 10)
            throw new ArgumentException("Телефон має містити рівно 10 цифр.", nameof(phone));
        foreach (char c in phone)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException("Телефон має містити лише цифри.", nameof(phone));
        }
    }
    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value.Date > DateTime.Today || value.Year < 1900)
            throw new ArgumentOutOfRangeException(nameof(fieldName), "Дата народження має бути між 1900 роком і сьогоднішнім днем.");
    }
    public static void ValidatePositive(int value, string fieldName)
    {
        if(value < 0)
            throw new ArgumentOutOfRangeException(nameof(fieldName), "Тривалість не може бути відємною.");
    }
}