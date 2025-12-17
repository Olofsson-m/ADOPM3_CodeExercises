// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace IEnumerable // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nFriendList - friends1");
            var friendList1 = FriendList.Factory.CreateRandom(100);
            Console.WriteLine($"[12]: {friendList1[12]}");
            
            //1. modify the code in FriendList so you can iterate over all friends in friendList1
            //using a for loop
            //Your code:
            Console.WriteLine("\nfriendList1 using for - loop");
            
            for (var i = 0; i < friendList1.Count; i++)
            {
                Console.WriteLine(friendList1[i]);
            }

            //2. modify the code in FriendList so you can iterate over all friends in friendList1
            //using a foreach loop
            //Your code:
            Console.WriteLine("\nfriendList1 using foreach - loop");

            foreach (var item in friendList1.fl)
            {
                System.Console.WriteLine(item);
            }

            //3. Use Linq to sort your friend list according to country
            //Your code:
            Console.WriteLine("\nSorted copyfriends using foreach - loop");
            var sortedList = friendList1.fl.OrderBy(n => n.Address.Country);
            
            foreach (var item in sortedList)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
//Exercises make a class an enumerable
//1. Modify the code according to the instructions above for point 1 to 3
//2. Sort and print the list according to instructions above for point 3