using System.Text.RegularExpressions;

public static class Validator
{
    public static bool IsValidDescription(string description)
    {
        return Regex.IsMatch(description, @"^[a-zA-Z0-9\s]+");
    }
    public static bool IsValidCategory(string category)
    {
        if (int.TryParse(category, out int categoryNumber))
        {
            // Enumga tegishli bo'lgan raqam bilan tekshiradi
            return Enum.IsDefined(typeof(ExpenseCategory), categoryNumber);
        }
        return false;
    }
    public static bool IsValidAmount(string amount)
    {
        return decimal.TryParse(amount, out decimal result) && result > 0;
    }
    public static bool IsValidDate(string dateTime)
    {
        return DateTime.TryParse(dateTime, out _);
    }
}