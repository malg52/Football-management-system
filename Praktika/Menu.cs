using Microsoft.IdentityModel.Tokens;
using Praktika;
using Praktika.DAL;
using Praktika.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Praktika_menu
{
    public class Menu
    {
        public Menu() { }

        //Модуль 6 Часть 4
        public void Menu_TopTeams(PrRepository repo)
        {
            while (true)
            {
                Console.WriteLine("1 - Top 3 teams by goals scored");
                Console.WriteLine("2 - Team with the most goals scored");
                Console.WriteLine("3 - Top 3 teams with the fewest goals conceded");
                Console.WriteLine("4 - Team with the fewest goals conceded");
                Console.WriteLine("0 - Exit");
                int choice = EnterInt("Select an option: ");
                if (choice == 0) return;
                TopTeams_Swich(repo, choice);
            }
            
        }
        public void TopTeams_Swich(PrRepository repo, int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    var top3Scored = repo.Top3_TeamsByGoalsScored();
                    PrintTeams(top3Scored, "Top 3 teams by goals scored:");
                    ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    var top1Scored = repo.Top1_TeamByGoalsScored();
                    PrintTeams(top1Scored != null ? new List<Team> { top1Scored } : new List<Team>(),
                        "Team with the most goals scored:");
                    ReadKey();
                    break;

                case 3:
                    Console.Clear();
                    var top3Conceded = repo.Top3_TeamsByGoalsConceded();
                    PrintTeams(top3Conceded, "Top 3 teams with the fewest goals conceded:");
                    ReadKey();
                    break;

                case 4:
                    Console.Clear();
                    var top1Conceded = repo.Top1_TeamByGoalsConceded();
                    PrintTeams(top1Conceded != null ? new List<Team> { top1Conceded } : new List<Team>(),
                        "Team with the fewest goals conceded:");
                    ReadKey();
                    break;

                default:
                    Console.WriteLine("Invalid selection!");
                    ReadKey();
                    break;
            }
        }
        public void PrintTeams(List<Team> teams, string message)
        {
            if (teams.Count == 0)
                Console.WriteLine("No teams found.");
            else
            {
                Console.WriteLine(message);
                foreach (var t in teams)
                {
                    Console.WriteLine($"{t.Name} ({t.City}) - Scored: {t.Goals_scored}, Conceded: {t.Goals_missed}");
                }
            }
        }
        public void Menu_TopScorers(PrRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Top 3 scorers of a team");
                Console.WriteLine("2 - Best scorer of a team");
                Console.WriteLine("3 - Top 3 scorers of the league");
                Console.WriteLine("4 - Best scorer of the league");
                Console.WriteLine("0 - Exit");

                int choice = EnterInt("Select an option: ");
                if (choice == 0) return;
                TopScorers_Swich(repo,choice);
            }
            
        }
        public void TopScorers_Swich(PrRepository repo,int choice)
        {
            Console.Clear();

            string teamName = null;
            if (choice == 1 || choice == 2)
            {
                teamName = EnterString("Enter team name: ");
                if (!CheckTeamExists(repo, teamName)) return;
            }
            switch (choice)
            {
                case 1:
                    PrintTopScorers(repo.Top3_ScorersByTeam(teamName), $"Top 3 scorers of team {teamName}:");
                    break;

                case 2:
                    PrintTopScorers(repo.Top1_ScorersByTeam(teamName), $"Best scorer of team {teamName}:");
                    break;

                case 3:
                    PrintTopScorers(repo.Top3_ScorersByAllTeams(), "Top 3 scorers of the whole championship:");
                    break;

                case 4:
                    PrintTopScorers(repo.Top1_ScorersByAllTeams(), "Best scorer of the championship:");
                    break;

                default:
                    Console.WriteLine("Invalid selection!");
                    break;
            }
            ReadKey();
        }
        public void PrintTopScorers(List<(Player player, int goals)> scorers, string message)
        {
            if (scorers.Count == 0)
            {
                Console.WriteLine("No goals found.");
            }
            else
            {
                Console.WriteLine(message);
                foreach (var s in scorers)
                {
                    if (s.player.Team != null)
                        Console.WriteLine($"{s.player.FullName} ({s.player.Team.Name}) - {s.goals} goals");
                    else
                        Console.WriteLine($"{s.player.FullName} - {s.goals} goals");
                }
            }
        }
        public bool CheckTeamExists(PrRepository repo, string teamName)
        {
            var teamExists = repo.GetAll().Any(t => t.Name == teamName);
            if (!teamExists)
            {
                Console.WriteLine($"Team '{teamName}' was not found in the database.");
                ReadKey();
                return false;
            }
            return true;
        }
        public void Menu_TopPoints(PrRepository repo)
        {
            Console.Clear();
            Console.WriteLine("1 - Top 3 teams by points");
            Console.WriteLine("2 - Team with the highest number of points");
            Console.WriteLine("3 - Top 3 teams with the lowest number of points");
            Console.WriteLine("4 - Team with the lowest number of points");

            int choice = EnterInt("Enter your choice: ");

            var teamsWithPoints = repo.TeamsWithPoints();
            Console.Clear();
            switch (choice)
            {
                case 1:
                    PrintTeams(teamsWithPoints.OrderByDescending(t => t.Points).Take(3).Select(t => t.Team).ToList(), "Top 3 teams by points:");
                    break;

                case 2:
                    PrintTeams(teamsWithPoints.OrderByDescending(t => t.Points).Take(1).Select(t => t.Team).ToList(), "Team with the highest number of points:");
                    break;

                case 3:
                    PrintTeams(teamsWithPoints.OrderBy(t => t.Points).Take(3).Select(t => t.Team).ToList(), "Top 3 teams with the lowest number of points:");
                    break;

                case 4:
                    PrintTeams(teamsWithPoints.OrderBy(t => t.Points).Take(1).Select(t => t.Team).ToList(), "Team with the lowest number of points:");
                    break;

                default:
                    Console.WriteLine("Invalid selection!");
                    break;
            }
            ReadKey();
        }


        //------------------------------------------------------------------ 
        // Модуль 6 Часть 3
        public void Menu_MatchFunctions(PrRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ADDITIONAL FUNCTIONS:");
                Console.WriteLine("1 - Goal difference (scored vs conceded) for each team");
                Console.WriteLine("2 - Full information about all matches");
                Console.WriteLine("3 - Matches on a specific date");
                Console.WriteLine("4 - Matches of a specific team");
                Console.WriteLine("5 - Players who scored goals on a specific date");
                Console.WriteLine("6 - Add matches randomly");
                Console.WriteLine("0 - Back");

                int choice = EnterInt("Enter a number: ");
                if (choice == 0) { return; }
                Menu_MatchFunctionsSwitch(repo, choice);
            }
        }
        private void Menu_MatchFunctionsSwitch(PrRepository repo, int choice)
        {
            Console.Clear();
            List<Match> matches = null;
            switch (choice)
            {
                case 1:
                    
                    var teams = repo.GetAll();

                    if (teams.Count == 0)
                    {
                        Console.WriteLine("No teams in the database.");
                        ReadKey();
                        return;
                    }

                    Console.WriteLine("Goal difference (scored vs conceded):");
                    foreach (var t in teams)
                    {
                        int diff = t.Goals_scored - t.Goals_missed;
                        Console.WriteLine($"{t.Name} ({t.City}) -> Difference: {diff}");
                    }
                    break;
                case 2:
                    matches = repo.GetAllMatches();

                    if (matches.Count == 0)
                    {
                        Console.WriteLine("No matches in the database.");
                    }
                    else
                    {
                        foreach (var m in matches)
                        {
                            PrintMatches(m);
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the date to search for matches (yyyy-MM-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        matches = repo.GetAllMatches().Where(m => m.MatchDate.Date == date.Date).ToList();

                        if (matches.Count == 0)
                        {
                            Console.WriteLine("No matches on this date.");
                        }
                        else
                        {
                            foreach (var m in matches)
                            {
                                Console.WriteLine($"Team: {m.Team.Name}, Opponent: {m.Opponent.Name}, Scored: {m.GoalsScored}, Conceded: {m.GoalsConceded}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                    break;
                case 4:
                    string teamName = EnterString("Enter team name: ");
                    matches = repo.GetAllMatches().Where(m => m.Team.Name == teamName || m.Opponent.Name == teamName).ToList();

                    if (matches.Count == 0)
                    {
                        Console.WriteLine("No matches for this team.");
                    }
                    else
                    {
                        foreach (var m in matches)
                        {
                            PrintMatches(m);
                        }
                    }
                    break;
                case 5:
                    Console.WriteLine("Enter the date to search for matches (yyyy-MM-dd): ");

                    string inputDate = Console.ReadLine();
                    if (DateTime.TryParse(inputDate, out DateTime date_1))
                    {
                        matches = repo.GetAllMatches().Where(m => m.MatchDate.Date == date_1.Date).ToList();

                        if (matches.Count == 0)
                        {
                            Console.WriteLine("No matches on this date.");
                        }
                        else
                        {
                            List<string> players = new List<string>();

                            foreach (var match in matches)
                            {
                                if (match.Players != null && match.Players.Count > 0)
                                {
                                    foreach (var p in match.Players)
                                    {
                                        if (!players.Contains(p.FullName))
                                            players.Add(p.FullName);
                                    }
                                }
                            }

                            if (players.Count == 0)
                                Console.WriteLine("No goals were scored on this day.");
                            else
                            {
                                Console.WriteLine("Players who scored goals on this day:");
                                foreach (var player in players)
                                    Console.WriteLine(player);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }

                    
                    break;
                case 6:
                    Console.Clear();
                    int count = EnterInt("How many matches to add randomly? ");
                    matches = repo.MatchRandom(count);
                    foreach (var match in matches)
                    {
                        UpdateTeamStatsForMatch(match, true); 
                        repo.Update(match.Team);
                        repo.Update(match.Opponent);
                    }
                    break;
                default: Console.WriteLine("Invalid selection!"); ReadKey(); break;
            }
            ReadKey();
        }
        private void PrintMatches(Match m)
        {
            Console.WriteLine($"Match ID: {m.Id}");
            Console.WriteLine($"{m.Team.Name} vs {m.Opponent.Name}");
            Console.WriteLine($"Score: {m.GoalsScored} : {m.GoalsConceded}");
            Console.WriteLine($"Date: {m.MatchDate:dd.MM.yyyy}");

            PrintTeamScorers(m.Team, m.GoalsScored, m.Players);
            PrintTeamScorers(m.Opponent, m.GoalsConceded, m.Players);

            Console.WriteLine("-----------------------------------------");
        }
        private void PrintTeamScorers(Team team, int goals, List<Player> players)
        {
            var scorers = players?.Where(p => p.TeamId == team.Id).ToList() ?? new List<Player>();
            Console.WriteLine($"{team.Name} ({goals}):");

            if (scorers.Count == 0)
                Console.WriteLine("  — nobody scored");
            else
                scorers.ForEach(p => Console.WriteLine($"  - {p.FullName}"));
        }

        public void Menu_Add_Upd_Del_Match(PrRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose what you want to do:");
                Console.WriteLine("1 - Add match");
                Console.WriteLine("2 - Edit match");
                Console.WriteLine("3 - Delete match");
                Console.WriteLine("0 - Exit");
                int choice = EnterInt("Enter a number: ");
                if (choice == 0) return;
                Add_Upd_Del_Match_Switch(repo, choice);
            }
        }
        public void Add_Upd_Del_Match_Switch(PrRepository repo, int choice)
        {
            switch (choice)
            {
                case 1: AddMatch(repo); break;
                case 2: EditMatch(repo); break;
                case 3: DeleteMatch(repo); break;
                default: Console.WriteLine("Invalid selection!"); ReadKey(); break;
            }
        }

        public void AddMatch(PrRepository repo)
        {
            Console.Clear();
            Console.WriteLine("Adding a new match:");

            string teamName = EnterString("Enter team name: ");
            var team = repo.GetAll().FirstOrDefault(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("No such team exists in the database.");
                ReadKey();
                return;
            }

            string opponentName = EnterString("Enter opponent team name: ");
            var opponent = repo.GetAll().FirstOrDefault(t => t.Name == opponentName);
            if (opponent == null)
            {
                Console.WriteLine("Opponent team not found.");
                ReadKey();
                return;
            }

            Console.Write("Enter match date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime matchDate))
            {
                Console.WriteLine("Invalid date format.");
                ReadKey();
                return;
            }

            var existingMatch = repo.GetAllMatches().FirstOrDefault(m => m.TeamId == team.Id && m.Opponent.Id == opponent.Id && m.MatchDate.Date == matchDate.Date);
            if (existingMatch != null)
            {
                Console.WriteLine("Such a match already exists.");
                ReadKey();
                return;
            }

            int goalsScored = EnterInt($"How many goals did team {team.Name} score? ");
            int goalsConceded = EnterInt($"How many goals did team {opponent.Name} score? ");

            var playersTeam = EnterPlayersForGoals(repo, team, goalsScored);
            var playersOpponent = EnterPlayersForGoals(repo, opponent, goalsConceded);

            var allPlayers = new List<Player>();
            allPlayers.AddRange(playersTeam);
            allPlayers.AddRange(playersOpponent);

            var match = new Match
            {
                Team = team,
                TeamId = team.Id,
                Opponent = opponent,
                GoalsScored = goalsScored,
                GoalsConceded = goalsConceded,
                MatchDate = matchDate,
                Players = allPlayers
            };

            UpdateTeamStatsForMatch(match, true); 
            repo.AddMatch(match);

            Console.WriteLine("Match successfully added!");
            ReadKey();
        }
        public List<Player> EnterPlayersForGoals(PrRepository repo, Team team, int goals)
        {
            var result = new List<Player>();

            if (goals == 0)
            {
                Console.WriteLine($"Team {team.Name} did not score any goals.");
                return result;
            }

            var teamPlayers = repo.GetAllPlayers().Where(p => p.TeamId == team.Id).ToList();

            for (int i = 1; i <= goals; i++)
            {
                while (true)
                {
                    Console.Write($"Who scored the {i}-th goal for {team.Name}? ");
                    string playerName = Console.ReadLine().Trim();

                    var player = teamPlayers.FirstOrDefault(p => p.FullName == playerName);

                    if (player != null)
                    {
                        result.Add(player);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Player '{playerName}' not found in team {team.Name}. Please try again.");
                    }
                }
            }

            return result;
        }

        public Match FindMatch(PrRepository repo)
        {
            string teamName = EnterString("Enter the name of the team: ");
            var team = repo.GetAll().FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase));
            if (team == null)
            {
                Console.WriteLine("Team not found.");
                ReadKey();
                return null;
            }

            string opponentName = EnterString("Enter the name of the opponent team: ");
            var opponent = repo.GetAll().FirstOrDefault(t => t.Name.Equals(opponentName, StringComparison.OrdinalIgnoreCase));
            if (opponent == null)
            {
                Console.WriteLine("Opponent team not found.");
                ReadKey();
                return null;
            }

            Console.Write("Enter the match date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime matchDate))
            {
                Console.WriteLine("Invalid date format.");
                ReadKey();
                return null;
            }

            var match = repo.GetAllMatches()
                .FirstOrDefault(m => m.Team.Id == team.Id && m.Opponent.Id == opponent.Id && m.MatchDate.Date == matchDate.Date);

            if (match == null)
            {
                Console.WriteLine("Match not found.");
                ReadKey();
                return null;
            }

            return match;
        }
        public void EditMatch(PrRepository repo)
        {
            Console.Clear();
            Console.WriteLine("Editing match:");

            Team team, opponent;
            var match_f = FindMatch(repo);
            if (match_f == null) return;

            UpdateTeamStatsForMatch(match_f, false);

            Console.WriteLine("What do you want to change?");
            Console.WriteLine("1 - Opponent\n2 - Date\n3 - Goals scored by the first team\n4 - Goals scored by the second team\n5 - Players who scored goals\n0 - Exit");
            int choice = EnterInt("Enter: ");

            switch (choice)
            {
                case 0: return;
                case 1:
                    string newOpponentName = EnterString("New opponent: ");
                    var newOpponent = repo.GetAll().FirstOrDefault(t => t.Name == newOpponentName);
                    if (newOpponent != null)
                    {
                        var oldOpponent = match_f.Opponent;
                        if (oldOpponent != null)
                        {
                            match_f.Players.RemoveAll(p => p.TeamId == oldOpponent.Id);
                        }

                        match_f.Opponent = newOpponent;

                        if (match_f.GoalsConceded > 0)
                        {
                            Console.WriteLine($"\nNeed to re-specify who scored {match_f.GoalsConceded} goals for {newOpponent.Name}:");
                            var OpponentScorers = EnterPlayersForGoals(repo, newOpponent, match_f.GoalsConceded);

                            match_f.Players.AddRange(OpponentScorers);
                        }

                        Console.WriteLine($"Opponent changed to {newOpponent.Name}, goals updated.");
                    }
                    else
                    {
                        Console.WriteLine("Team not found, opponent not changed.");
                    }
                    break;
                case 2:
                    Console.Write("New date (yyyy-MM-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
                        match_f.MatchDate = newDate;
                    break;
                case 3:
                    int newScored = EnterInt("New goals scored by the first team: ");

                    if (newScored != match_f.GoalsScored)
                    {
                        Console.WriteLine("Number of goals changed! Need to re-specify who scored.");

                        match_f.Players.Clear();

                        match_f.GoalsScored = newScored;

                        var newTeamScorerss = EnterPlayersForGoals(repo, match_f.Team, match_f.GoalsScored);
                        var newOpponentScorerss = EnterPlayersForGoals(repo, match_f.Opponent, match_f.GoalsConceded);

                        match_f.Players.AddRange(newTeamScorerss);
                        match_f.Players.AddRange(newOpponentScorerss);
                    }
                    break;
                case 4:
                    int newConceded = EnterInt("New goals conceded by the second team: ");
                    if (newConceded != match_f.GoalsConceded)
                    {
                        Console.WriteLine("Number of goals conceded changed! Need to re-specify who scored for the opponent.");

                        match_f.Players.Clear();

                        match_f.GoalsConceded = newConceded;

                        var newTeamScorerss_2 = EnterPlayersForGoals(repo, match_f.Team, match_f.GoalsScored);
                        var newOpponentScorerss_2 = EnterPlayersForGoals(repo, match_f.Opponent, match_f.GoalsConceded);

                        match_f.Players.AddRange(newTeamScorerss_2);
                        match_f.Players.AddRange(newOpponentScorerss_2);
                    }
                    break;
                case 5:
                    Console.WriteLine($"Editing the list of players who scored in the match {match_f.Team.Name} vs {match_f.Opponent.Name}");

                    var newTeamScorers = EnterPlayersForGoals(repo, match_f.Team, match_f.GoalsScored);
                    var newOpponentScorers = EnterPlayersForGoals(repo, match_f.Opponent, match_f.GoalsConceded);

                    match_f.Players.Clear();
                    match_f.Players.AddRange(newTeamScorers);
                    match_f.Players.AddRange(newOpponentScorers);

                    Console.WriteLine("Players who scored have been successfully updated!");
                    break;
                default:
                    Console.WriteLine("Error!");
                    break;
            }

            UpdateTeamStatsForMatch(match_f, true);
            repo.UpdateMatch(match_f);

            Console.WriteLine("Match updated!");
            ReadKey();
        }
        public void DeleteMatch(PrRepository repo)
        {
            Console.Clear();
            Console.WriteLine("Deleting match:");

            var match = FindMatch(repo);
            if (match == null) return;

            UpdateTeamStatsForMatch(match, false);
            repo.RemoveMatch(match.Id);

            Console.WriteLine("Match deleted!");
            ReadKey();
        }      
        public void UpdateTeamStatsForMatch(Match match, bool isAdding)
        {
            int factor;

            if (isAdding)
            {
                factor = 1; 
            }
            else
            {
                factor = -1; 
            }

            if (match.GoalsScored > match.GoalsConceded)
            {
                match.Team.Win += 1 * factor;
                match.Opponent.Lose += 1 * factor;
            }
            else if (match.GoalsScored < match.GoalsConceded)
            {
                match.Team.Lose += 1 * factor;
                match.Opponent.Win += 1 * factor;
            }
            else
            {
                match.Team.Draw += 1 * factor;
                match.Opponent.Draw += 1 * factor;
            }

            match.Team.Goals_scored += match.GoalsScored * factor;
            match.Team.Goals_missed += match.GoalsConceded * factor;
            match.Opponent.Goals_scored += match.GoalsConceded * factor;
            match.Opponent.Goals_missed += match.GoalsScored * factor;
        }

        //----------------------------------------------------------------------
        // Модуль 6 Часть 2
        public void PrintTeam(Team t)
        {
            if (t == null)
            {
                Console.WriteLine("Team not found.");
                return;
            }

            Console.WriteLine($"Id: {t.Id}, Name: {t.Name}, City: {t.City}, Wins: {t.Win}, Losses: {t.Lose}, Draws: {t.Draw}, Scored: {t.Goals_scored}, Conceded: {t.Goals_missed}");
        }
        public void ReadKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        public string EnterString(string text)
        {
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Error! Field cannot be empty.");
            }
        }
        public int EnterInt(string text)
        {
            while (true)
            {
                Console.Write(text);
                if (int.TryParse(Console.ReadLine(), out int num))
                    return num;

                Console.WriteLine("Error! Please enter a number.");
            }
        }


        public void Menu_ShowAllTeams(PrRepository repo)
        {
            Console.Clear();
            var teams = repo.GetAll();

            if (teams.Count == 0)
                Console.WriteLine("No teams found.");
            else
            {
                Console.WriteLine("List of all teams:");
                foreach (var t in teams)
                    PrintTeam(t);
            }
            ReadKey();
        }

        public void Menu_AddTeam(PrRepository repo)
        {
            Console.Clear();
            string name = EnterString("Enter team name: ");
            string city = EnterString("Enter city: ");

            if (repo.GetAll().Any(t => t.Name == name && t.City == city))
            {
                Console.WriteLine("Such a team already exists.");
                ReadKey();
                return;
            }

            int win = EnterInt("Enter number of wins: ");
            int lose = EnterInt("Enter number of losses: ");
            int draw = EnterInt("Enter number of draws: ");
            int goalsScored = EnterInt("Enter number of goals scored: ");
            int goalsMissed = EnterInt("Enter number of goals conceded: ");

            repo.Add(new Team
            {
                Name = name,
                City = city,
                Win = win,
                Lose = lose,
                Draw = draw,
                Goals_scored = goalsScored,
                Goals_missed = goalsMissed
            });

            Console.WriteLine("Team successfully added!");
            ReadKey();
        }

        public void Menu_UpdateTeam(PrRepository repo)
        {
                Console.Clear();

                Console.WriteLine("Editing team data");
                string name = EnterString("Enter team name: ");
                string city = EnterString("Enter city: ");

                var team = repo.GetAll().FirstOrDefault(t => t.Name == name && t.City == city);

                if (team == null)
                {
                    Console.WriteLine("Team not found.");
                    ReadKey();
                    return;
                }

                Console.WriteLine("Current data:");
                PrintTeam(team);

                Console.WriteLine("\nWhat do you want to change?");
                Console.WriteLine("1 - Name\n2 - City\n3 - Wins\n4 - Losses\n5 - Draws\n6 - Goals Scored\n7 - Goals Missed\n0 - Exit");
                int choice = EnterInt("Enter: ");

                UpdateTeamSwitch(team, choice);
                repo.Update(team);

                Console.WriteLine("Team updated!");
                PrintTeam(team);
                ReadKey();
        }
        private void UpdateTeamSwitch(Team team, int choice)
        {
            switch (choice)
            {
                case 1: team.Name = EnterString("New name: "); break;
                case 2: team.City = EnterString("New city: "); break;
                case 3: team.Win = EnterInt("New wins: "); break;
                case 4: team.Lose = EnterInt("New losses: "); break;
                case 5: team.Draw = EnterInt("New draws: "); break;
                case 6: team.Goals_scored = EnterInt("New goals scored: "); break;
                case 7: team.Goals_missed = EnterInt("New goals missed: "); break;
                case 0: return;
                default: Console.WriteLine("Error!"); break;
            }
        }

        public void Menu_DeleteTeam(PrRepository repo)
        {
            Console.Clear();
            string name = EnterString("Enter team name: ");
            string city = EnterString("Enter city: ");

            var team = repo.GetAll().FirstOrDefault(t => t.Name == name && t.City == city);

            if (team == null)
            {
                Console.WriteLine("Team not found.");
                ReadKey();
                return;
            }

            PrintTeam(team);
            Console.Write("Delete? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                repo.Remove(team.Id);
                Console.WriteLine("Team deleted!");
            }
            else
                Console.WriteLine("Deletion canceled.");

            ReadKey();
        }

        public void Menu_SearchTeam(PrRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Search by:\n1 - Name\n2 - City\n3 - Name + City\n0 - Exit");
                int choice = EnterInt("Enter: ");
                if (choice == 0) return;
                SearchTeamSwitch(repo.GetAll(), choice);
                ReadKey();
            }
        }
        private void SearchTeamSwitch(List<Team> teams, int choice)
        {
            
                switch (choice)
                {
                    case 1:
                        var name = EnterString("Name: ");
                        teams.Where(t => t.Name == name).ToList().ForEach(PrintTeam);
                        break;
                    case 2:
                        var city = EnterString("City: ");
                        teams.Where(t => t.City == city).ToList().ForEach(PrintTeam);
                        break;
                    case 3:
                        name = EnterString("Name: ");
                        city = EnterString("City: ");
                        teams.Where(t => t.Name == name && t.City == city).ToList().ForEach(PrintTeam);
                        break;
                    default: Console.WriteLine("Error!"); break;
                }
        }

        public void Menu_SelectTeam(PrRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - max Wins\n2 - max Losses\n3 - max Draws\n4 - max Goals Scored\n5 - max Goals Missed\n0 - Exit");
                int choice = EnterInt("Enter: ");
                if(choice == 0) return;
                SelectTeamSwitch(repo.GetAll(), choice);      
                ReadKey();
            }
        }
        private void SelectTeamSwitch(List<Team> teams, int choice)
        {           
                Team result = null;
                switch (choice)
                {
                    case 1: result = teams.OrderByDescending(t => t.Win).FirstOrDefault(); break;
                    case 2: result = teams.OrderByDescending(t => t.Lose).FirstOrDefault(); break;
                    case 3: result = teams.OrderByDescending(t => t.Draw).FirstOrDefault(); break;
                    case 4: result = teams.OrderByDescending(t => t.Goals_scored).FirstOrDefault(); break;
                    case 5: result = teams.OrderByDescending(t => t.Goals_missed).FirstOrDefault(); break;
                    default: Console.WriteLine("Error!"); return;
                }
                Console.WriteLine("Result:");
                PrintTeam(result);
            
        }
    }
}
