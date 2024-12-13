public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public ExpenseCategory Category { get; set; }
    public DateOnly Date { get; set; }

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