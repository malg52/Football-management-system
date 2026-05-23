using System.Data;

public class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Country { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();
    public List<Review> Reviews { get; set; } = new List<Review>();
}

public class Actor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();
}

public class Role
{
    public int Id { get; set; }
    public string Character { get; set; }
    public string Type { get; set; }

    public int FilmId { get; set; }
    public Film Film { get; set; }

    public int ActorId { get; set; }
    public Actor Actor { get; set; }  
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; } 
    public string Email { get; set; }

    public List<Review> Reviews { get; set; } = new List<Review>();
}

public class Review
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public Film Film { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
}