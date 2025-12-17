using System;
using System.Collections.Generic;

namespace Lambda3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Lambda Variable Capture Demo ===\n");

            // Example 1: Simple variable capture
            SimpleVariableCapture();

            Console.WriteLine("\n" + new string('-', 60) + "\n");

            // Example 2: The WRONG way - Capturing loop variable (deferred execution)
            WrongLoopCapture();

            Console.WriteLine("\n" + new string('-', 60) + "\n");

            // Example 3: The RIGHT way - Using a local copy
            CorrectLoopCapture();
        }

        static void SimpleVariableCapture()
        {
            Console.WriteLine("Example 1: Simple Variable Capture");
            Console.WriteLine("-----------------------------------");

            string greeting = "Hello";
            string name = "World";

            // Lambda captures the variables 'greeting' and 'name'
            Action sayHello = () => Console.WriteLine($"{greeting}, {name}!");

            sayHello(); // Output: Hello, World!

            // Changing the captured variable affects the lambda
            greeting = "Hi";
            name = "Lambda";

            sayHello(); // Output: Hi, Lambda!

            Console.WriteLine("\nNote: Lambda captures variables by REFERENCE, not by value.");
        }

        static void WrongLoopCapture()
        {
            Console.WriteLine("Example 2: WRONG - Capturing Loop Variable (Common Mistake)");
            Console.WriteLine("------------------------------------------------------------");

            List<Action> actions = new List<Action>();

            // WRONG: All lambdas will capture the SAME variable 'i'
            for (int i = 0; i < 5; i++)
            {
                int si = i;
                actions.Add(() => Console.WriteLine($"Value: {si}"));
            }

            Console.WriteLine("\nExecuting deferred actions:");
            Console.WriteLine("Expected: 0, 1, 2, 3, 4");
            Console.Write("Actual:   ");
            
            foreach (var action in actions)
            {
                action(); // All will print 5! (the final value of i after the loop)
            }

            Console.WriteLine("\nWhy? All lambdas captured the SAME variable 'i'.");
            Console.WriteLine("After the loop, i = 5, so all lambdas see 5.");
        }

        static void CorrectLoopCapture()
        {
            Console.WriteLine("Example 3: CORRECT - Using Local Copy");
            Console.WriteLine("--------------------------------------");

            List<Action> actions = new List<Action>();

            // CORRECT: Create a local copy for each iteration
            for (int i = 0; i < 5; i++)
            {
                int localCopy = i; // Each iteration gets its own variable
                actions.Add(() => Console.WriteLine($"Value: {localCopy}"));
            }

            Console.WriteLine("\nExecuting deferred actions:");
            Console.WriteLine("Expected: 0, 1, 2, 3, 4");
            Console.Write("Actual:   ");
            
            foreach (var action in actions)
            {
                action(); // Now correctly prints 0, 1, 2, 3, 4
            }

            Console.WriteLine("\nWhy? Each lambda captures its OWN variable 'localCopy'.");
        }
    }
}

/* Exercise
1. Use the debugger to run through each example and observe how variable capture works.
2. Modify the SimpleVariableCapture method to demonstrate capturing a variable of a different type (e.g., an integer).
*/