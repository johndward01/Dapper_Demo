using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace Dapper_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            // ------- READ ----------
            Console.WriteLine("Would you like to see all the departments?");
            var userInput = Console.ReadLine();
            Console.WriteLine();
            var dept_repo = new DapperDepartmentRepository(conn);

            if (userInput.ToLower() == "yes")
            {                
                var departments = dept_repo.GetAllDepartments();
                
                foreach (var dept in departments)
                {
                    Console.WriteLine(dept.Name);
                }
            }
            // ------- CREATE ----------
            Console.WriteLine("Would you like to add a department to the database?");
            userInput = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What is the new department's name?");
            var deptName = Console.ReadLine();
            Console.WriteLine();

            if (userInput.ToLower() == "yes")
            {
                dept_repo.InsertDepartment(deptName);
            }

            Console.WriteLine("Would you like to see the Departments List again?");
            userInput = Console.ReadLine();
            Console.WriteLine();

            if (userInput.ToLower() == "yes")
            {
                foreach(var dept in dept_repo.GetAllDepartments())
                {
                    Console.WriteLine($"{dept.Name} - {dept.DepartmentID}");
                }
            }

            // ------- UPDATE ----------
            Console.WriteLine("Would you like to update a department?");
            userInput = Console.ReadLine();
            Console.WriteLine();

            if (userInput.ToLower() == "yes")
            {
                Console.WriteLine("What is the department's new name?");
                deptName = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("What is the departments's ID?");
                Console.WriteLine();
                var deptID = int.Parse(Console.ReadLine());
                dept_repo.UpdateDepartment(deptName, deptID);

                Console.WriteLine("Would you like to refresh the table");
                userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput.ToLower() == "yes")
                {
                    foreach (var dept in dept_repo.GetAllDepartments())
                    {
                        Console.WriteLine($"{dept.Name} - {dept.DepartmentID}");
                    }
                    Console.WriteLine();
                }

                // ------- DELETE ----------
                Console.WriteLine("Would you like to Delete a department?");
                userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput.ToLower() == "yes")
                {
                    foreach(var dept in dept_repo.GetAllDepartments())
                    {
                        Console.WriteLine($"{dept.Name} - {dept.DepartmentID}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("What is the ID of the department you would like to delete?");
                    Console.WriteLine();
                    var deleteThis = int.Parse(Console.ReadLine());
                    dept_repo.DeleteDepartment(deleteThis);

                    Console.WriteLine("Would you like to see the deparments list again?");
                    userInput = Console.ReadLine();
                    if (userInput.ToLower() == "yes")
                    {
                        foreach (var dept in dept_repo.GetAllDepartments())
                        {
                            Console.WriteLine($"{dept.Name} - {dept.DepartmentID}");
                        }
                    }
                }


            }
            Console.WriteLine();
            Console.WriteLine("Standing By...");
            Console.ReadLine();
        }
    }
}
