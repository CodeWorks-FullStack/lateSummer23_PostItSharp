namespace PostIt_Sharp.Models;

public class Account
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }
}


public class AccountCollaboratorViewModel : Account
{
  public int CollaboratorId { get; set; }
  public int AlbumId { get; set; }
}



