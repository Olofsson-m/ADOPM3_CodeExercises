// See https://aka.ms/new-console-template for more information
using Models.Employees;
using Models.Employees.Interfaces;
using Seido.Utilities.SeedGenerator;

using Models.Employees;
using Models.Employees.Interfaces;
using Seido.Utilities.SeedGenerator;

Console.WriteLine("Hello, World!");
var seeder = new SeedGenerator();
var employeeList = new EmployeeList().Seed(seeder);

Console.WriteLine(employeeList.Count);

//write out all info
foreach (var employee in employeeList)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    Console.WriteLine($"  Credit Cards: {employee.CreditCards.Count}");
    foreach (var card in employee.CreditCards)
    {
        Console.WriteLine($"    - {card.Issuer}: {card.Number}");
    }
}

var managementWithAmex = employeeList.Filter(
    (card, employee) => employee.Role == WorkRole.Management && 
                        card.Issuer == CardIssuer.AmericanExpress
);

foreach (var employee in managementWithAmex)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    var amexCards = employee.CreditCards.Where(c => c.Issuer == CardIssuer.AmericanExpress);
    foreach (var card in amexCards)
    {
        Console.WriteLine($"  AMEX: {card.Number} (Exp: {card.ExpirationMonth}/{card.ExpirationYear})");
    }
}

var numberOfEmployeesPerDepartment = employeeList
    .GroupBy(emp => emp.Role)
    .Select(group => new
    {
      Role = group.Key,
      Count = group.Count()  
    });

foreach (var rolegroup in numberOfEmployeesPerDepartment)
{
    System.Console.WriteLine($"{rolegroup.Role}: {rolegroup.Count} employees");
}

var employeesWithoutCard = employeeList.Where(c => c.CreditCards.Count == 0);

foreach (var item in employeesWithoutCard)
{
    System.Console.WriteLine($"{item.FirstName}");
}
var employeesWithFourCards = employeeList.Where(c => c.CreditCards.Count == 4);

foreach (var item in employeesWithFourCards)
{
    System.Console.WriteLine($"{item.FirstName}: has {item.CreditCards.Count} cards");
}



