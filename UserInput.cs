using System.Globalization;

namespace CodingTracker
{
    public class UserInput
    {
        public static int GetRequiredInt(string prompt)
        {
            int result = 0;
            string? input;
            bool isValid;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("The input can't be empty!");
                    isValid = false;
                }
                else
                {
                    isValid = int.TryParse(input, out result); // From the string input it makes an integer result. 

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid format!");
                    }
                }
            } while (!isValid);

            return result;
        }


        public static string GetReqInput(string prompt) // Check if the string input exist. If not or whitespace -> repeat
        {
            string? input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("The input can not empty!");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static string GetValidDateTimeInput(string prompt)
        {
            DateTime parsedDate;
            string input;
            bool isValid = false;

            do
            {
                input = GetReqInput(prompt);

                isValid = DateTime.TryParseExact(
                    input,
                    DateTimeFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate
                );

                if (!isValid)
                {
                    Console.WriteLine($"\n Invalid date format. Please use the correct format: {DateTimeFormat}\n");
                }

            } while (!isValid);

            return parsedDate.ToString(DateTimeFormat);
        }
    }

}