using System.Collections;
using Models.Employees.Interfaces;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace Models.Employees;

public class EmployeeList : IEmployeeList, ISeed<EmployeeList>
{
    private List<IEmployee> _employees = new List<IEmployee>();
    
    public IEmployee this[int index] => _employees[index];
    public int Count => _employees.Count;
    
    public bool Seeded { get; set; } = false;
    
    public IEnumerator<IEmployee> GetEnumerator() => _employees.GetEnumerator();

    //Legacy implementation
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 

    
    public IEnumerable<IEmployee> Filter(Func<ICreditCard, IEmployee, bool> predicate)
    {
        // Filter implementation
        var ret = new List<IEmployee>();
        foreach (var emp in _employees)
        {
            foreach(var card in emp.CreditCards)
            {
                if (predicate(card, emp))
                {
                    ret.Add(emp);
                    break;
                }
            }
        }
        return ret;


        /*
        foreach (var emp in _employees)
        {
            foreach (var cc in emp.CreditCards)
            {
                if (predicate(cc, emp))
                {
                    yield return emp;
                    break; // Move to the next employee after the first match
                }
            }
        }
        */
    }
    
    public EmployeeList Seed(SeedGenerator seeder)
    {
        // Create 100 employees
        _employees = seeder.ItemsToList<Employee>(100).ToList<IEmployee>();
        return this;
    }
}