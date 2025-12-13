using System;
using CodingTracker;

class Program
{
    static void Main(string[] args)
    {
        DatabaseManager dbManager = new DatabaseManager(); // Create a DB instance
        dbManager.InitializeDatabase();

        Menuhandler menu = new Menuhandler();

        bool runProgram = true;
        do
        {
            int operation = menu.DisplayMenu();
            switch (operation)
            {
                case 0:
                    runProgram = false;
                    Console.WriteLine("Thank you Good Bye!");
                    return;
                case 1: // Insert
                    var name = UserInput.GetReqInput("Please write your name:");
                    var startDate = UserInput.GetValidDateTimeInput($"Please Write the starting date:({UserInput.DateTimeFormat})");
                    var endDate = UserInput.GetValidDateTimeInput($"Please Write the ending date:({UserInput.DateTimeFormat})");
                    dbManager.InsertRowIntoDB(name, startDate, endDate);
                    break;
                case 2: // Update
                    var updateId = UserInput.GetRequiredInt("Please give an id to update:");
                    name = UserInput.GetReqInput("Please Write your name:");
                    startDate = UserInput.GetValidDateTimeInput($"Please Write the starting date:({UserInput.DateTimeFormat})");
                    endDate = UserInput.GetValidDateTimeInput($"Please Write the ending date:({UserInput.DateTimeFormat})");
                    dbManager.UpdateRowInDB(updateId, name, startDate, endDate);
                    break;
                case 3: // Delete
                    var deleteId = UserInput.GetRequiredInt("Please give an id to delete:");
                    dbManager.DeleteRowInDB(deleteId);
                    break;
                case 4: // View all
                    dbManager.ViewAllRecords();
                    break;
            }

        } while (runProgram);
    }
}