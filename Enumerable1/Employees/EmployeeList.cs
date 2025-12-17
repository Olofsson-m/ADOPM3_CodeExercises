using System.Collections;
using Models.Employees;
using Models.Employees.Interfaces;
using Seido.Utilities.SeedGenerator;

public class EmployeeList : IEmployeeList, ISeed<EmployeeList>, IEnumerable<IEmployee>
{
    private List<IEmployee> _employees = new();
    public int Count => _employees.Count;
    public IEmployee this[int index] => _employees[index];
    public bool Seeded {get; set;}
    public IEnumerator<IEmployee> GetEnumerator()
    {
        // Return enumerator for _employees
        foreach (var item in _employees)
        {
            yield return item;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerable<IEmployee> Filter(Func<ICreditCard, IEmployee, bool> predicate)
    {
        return _employees.Where(emp => emp.CreditCards.Any(card => predicate(card, emp)));
    }
    
    public EmployeeList Seed(SeedGenerator seeder)
    {
        _employees = seeder.ItemsToList<Employee>(100).ToList<IEmployee>();
        return this;
    }
}