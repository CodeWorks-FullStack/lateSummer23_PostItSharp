


namespace PostIt_Sharp.Repositories;

public class AlbumsRepository
{
    private readonly IDbConnection _db;

    public AlbumsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Album Create(Album albumData)
    {
        string sql = @"
        INSERT INTO albums
        (title, category, coverImg, creatorId)
        VALUES
        (@title, @category, @coverImg, @creatorId);

        SELECT
        act.*,
        alb.*
        FROM albums alb
        JOIN accounts act ON act.id = alb.creatorId
        WHERE alb.id = LAST_INSERT_ID()
        ;";
        // ⬆NOTE ⬆️⬆️ we can switch up the order of our select clause as long as the order of the return types in our dapper correspond correctly
        Album newAlbum = _db.Query<Account, Album, Album>(sql, (account, album) =>
        {
            album.Creator = account;
            return album;
        }, albumData).FirstOrDefault();
        return newAlbum;
    }

    internal List<Album> Get()
    {
        string sql = @"
     SELECT
     alb.*,
     act.*
     FROM albums alb
     JOIN accounts act ON act.id = alb.creatorId 
       ;";
        //    NOTE.....................🔽first return type matches first line of select
        //    NOTE.............................🔽second return type matches second line of select
        // NOTE........................................🔽 third argument will always match return type of the method itself
        //    NOTE....................................................🔽🔽banana words for what we alias our returned data as to reference later
        List<Album> albums = _db.Query<Album, Account, Album>(sql, (album, account) =>
        {
            // NOTE this is creating the album object we want to retrun from the method out of the data returned from the tables
            album.Creator = account; // take the account dapper returned from the table and assign to creator property on the album
            return album; // return the album... this final return needs to match the final return type in our dapper statement
        }).ToList();
        return albums;
    }

    internal Album Get(int albumId)
    {
        string sql = @"
       SELECT
       alb.*,
       act.*
       FROM albums alb
       JOIN accounts act ON alb.creatorId = act.id
       WHERE alb.id = @albumId
       ;";
        Album foundAlbum = _db.Query<Album, Account, Album>(sql, (album, creator) =>
        {
            album.Creator = creator;
            return album;
        }, new { albumId }).FirstOrDefault();
        return foundAlbum;
    }

    internal void Edit(Album album)
    {
        string sql = @"
       UPDATE albums
       SET
       title = @title,
       category = @category,
       coverImg = @coverImg,
       archived = @archived
       WHERE id = @id
       ;";
        _db.Execute(sql, album);
    }
}