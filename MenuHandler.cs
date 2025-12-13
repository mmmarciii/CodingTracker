using System.Runtime.CompilerServices;

namespace CodingTracker
{
    public class Menuhandler
    {
        private List<string> menuOptions = new List<string>
        {
            "1. Insert",
            "2. Update",
            "3. Delete",
            "4. View all",
            "0. Exit"
        };

        public int DisplayMenu()
        {

            Console.WriteLine("----- MENU -----");
            Console.WriteLine("----------------");
            foreach (string option in menuOptions)
            {
                Console.WriteLine(option);
            }
            Console.WriteLine("----------------");
            var operation = UserInput.GetRequiredInt("Please choose an option");
            return operation;
        }
    }
}