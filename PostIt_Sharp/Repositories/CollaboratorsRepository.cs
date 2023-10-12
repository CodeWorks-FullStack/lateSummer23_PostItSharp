



namespace PostIt_Sharp.Repositories;

public class CollaboratorsRepository
{
    private readonly IDbConnection _db;

    public CollaboratorsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Collaborator CreateCollab(Collaborator collabData)
    {
        string sql = @"
       INSERT INTO collaborators
       (albumId, accountId)
       VALUES
       (@albumId, @accountId);
SELECT LAST_INSERT_ID()
       ;";
        //    NOTE db.Query will return entire rows, ExecuteScalar will return a single cell
        // NOTE in this case, we are selecting and returning just the cell of the last insert id
        int lastInsertId = _db.ExecuteScalar<int>(sql, collabData); // returns as an int bc that is the data-type of the primary key in the table
        collabData.Id = lastInsertId; // cast the last insert id from the table to finish building out this new instance of our model
        return collabData;
    }

    internal int DeleteCollab(int collabId)
    {
        string sql = @"
       DELETE FROM collaborators
       WHERE id = @collabId
              LIMIT 1
       ;";
        int rows = _db.Execute(sql, new { collabId });
        return rows;
    }

    internal List<AlbumCollaboratorViewModel> GetAlbumsByAccount(string accountId)
    {
        string sql = @"
        SELECT
        collab.*,
        alb.*
        FROM collaborators collab
        JOIN albums alb ON alb.id = collab.albumId
        WHERE collab.accountId = @accountId
        ;";
        List<AlbumCollaboratorViewModel> myAlbums = _db.Query<Collaborator, AlbumCollaboratorViewModel, AlbumCollaboratorViewModel>(sql, (collab, album) =>
        {
            album.CollaboratorId = collab.Id;
            album.AccountId = collab.AccountId;
            return album;
        }, new { accountId }).ToList();
        return myAlbums;
    }

    internal Collaborator GetById(int collabId)
    {
        string sql = @"
        SELECT
        *
        FROM collaborators
        WHERE id = @collabId
        ;";
        Collaborator collab = _db.Query<Collaborator>(sql, new { collabId }).FirstOrDefault();
        return collab;
    }

    internal List<AccountCollaboratorViewModel> GetCollabsByAlbumId(int albumId)
    {
        string sql = @"
        SELECT
        collab.*,
        act.*
        FROM collaborators collab
        JOIN accounts act ON act.id = collab.accountId
        WHERE collab.albumId = @albumId
        ;";
        List<AccountCollaboratorViewModel> collabs = _db.Query<Collaborator, AccountCollaboratorViewModel, AccountCollaboratorViewModel>(sql, (collab, accountCollab) =>
        {
            accountCollab.CollaboratorId = collab.Id;
            accountCollab.AlbumId = collab.AlbumId;
            return accountCollab;
        }, new { albumId }).ToList();
        return collabs;
    }
}