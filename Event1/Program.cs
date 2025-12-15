// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using Microsoft.AspNetCore.Http;
using Seido.Utilities.SeedGenerator;

namespace Event1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
          Console.WriteLine("\nHuge friendlist");
          
          var friendList = new FriendList();

          friendList.CreationProgress += EventHandler;
          
          friendList.Seed(100000);

        }

        //Declare your Eventhandler
        
        public static void EventHandler(object fl, int sum)
        {
          System.Console.WriteLine(sum);            
        }
        //Your code
    }
}
//Exercise
//1. In Friendlist implement the firing of an event, 
//2. In Program implement the event handler and assign it to the event CreationProgress