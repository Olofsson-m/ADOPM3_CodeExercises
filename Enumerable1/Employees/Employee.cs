using Models.Employees.Interfaces;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace Models.Employees;

public class Employee : IEmployee, ISeed<Employee> 
{
    public Guid EmployeeId {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public DateTime HireDate {get; set;}
    public WorkRole Role {get; set;}
    public List<ICreditCard> CreditCards {get; set;}
    public bool Seeded {get; set;}

    public Employee(){ }
    public Employee(IEmployee org)
    {
        EmployeeId = org.EmployeeId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        HireDate = org.HireDate;
        Role = org.Role;
        CreditCards = new List<ICreditCard>(org.CreditCards);
        Seeded = false;
    }

    public Employee Seed(SeedGenerator seeder)
    {
        Seeded = true;
        EmployeeId = Guid.NewGuid();
        FirstName = seeder.FirstName;
        LastName = seeder.LastName;
        HireDate = seeder.DateAndTime();
        Role = seeder.FromEnum<WorkRole>();
        
        CreditCards = new List<ICreditCard>();
        int creditCardCount = seeder.Next(0, 5);
        for (int i = 0; i < creditCardCount; i++)
        {
            var card = new CreditCard();
            card.Seed(seeder);
            CreditCards.Add(card);
        }
        return this;
    }
    
}