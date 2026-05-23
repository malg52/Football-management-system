using Praktika.DAL;
using Praktika.DAL.Entities;
using Praktika_menu;
using System;

namespace Praktika
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ");
            var rep = new PrRepository();
            Menu menu = new Menu();


            while (true)
            {
                int choice;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1 - Add team.\n2 - Show all teams.\n3 - Delete team.\n4 - Update team.\n5 - Find team.\n6 - Filter by parameters.\n7 - Player and match functions.\n8 - Add/Delete/Update match.\n9 - Top scorers statistics.\n10 - Team goal statistics.\n0 - Exit.");
                    Console.Write("Enter: ");
                    string inp = Console.ReadLine();

                    if (int.TryParse(inp, out choice))
                    {
                        if (choice >= 0 && choice <= 10)
                            break;
                        else
                        Console.WriteLine("Error! Enter a number from 0 to 10.");
                        menu.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Input error! Enter a number.");
                        menu.ReadKey();
                    }
                }
                Console.Clear();
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        AddTeam(rep, menu);
                        break;
                    case 2:
                        ShowAllTeams(rep, menu);
                        break;
                    case 3:
                        DeleteTeam(rep, menu);
                        break;
                    case 4:
                        UpdateTeam(rep, menu);
                        break;
                    case 5:
                        SearchTeam(rep, menu);
                        break;
                    case 6:
                        SelectTeam(rep, menu);
                        break;
                    case 7:
                        Console.Clear();
                        MatchFunctions(rep, menu);
                        break;
                    case 8:
                        Console.Clear();
                        Add_Upd_Del_Match(rep, menu);
                        break;
                    case 9:
                        Console.Clear();
                        TopScorers(rep, menu);
                        break;
                    case 10:
                        Console.Clear();
                        TopTeams(rep, menu);
                        break;
                }
            }
        }
        static void TopTeams(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_TopTeams(rep);
        }

        static void TopScorers(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_TopScorers(rep);
        }

        static void Add_Upd_Del_Match(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_Add_Upd_Del_Match(rep);
        }

        static void MatchFunctions(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_MatchFunctions(rep);
        }

        static void AddTeam(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_AddTeam(rep);
        }
        static void ShowAllTeams(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_ShowAllTeams(rep);
        }
        static void DeleteTeam(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_DeleteTeam(rep);
        }
        static void UpdateTeam(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_UpdateTeam(rep);
        }
        static void SearchTeam(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_SearchTeam(rep);
        }
        static void SelectTeam(PrRepository rep, Menu menu)
        {
            Console.Clear();
            menu.Menu_SelectTeam(rep);
        }
    }
}


