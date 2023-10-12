

namespace PostIt_Sharp.Services;

public class PicturesService
{
    private readonly PicturesRepository _repo;

    public PicturesService(PicturesRepository repo)
    {
        _repo = repo;
    }

    internal Picture CreatePicture(Picture pictureData)
    {
        Picture newPicture = _repo.CreatePicture(pictureData);
        return newPicture;
    }

    internal List<Picture> GetPicturesByAlbumId(int albumId)
    {
        List<Picture> pictures = _repo.GetPicturesByAlbumId(albumId);
        return pictures;
    }
}