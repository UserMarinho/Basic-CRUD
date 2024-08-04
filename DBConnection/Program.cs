using DBConnection.Entities;
using DBConnection.Database;
using System.Timers;
using System.Threading;

namespace DBConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            bool appFinish = false;
            using (var dbContext = new AppDbContext())
            {
                while (!appFinish) 
                {
                    Console.WriteLine("What do you want?");
                    Console.WriteLine("[0] Exit the app");
                    Console.WriteLine("[1] Add a student");
                    Console.WriteLine("[2] Update a student's information");
                    Console.WriteLine("[3] Remove a student");
                    Console.WriteLine("[4] Search for a student");
                    Console.WriteLine("[5] List all students");
                    Console.Write("> ");
                    string? option = Console.ReadLine().ToUpper();

                    switch (option)
                    {       
                        case "0":
                        case "EXIT":
                            appFinish = true;
                            break;
                        case "1":
                        case "ADD":
                            {
                                bool success = false;
                                while (!success)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter the student's information: ");

                                        Console.Write("Name: ");
                                        string name = Console.ReadLine();

                                        Console.Write("Birth Date: ");
                                        DateTime birth = DateTime.Parse(Console.ReadLine());

                                        Console.Write("CPF: ");
                                        string cpf = Console.ReadLine();

                                        Console.Write("Email: ");
                                        string email = Console.ReadLine();

                                        Student stdnt = new Student(cpf, name, email, birth);

                                        dbContext.Add(stdnt);
                                        dbContext.SaveChanges();

                                        success = true;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"An error has occurred: {e.Message}");
                                        Thread.Sleep(3000);
                                    }
                                }
                                break;
                            }
                        case "2":
                        case "UPDATE":
                            {
                                bool success = false;
                                while (!success)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Console.Write("Enter the student's ID: ");
                                        string id = Console.ReadLine();
                                        if (id == "")
                                        {
                                            break;
                                        }
                                        var stdnt = dbContext.Students.FirstOrDefault(s => s.Id == int.Parse(id));

                                        if (stdnt != null)
                                        {
                                            Console.WriteLine("Update the student's information: ");
                                            Console.Write("Name: ");
                                            string name = Console.ReadLine();

                                            Console.Write("Birth Date: ");
                                            DateTime birth = DateTime.Parse(Console.ReadLine());

                                            Console.Write("CPF: ");
                                            string cpf = Console.ReadLine();

                                            Console.Write("Email: ");
                                            string? email = Console.ReadLine();

                                            stdnt.Name = name;
                                            stdnt.BirthDate = birth;
                                            stdnt.CPF = cpf;
                                            stdnt.Email = email;

                                            dbContext.Update(stdnt);
                                            dbContext.SaveChanges();

                                            success = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("ID not found");
                                            Thread.Sleep(1000);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"An error has occurred: {e.Message}");
                                        Thread.Sleep(3000);
                                    }
                                }
                                break;
                            }
                        case "3":
                        case "REMOVE":
                            {
                                bool success = false;
                                while (!success)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Console.Write("Enter the student's ID: ");
                                        string id = Console.ReadLine();
                                        if (id == "")
                                        {
                                            break;
                                        }
                                        var stdnt = dbContext.Students.FirstOrDefault(s => s.Id == int.Parse(id));

                                        if (stdnt != null)
                                        {
                                            Console.WriteLine(stdnt);
                                            Console.WriteLine("\nDo you want to delete this student? (y/n)");
                                            Console.Write("> ");
                                            string? confirmation = Console.ReadLine().ToUpper();

                                            if (confirmation == "Y")
                                            {
                                                dbContext.Remove(stdnt);
                                                dbContext.SaveChanges();
                                                Console.WriteLine("Student deleted");
                                                Thread.Sleep(1000);
                                            }

                                            success = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("ID not found");
                                            Thread.Sleep(1000);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"An error has occurred: {e.Message}");
                                        Thread.Sleep(3000);
                                    }
                                }
                                break;
                            }
                        case "4":
                        case "SEARCH":
                            {
                                bool success = false;
                                while (!success)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Console.Write("Enter the student's CPF: ");
                                        string cpf = Console.ReadLine();
                                        if (cpf == "")
                                        {
                                            break;
                                        }
                                        var stdnt = dbContext.Students.FirstOrDefault(s => s.CPF == cpf);

                                        if (stdnt != null)
                                        {
                                            Console.WriteLine(stdnt);
                                            success = true;
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("CPF not found");
                                            Thread.Sleep(1000);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"An error has occurred: {e.Message}");
                                        Thread.Sleep(3000);
                                    }
                                }
                                break;
                            }
                        case "5":
                        case "LIST":
                            {
                                bool success = false;
                                while (!success)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Console.WriteLine("All students:");
                                        var stdnts = dbContext.Students.ToList();

                                        if (stdnts != null)
                                        {
                                            foreach (Student s in stdnts)
                                            {
                                                Console.WriteLine(s);
                                            }
                                            success = true;
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error");
                                            Thread.Sleep(1000);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"An error has occurred: {e.Message}");
                                        Thread.Sleep(3000);
                                    }
                                }
                                break;
                            }
                        default:
                            Console.WriteLine("Enter a valid option");
                            Thread.Sleep(1000);
                            break;
                    }

                    Console.Clear();
                }
            }
        }
    }
}
