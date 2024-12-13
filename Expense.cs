public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Amount { get; set; }
    public ExpenseCategory Category { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Category: {Category}, Amount: {Amount}, Date: {Date}\nDescription: {Description}";
    }

}

public enum ExpenseCategory
{
    Food = 1,       // oziq ovqat
    Entertainment, // kongilochar
    Utilities,     // komunal
    Transportation, // Transport
    Health,         // salomatlik
    Other           // boshqa
}