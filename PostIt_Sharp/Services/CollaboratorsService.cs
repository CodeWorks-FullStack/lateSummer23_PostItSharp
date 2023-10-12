



namespace PostIt_Sharp.Services;

public class CollaboratorsService
{
    private readonly CollaboratorsRepository _repo;

    public CollaboratorsService(CollaboratorsRepository repo)
    {
        _repo = repo;
    }

    internal Collaborator CreateCollab(Collaborator collabData)
    {
        Collaborator newCollab = _repo.CreateCollab(collabData);
        return newCollab;
    }

    internal void DeleteCollab(int collabId, string userId)
    {
        Collaborator foundCollab = _repo.GetById(collabId);
        if (foundCollab == null) throw new Exception("Invalid collaborator id");
        if (foundCollab.AccountId != userId) throw new Exception("Unauthorized to remove");
        int rows = _repo.DeleteCollab(collabId);
        if (rows > 1) throw new Exception("Something went wrong");
        if (rows < 1) throw new Exception("Something went way wrong");
    }

    internal List<AlbumCollaboratorViewModel> GetAlbumsByAccount(string accountId)
    {
        List<AlbumCollaboratorViewModel> myAlbums = _repo.GetAlbumsByAccount(accountId);
        return myAlbums;
    }

    internal List<AccountCollaboratorViewModel> GetCollabsByAlbumId(int albumId)
    {
        List<AccountCollaboratorViewModel> collabs = _repo.GetCollabsByAlbumId(albumId);
        return collabs;
    }
}