string basePath = @"C:\Users\Lenovo 5i Pro\ExpenseManager";
string dirPath = Path.Combine(basePath, "ExpenseData");
string jsonPath = Path.Combine(dirPath, "expenseData.json");

try
{
    FileHandler.EnsureDirectoryExists(dirPath);
    FileHandler.EnsureFileExists(jsonPath);
}
catch (Exception ex)
{
    Logger.WriteLog(ex.Message);
}

var expenses = FileHandler.ReadFromJsonFile(jsonPath);

while (true)
{
    Console.WriteLine("\n\t\tExpense Manager");
    Console.Write($"{"Xarajat qo'shish:",-30} - 1\n{"Barcha xarajatlarni ko'rish:",-30} - 2\n{"Xarajatlarni qidirish:",-30} - 3\n{"Xarajatlarni filtirlash:",-30} - 4\n{"Dasturdan chiqish:",-30} - 5\n--> ");
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            ExpenseManager.AddExpense(ref expenses, jsonPath);
            break;
        case "2":
            ExpenseManager.ViewAllExpenses(expenses);
            break;
        case "3":
            ExpenseManager.SearchExpenses(expenses);
            break;
        case "4":
            ExpenseManager.FilterExpensesByDateRange(expenses);
            break;
        default:
            Console.WriteLine("\n\tDastur yakunlandi :)");
            return;

    }
}

