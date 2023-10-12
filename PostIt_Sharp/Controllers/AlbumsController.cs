namespace PostIt_Sharp.Controllers;

// [Authorize] NOTE if we wanted to 'lock down' the entire endpoint
[ApiController]
[Route("api/albums")]
public class AlbumsController : ControllerBase
{
    private readonly AlbumsService _albumsService;
    private readonly PicturesService _picturesService;
    private readonly CollaboratorsService _collabsService;
    private readonly Auth0Provider _auth0;

    public AlbumsController(AlbumsService albumsService, Auth0Provider auth0, PicturesService picturesService, CollaboratorsService collabsService)
    {
        _albumsService = albumsService;
        _auth0 = auth0;
        _picturesService = picturesService;
        _collabsService = collabsService;
    }

    [Authorize] // says you MUST be authorized before making this request...applies to req directly below
    [HttpPost]
    // NOTE when using async in C#...we must also use Task
    // NOTE creates a new thread for our async req to run in and ends the thread when it returns the promise
    public async Task<ActionResult<Album>> Create([FromBody] Album albumData)
    {
        try
        {
            // NOTE grab the userInfo from Auth0 using the bearer token of the req.
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            // NOTE assign the creatorId to the id of the authorized user
            albumData.CreatorId = userInfo.Id;
            Album newAlbum = _albumsService.Create(albumData);
            return newAlbum;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet]
    public ActionResult<List<Album>> Get()
    {
        try
        {
            List<Album> albums = _albumsService.Get();
            return albums;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}")]
    public ActionResult<Album> GetById(int albumId)
    {
        try
        {
            Album album = _albumsService.Get(albumId);
            return album;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{albumId}/pictures")]
    public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
    {
        try
        {
            List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
            return pictures;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{albumId}/collaborators")]
    public ActionResult<List<AccountCollaboratorViewModel>> GetCollabsByAlbumId(int albumId)
    {
        try
        {
            List<AccountCollaboratorViewModel> collabs = _collabsService.GetCollabsByAlbumId(albumId);
            return collabs;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [Authorize]
    [HttpDelete("{albumId}")]
    public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            Album album = _albumsService.ArchiveAlbum(albumId, userInfo.Id);
            return album;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



}