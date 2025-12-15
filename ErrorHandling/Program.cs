using System;

namespace ErrorHandling
{
    public enum Severity {Fatal, Manageble}
    class ExplosionException : Exception
    {
        public int ButtonPressed {get; set;}
        public Severity severity1 {get; set;}  
        public ExplosionException(int buttonPressed)
        {
            ButtonPressed = buttonPressed;
            if (buttonPressed == 5)
            {
                severity1 = Severity.Manageble;
                System.Console.WriteLine("You pressed a bad button ");
            }
            if (buttonPressed == 7)
            {
                severity1 = Severity.Fatal;
                System.Console.WriteLine("KAAABOOOM!!!");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ProcessUserInput();
            }
            catch (Exception ex)
            {
                //AppLog.Instance.LogException(ex);
                Console.WriteLine("Pls contact our service center, you have a virus in your computer");
            }
            finally
            {
                Console.WriteLine(AppLog.Instance.WriteToDisk());
            }
        }
        private static void ProcessUserInput()
        {
            bool quit = false;
            Console.WriteLine("Don't Press Button 5 or 7!");
            do
            {
                Console.WriteLine("Which button do you want to press?");
                string sButtonToPress = Console.ReadLine();

                if (sButtonToPress == "q")
                {
                    quit = true;
                }
                else
                {
                    int buttonToPress;
                    if (int.TryParse(sButtonToPress, out buttonToPress))
                    {
                        try
                        {
                            PressTheButton(buttonToPress);
                            Console.WriteLine("Indeed the button was pressed successfully");
                        }
                        catch (ExplosionException ex) when (ex.severity1 == Severity.Fatal)
                        {
                            AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message} - Why cant you listen!!");
                            throw;
                        }
                        catch (ExplosionException ex) when (ex.severity1 == Severity.Manageble)
                        {
                            //AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message} - But it is alright my friend!");
                        }
                        finally
                        {
                            Console.WriteLine("Code here will always be executed!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, try again");
                    }
                }
            } while (!quit);
        }
        static void PressTheButton(int buttonNr)
        {
            if (buttonNr == 5)
                throw new ExplosionException(buttonNr);

            if (buttonNr == 7)
                throw new ExplosionException(buttonNr);

            Console.WriteLine($"You pressed button {buttonNr}");
        }

    }
}



//Exercise:
//1. Create your own exception class called ExplosionException with two properties: ButtonPressed and Severity (enum with values Manageable and Fatal). 
//2. Throw a ExplosionException when button 6 is pressed with Severity set to Manageable.
//3. Throw a ExplosionException when button 8 is pressed with Severity set to Fatal.
//4. Modify the code in ProcessUserInput() to catch ExplosionException and depending on severity gives an user message(manageable) or rethrow (fatal)
