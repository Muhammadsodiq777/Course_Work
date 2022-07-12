using Demo_Project.Data;
using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<FilesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public FilesController(IWebHostEnvironment webHostEnvironment, 
        ILogger<FilesController> logger, IUnitOfWork unitOfWork)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
     {
        if(files.Count == 0)
        {
            _logger.LogError($"Invalid Upload attemp in {nameof(UploadFiles)}");
            return BadRequest("Submitted data is invalid");
        }

        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");

        foreach (var file in files)
        {
            //System.Guid guid = System.Guid.NewGuid();
            //var fileName = file.FileName + guid.ToString();

            var fileName = file.FileName + " " + Guid.NewGuid().ToString();
            string filePath = Path.Combine(directoryPath, fileName);

            var fileEntity = new FilesEntity()
            {
                OriginalName = file.FileName,
                FileURL = filePath
            };

            await _unitOfWork.Files.Insert(fileEntity);
            await _unitOfWork.SaveAsync();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);   
            }
        }
        _logger.LogError($"File upload is success {nameof(UploadFiles)}");
        return Ok("Upload Success");

    } 

}
