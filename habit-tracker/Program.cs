using System;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static string connectionString = @"Data Source=habit-tracker.db";

    public static void Main(string[] args)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = 
            @"CREATE TABLE IF NOT EXISTS drinking_water (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT,
                Quantity INTEGER        
            )";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
        GetUserInput();
    }

    static void GetUserInput()
    {
        Console.Clear();
        bool closeApp = false;
        while(closeApp == false)
        {
            System.Console.WriteLine("\n\nMAIN MENU");
            System.Console.WriteLine("\nWHAT would you like to do?");
            System.Console.WriteLine("\nType 0 to Close Application");
            System.Console.WriteLine("Type 1 to View All Records.");
            System.Console.WriteLine("Type 2 to Insert Record");
            System.Console.WriteLine("Type 3 to Delete Record");
            System.Console.WriteLine("Type 4 to Update Record");
            System.Console.WriteLine("-------------------------------------\n");

            string commandInput = Console.ReadLine();

            switch (commandInput)
            {
                case "0":
                    System.Console.WriteLine("\nGoodbye!");
                    closeApp = true;
                break;
                /**
                case 1:
                    GetAllRecords();
                break;**/
                case "2":
                    Insert();
                break;
                /**
                case 3:
                    Delete();
                break;
                case 4:
                    Update();
                break;
                default:
                System.Console.WriteLine("\n Invalid command. Please type a number from 0 to 4!");
                break;
                **/
            }
        }
    }
    private static void Insert()
    {
        string date = GetDateInput();
        int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice - no DECIMALS ALLOWED");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = 
            $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }
    internal static string GetDateInput()
    {
        System.Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu");

        string dateInput = Console.ReadLine();

        if(dateInput == "0")
        {
            GetUserInput();
        }
        return dateInput;
    }

    internal static int GetNumberInput(string message)
    {
        System.Console.WriteLine(message);

        string numberInput = Console.ReadLine();    

        if (numberInput == "0") GetUserInput();
 
        int result = Convert.ToInt32(numberInput);   
        return result; 
    }
}


