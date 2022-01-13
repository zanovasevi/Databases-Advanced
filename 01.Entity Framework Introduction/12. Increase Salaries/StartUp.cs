using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new SoftUniContext();
            Console.WriteLine(IncreaseSalaries(context));

        }

        // 7 задача
        //public static string GetEmployeesInPeriod(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //        .Include(x => x.EmployeesProjects)
        //        .ThenInclude(x => x.Project)
        //        .Where(x => x.EmployeesProjects.Any(x => x.Project.StartDate.Year >= 2001 &&
        //                                                x.Project.StartDate.Year <= 2003))
        //        .Select(x => new
        //        {
        //            EmployeeFirstName = x.FirstName,
        //            EmployeeLasttName = x.LastName,
        //            ManagerFirstName = x.Manager.FirstName,
        //            ManagerLastName = x.Manager.LastName,
        //            Projects = x.EmployeesProjects.Select(x => new
        //            {
        //                ProjectName = x.Project.Name,
        //                StartDate = x.Project.StartDate,
        //                EndDate = x.Project.EndDate
        //            })
        //        })
        //        .Take(10)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var emp in employees)
        //    {
        //        sb.AppendLine($"{emp.EmployeeFirstName} {emp.EmployeeLasttName} - Manager: " +
        //            $"{emp.ManagerFirstName} {emp.ManagerLastName}");

        //        foreach (var project in emp.Projects)
        //        {
        //            if(project.EndDate.HasValue)
        //            {
        //                sb.AppendLine($"--{project.ProjectName} - {project.StartDate} - " +
        //                $"{project.EndDate}");
        //            }
        //            else
        //            {
        //                sb.AppendLine($"--{project.ProjectName} - {project.StartDate} - not finished");
        //            }

        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // 8 задача
        //public static string GetAddressesByTown(SoftUniContext context)
        //{
        //    var addresses = context.Addresses
        //        .OrderByDescending(x => x.Employees.Count)
        //        .ThenBy(x => x.Town.Name)
        //        .ThenBy(x => x.AddressText)
        //        .Take(10)
        //        .Select(x => new
        //        {
        //            AddressText = x.AddressText,
        //            TownName = x.Town.Name,
        //            EmployeeCount = x.Employees.Count
        //        })
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var ad in addresses)
        //    {
        //        sb.AppendLine($"{ad.AddressText}, {ad.TownName} - {ad.EmployeeCount} employees");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // 9 задача
        //public static string GetEmployee147(SoftUniContext context)
        //{
        //    var employee = context.Employees
        //        .Where(x => x.EmployeeId == 147)
        //        .Select(x => new
        //        {
        //            x.FirstName,
        //            x.LastName,
        //            x.JobTitle,
        //        })
        //        .ToList();

        //    var projects = context.Projects
        //        .Include(x => x.EmployeesProjects)
        //        .ThenInclude(x => x.Employee)
        //        .Where(x => x.EmployeesProjects.Any(x => x.EmployeeId == 147))
        //        .OrderBy(x => x.Name)
        //        .ToList();

        //    var sb = new StringBuilder();


        //    foreach (var emp in employee)
        //    {
        //        sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");

        //        foreach (var pr in projects)
        //        {
        //            sb.AppendLine($"{pr.Name}");
        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // 10 задача
        //public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        //{
        //    var departments = context.Departments
        //        .Where(x => x.Employees.Count > 5)
        //        .OrderBy(x => x.Employees.Count)
        //        .ThenBy(x => x.Name)
        //        .Select(x => new
        //        {
        //            DepartmentName = x.Name,
        //            ManagerFirstName = x.Manager.FirstName,
        //            ManagerLastName = x.Manager.LastName,
        //            Employeess = x.Employees.Select(x => new
        //            {
        //                EmployeeFirstName = x.FirstName,
        //                EmployeeLastName = x.LastName,
        //                EmployeeJobTitle = x.JobTitle
        //            })
        //            .OrderBy(x => x.EmployeeFirstName)
        //            .ThenBy(x => x.EmployeeLastName)
        //            .ToList()
        //        })
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var dep in departments)
        //    {
        //        sb.AppendLine($"{dep.DepartmentName} - {dep.ManagerFirstName} {dep.ManagerLastName}");

        //        foreach (var emp in dep.Employeess)
        //        {
        //            sb.AppendLine($"{emp.EmployeeFirstName} {emp.EmployeeLastName} - {emp.EmployeeJobTitle}");
        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // 11 задача
        //public static string GetLatestProjects(SoftUniContext context)
        //{
        //    var projects = context.Projects
        //        .OrderByDescending(x => x.StartDate)
        //        .Take(10)
        //        .Select(x => new
        //        {
        //            ProjectName = x.Name,
        //            DescriptionName = x.Description,
        //            StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
        //        })
        //        .OrderBy(x => x.ProjectName)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var pr in projects)
        //    {
        //        sb.AppendLine($"{pr.ProjectName}");
        //        sb.AppendLine($"{pr.DescriptionName}");
        //        sb.AppendLine($"{pr.StartDate}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // 12 задача
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.Department.Name == "Engineering" ||
                            x.Department.Name == "Tool Design" ||
                            x.Department.Name == "Marketing" ||
                            x.Department.Name == "Information Services")
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            foreach (var emp in employees)
            {
                emp.Salary = emp.Salary + (emp.Salary * 0.12m);
            }

            var sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        //// 13 задача
        //public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //        .Where(x => x.FirstName.StartsWith("Sa", true, CultureInfo.InvariantCulture))
        //        .Select(x => new
        //        {
        //            x.FirstName,
        //            x.LastName,
        //            x.JobTitle,
        //            x.Salary
        //        })
        //        .OrderBy(x => x.FirstName)
        //        .ThenBy(x => x.LastName)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach(var emp in employees)
        //    {
        //        sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:F2})");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //// 14 задача
        //public static string DeleteProjectById(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    var employeesProjectsToDelete = context.EmployeesProjects
        //        .Where(ep => ep.ProjectId == 2);

        //    var project = context.Projects
        //        .Where(p => p.ProjectId == 2)
        //        .Single();

        //    foreach (var ep in employeesProjectsToDelete)
        //    {
        //        context.EmployeesProjects.Remove(ep);
        //    }

        //    context.Projects.Remove(project);

        //    context.SaveChanges();

        //    context.Projects
        //        .Take(10)
        //        .Select(p => p.Name)
        //        .ToList()
        //        .ForEach(p => sb.AppendLine(p));

        //    return sb.ToString().Trim();
        //}


    }
}
