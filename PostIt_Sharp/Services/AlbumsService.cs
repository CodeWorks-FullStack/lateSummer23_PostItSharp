

namespace PostIt_Sharp.Services;

public class AlbumsService
{
    private readonly AlbumsRepository _repo;

    public AlbumsService(AlbumsRepository repo)
    {
        _repo = repo;
    }



    internal Album Create(Album albumData)
    {
        Album newAlbum = _repo.Create(albumData);
        return newAlbum;
    }

    internal List<Album> Get()
    {
        List<Album> albums = _repo.Get();
        return albums;
    }

    internal Album Get(int albumId)
    {
        Album foundAlbum = _repo.Get(albumId);
        if (foundAlbum == null) throw new Exception($"Unable to find album at {albumId}");
        return foundAlbum;
    }

    internal Album ArchiveAlbum(int albumId, string userId)
    {
        Album album = this.Get(albumId);
        if (album.CreatorId != userId) throw new Exception("Unauthorized");
        album.Archived = !album.Archived;
        _repo.Edit(album);
        return album;
    }

    // internal List<Picture> GetPicturesByAlbumId()
    // {
    //     _pictureService.
    // }
}