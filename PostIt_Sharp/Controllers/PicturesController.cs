namespace PostIt_Sharp.Controllers;

[ApiController]
[Route("api/pictures")]
public class PicturesController : ControllerBase
{
    private readonly PicturesService _picturesService;
    private readonly Auth0Provider _auth0;

    public PicturesController(PicturesService picturesService, Auth0Provider auth0)
    {
        _picturesService = picturesService;
        _auth0 = auth0;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            pictureData.CreatorId = userInfo.Id;
            Picture newPicture = _picturesService.CreatePicture(pictureData);
            // newPicture.Creator = userInfo;
            return newPicture;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}