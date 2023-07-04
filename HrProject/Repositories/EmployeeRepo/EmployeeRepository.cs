﻿using HrProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.EmployeeRepo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrContext context;
        public EmployeeRepository(HrContext context)
        {
            this.context = context;
        }
        public List<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            return context.Employees.Include(e=>e.Department).FirstOrDefault(emp => emp.Id == id);
        }
        public List< Employee> GetEmployeeByName(string name)
        {
            return context.Employees
                .Where(emp =>
                    (emp.FirstName ?? string.Empty + emp.LastName ?? string.Empty)
                        .ToLower()
                        .Contains(name.ToLower())
                    )
                .ToList();
        }

        public void Insert(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }
        public void Update(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Employee oldEmployee = GetEmployeeById(id);
            context.Employees.Remove(oldEmployee);
            context.SaveChanges();
        }
        public Employee GetEmployeeByNationalId(int Id)
        {
            return context.Employees.FirstOrDefault(e =>int.Parse(e.NationalId) == Id);
        }

    }
}
