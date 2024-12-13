using System.Security.Cryptography;
using System.Xml.Schema;

public static class ExpenseManager
{
    public static void AddExpense(ref List<Expense> expenses, string jsonPath)
    {

        var expense = new Expense();
        expense.Id = SequenseId(expenses); // id
        string helper = "";
        do
        {
            Console.Write("Xarajat tavsifini kiriting -> ");
            helper = Console.ReadLine();
        } while (!Validator.IsValidDescription(helper));
        expense.Description = helper; // description
        do
        {
            Console.Write("Xarajat miqdorini kiriting -> ");
            helper = Console.ReadLine();
        } while (!Validator.IsValidAmount(helper));
        expense.Amount = helper; // amount
        do
        {
            Console.WriteLine($"\tXarajat turlari:\n{"Food",-16} - 1\n{"Entertainment",-16} - 2\n{"Utilities",-16} - 3\n{"Transportation",-16} - 4\n{"Health",-16} - 5\n{"Other",-16} - 6");
            Console.Write("Xarajat turini tanlang -> ");
            helper = Console.ReadLine();
        } while (!Validator.IsValidCategory(helper));
        ExpenseCategory selectedcategory = (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), helper);
        expense.Category = selectedcategory; // enum category
        do
        {
            Console.WriteLine("Namuna: 2024-12-13 yyyy-MM-dd");
            Console.Write("Xarajat sanasini Namuna asosida kiriting -> ");
            helper = Console.ReadLine();
        } while (!Validator.IsValidDate(helper));
        expense.Date = DateTime.Parse(helper); // date
        expenses.Add(expense); // adding expense 
        Console.WriteLine("\tXarajat muvaffaqiyatli qo'shildi :)");

        try
        {
            FileHandler.WriteToJsonFile(jsonPath, expenses);
        }
        catch (Exception ex)
        {
            Logger.WriteLog(ex.Message);
        }
    }

    public static void ViewAllExpenses(List<Expense> expenses)
    {
        if (expenses.Count > 0)
        {
            foreach (var expense in expenses)
                Console.WriteLine(expense.ToString());
        }
        else
            Console.WriteLine("\tXarajatlar mavjud emas!");
    }
    public static void SearchExpenses(List<Expense> expenses)
    {
        if (expenses.Count > 0)
        {
            try
            {
                ViewAllExpenses(expenses);
                Console.WriteLine("\n\tXarajat qidiruv turini tanlang:");
                Console.Write($"{"Id bo'yicha:",-25} - 1\n{"Tavsif bo'yicha:",-25} - 2\n{"Category bo'yicha",-25} - 3\n{"Muddat(Sana) bo'yicha",-25} - 4\n{"Asosiy menyu:",-25} - 5\n--> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        int id;
                        do
                        {
                            Console.Write("Xarajat Id sini kiriring -> ");
                        } while (!int.TryParse(Console.ReadLine(), out id));
                        Expense selectedExpense = expenses.Find(x => x.Id == id);
                        if (selectedExpense != null)
                        {
                            Console.WriteLine(selectedExpense.ToString());
                        }
                        else
                            Console.WriteLine("\tId bo'yicha Xarajat topilmadi!");
                        break;
                    case "2":
                        Console.Write("Tavsifda yozilgan biron-bir kalit so'zni kiriting -> ");
                        string keyword = Console.ReadLine();
                        List<Expense> selectedExpenses = new List<Expense>();
                        foreach (var expense in expenses)
                        {
                            if (expense.Description.Contains(keyword))
                            {
                                selectedExpenses.Add(expense);
                            }
                        }
                        if (selectedExpenses.Count > 0)
                        {
                            foreach (var expense in selectedExpenses)
                            {
                                Console.WriteLine(expense.ToString());
                            }
                        }
                        else
                            Console.WriteLine("\tTavsif bo'yicha Xarajat topilmadi!");
                        break;
                    case "3":
                        string inputCategory = "";
                        do
                        {
                            Console.WriteLine($"\tXarajat turlari:\n{"Food",-16} - 1\n{"Entertainment",-16} - 2\n{"Utilities",-16} - 3\n{"Transportation",-16} - 4\n{"Health",-16} - 5\n{"Other",-16} - 6");
                            Console.Write("Xarajat turini tanlang -> ");
                            inputCategory = Console.ReadLine();
                        } while (!Validator.IsValidCategory(inputCategory));
                        ExpenseCategory selectedCategory = (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), inputCategory);

                        List<Expense> selectedExpensesByCategoty = new List<Expense>();
                        foreach (var expense in expenses)
                        {
                            if (expense.Category == selectedCategory)
                            {
                                selectedExpensesByCategoty.Add(expense);
                            }
                        }
                        if (selectedExpensesByCategoty.Count > 0)
                        {
                            foreach (var expense in selectedExpensesByCategoty)
                            {
                                Console.WriteLine(expense.ToString());
                            }
                        }
                        else
                            Console.WriteLine("\tCategory bo'yicha Xarajatlar topilmadi!");

                        break;
                    case "4":
                        Console.Write($"{"Kun bo'yicha",-16} - 1\n{"Oy bo'yicha",-16} - 2\n{"Yil bo'yicha",-16} - 3\n{"Asosiy menyu",-16} - 4\n--> ");
                        string helper = Console.ReadLine();
                        switch (helper)
                        {
                            case "1":
                                Console.Write("(Sana)kunni kiriting -> ");
                                int day = int.Parse(Console.ReadLine());
                                List<Expense> selectedExpensesByDay = new List<Expense>();
                                foreach (var expense in expenses)
                                {
                                    if (expense.Date.Day == day)
                                    {
                                        selectedExpensesByDay.Add(expense);
                                    }
                                }
                                if (selectedExpensesByDay.Count > 0)
                                {
                                    foreach (var expense in selectedExpensesByDay)
                                    {
                                        Console.WriteLine(expense.ToString());
                                    }
                                }
                                else
                                    Console.WriteLine("\t(Sana)oy bo'yicha xarajatlar topilmadi!");
                                break;
                            case "2":
                                Console.Write("(Sana)oyni kiriting -> ");
                                int month = int.Parse(Console.ReadLine());
                                List<Expense> selectedExpensesByMonth = new List<Expense>();
                                foreach (var expense in expenses)
                                {
                                    if (expense.Date.Month == month)
                                    {
                                        selectedExpensesByMonth.Add(expense);
                                    }
                                }
                                if (selectedExpensesByMonth.Count > 0)
                                {
                                    foreach (var expense in selectedExpensesByMonth)
                                    {
                                        Console.WriteLine(expense.ToString());
                                    }
                                }
                                else
                                    Console.WriteLine("\t(Sana)oy bo'yicha xarajatlar topilmadi!");
                                break;
                            case "3":
                                Console.Write("(Sana)yilni kiriting -> ");
                                int year = int.Parse(Console.ReadLine());
                                List<Expense> selectedExpensesByYear = new List<Expense>();
                                foreach (var expense in expenses)
                                {
                                    if (expense.Date.Year == year)
                                    {
                                        selectedExpensesByYear.Add(expense);
                                    }
                                }
                                if (selectedExpensesByYear.Count > 0)
                                {
                                    foreach (var expense in selectedExpensesByYear)
                                    {
                                        Console.WriteLine(expense.ToString());
                                    }
                                }
                                else
                                    Console.WriteLine("\t(Sana)yil bo'yicha xarajatlar topilmadi!");
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }
        else
            Console.WriteLine("\tXarajatlar mavjud emas!");
    }
    public static void FilterExpensesByDateRange(List<Expense> expenses)
    {
        if (expenses.Count > 0)
        {
            string startDate, endDate;
            do
            {
                Console.WriteLine("Namuna: 2024-12-13 yyyy-MM-dd");
                Console.Write("Xarajat sanasini chegara boshini Namuna asosida kiriting -> ");
                startDate = Console.ReadLine();
            } while (!Validator.IsValidDate(startDate));
            do
            {
                Console.WriteLine("Namuna: 2024-12-13 yyyy-MM-dd");
                Console.Write("Xarajat sanasini chegara oxirini Namuna asosida kiriting -> ");
                endDate = Console.ReadLine();
            } while (!Validator.IsValidDate(endDate));

            try
            {
                DateTime startDatetime = DateTime.Parse(startDate);
                DateTime endDateTime = DateTime.Parse(endDate);
                List<Expense> selectedExpensesByDateTime = new List<Expense>();
                foreach (var expense in expenses)
                {
                    if (expense.Date >= startDatetime && expense.Date <= endDateTime)
                    {
                        selectedExpensesByDateTime.Add(expense);
                    }
                }
                if (selectedExpensesByDateTime.Count > 0)
                {
                    foreach (var expense in selectedExpensesByDateTime)
                    {
                        Console.WriteLine(expense.ToString());
                    }
                }
                else
                    Console.WriteLine("\tBelgilangan Sanalar orag'ida xarajatlar topilmadi!");

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }
        else
            Console.WriteLine("\tXarajatlar mavjud emas!");

    }
    public static int SequenseId(List<Expense> expenses)
    {
        return (expenses.Count > 0) ? (expenses.Max(x => x.Id) + 1) : 1;
    }
}