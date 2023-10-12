namespace PostIt_Sharp.Controllers;

[ApiController]
[Route("api/collaborators")]
public class CollaboratorsController : ControllerBase
{
    private readonly CollaboratorsService _collabsService;
    private readonly Auth0Provider _auth0;

    public CollaboratorsController(CollaboratorsService collabsService, Auth0Provider auth0)
    {
        _collabsService = collabsService;
        _auth0 = auth0;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Collaborator>> CreateCollab([FromBody] Collaborator collabData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            collabData.AccountId = userInfo.Id;
            Collaborator newCollab = _collabsService.CreateCollab(collabData);
            return newCollab;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{collabId}")]
    public async Task<ActionResult<string>> DeleteCollab(int collabId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            _collabsService.DeleteCollab(collabId, userInfo.Id);
            string message = "Successfully removed collaborator.";
            return message;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}