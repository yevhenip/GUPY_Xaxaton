using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Base
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}