public static class Logger
{
    public static readonly string logPath = @"C:\Users\Lenovo 5i Pro\ExpenseManager\log.txt";
    public static void WriteLog(string log)
    {
        using (StreamWriter streamWriter = new StreamWriter(logPath, true))
        {
            streamWriter.WriteLine(log + " " + DateTime.Now.ToString("dd-MM-yyyy, HH-mm-ss"));
            Console.WriteLine("\n\tXalotik yuz berdi va log.txt file ga yozildi :(");
        }
    }
}