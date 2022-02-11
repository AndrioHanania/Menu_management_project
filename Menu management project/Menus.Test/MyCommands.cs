using System;

namespace Menus.Test
{
    public static class MyCommands
    {
        public static void ShowVersion()
        {
            System.Console.WriteLine("Version: 21.1.4.8930");
        }

        public static void ShowTime()
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
        }

        public static void ShowDate()
        {
            Console.WriteLine(DateTime.Now.Date.ToShortDateString());
        }

        public static void CountSpaces()
        {
            System.Console.WriteLine("Please enter string: ");
            string input = System.Console.ReadLine();
            int countSpaces = 0;

            foreach (char tav in input)
            {
                if (tav == ' ')
                {
                    countSpaces++;
                }
            }

            System.Console.WriteLine("The number of spaces in {2}{0}{2} is: {1}", input, countSpaces, '"');

        }
    }
}