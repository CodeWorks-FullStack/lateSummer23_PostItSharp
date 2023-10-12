

namespace PostIt_Sharp.Repositories;

public class PicturesRepository
{
    private readonly IDbConnection _db;

    public PicturesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Picture CreatePicture(Picture pictureData)
    {
        string sql = @"
       INSERT INTO pictures
       (imgUrl, creatorId, albumId)
       VALUES
       (@imgUrl, @creatorId, @albumId);

SELECT
pic.*,
act.*
FROM pictures pic
JOIN accounts act ON act.id = @creatorId
WHERE pic.id = LAST_INSERT_ID()
       ;";
        Picture newPicture = _db.Query<Picture, Account, Picture>(sql, (picture, account) =>
        {
            picture.Creator = account;
            return picture;
        }, pictureData).FirstOrDefault();
        return newPicture;
    }

    internal List<Picture> GetPicturesByAlbumId(int albumId)
    {
        string sql = @"
        SELECT
        pic.*,
        act.*
        FROM pictures pic
        JOIN accounts act ON act.id = pic.creatorId
        WHERE albumId = @albumId
        ;";
        List<Picture> pictures = _db.Query<Picture, Account, Picture>(sql, (picture, account) =>
        {
            picture.Creator = account;
            return picture;
        }, new { albumId }).ToList();
        return pictures;
    }
}