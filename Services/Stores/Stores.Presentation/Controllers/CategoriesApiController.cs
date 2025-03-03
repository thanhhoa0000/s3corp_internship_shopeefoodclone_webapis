namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
    [ApiController]
    [Route("api/v{version:apiVersion}/stores")]
    [ApiVersion(1)]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoriesApiController> _logger;
        private Response _response;

        public CategoriesApiController(ICategoryService service, ILogger<CategoriesApiController> logger)
        {
            _service = service;
            _logger = logger;
            _response = new Response();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize = 0, [FromQuery] int pageNumber = 1)
        {
            try
            {
                _logger.LogInformation("Getting the categories...");
                
                _response = await _service.GetAllAsync(pageSize:  pageSize, pageNumber: pageNumber);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when getting the categories!");
            }
        }
        
        [HttpGet("{cateId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid cateId)
        {
            try
            {
                _logger.LogInformation("Getting the category...");
                
                _response = await _service.GetAsync(cateId);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when getting the category!");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            try
            {
                _logger.LogInformation($"Creating category {categoryDto.Id}");
                
                _response = await _service.CreateAsync(categoryDto);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when creating the category!");
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDto category)
        {
            try
            {
                _logger.LogInformation($"Updating category {category.Id}");
                
                _response = await _service.UpdateAsync(category);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when updating the category!");
            }
        }
        
        [HttpDelete("{cateId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid cateId)
        {
            try
            {
                _logger.LogInformation($"Deleting category {cateId}");
                
                _response = await _service.RemoveAsync(cateId);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when deleting the category!");
            }
        }
    }
}
