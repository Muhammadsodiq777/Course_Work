namespace Demo_Project.Data;

public class InAppFileSaver
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public InAppFileSaver(IWebHostEnvironment webHostEnvironment)
    {
        this._webHostEnvironment = webHostEnvironment;
    }

    public async Task SaveFileAsync(IFormFile file)
    {
        var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string route = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");

        if (!Directory.Exists(route))
            Directory.CreateDirectory(route);
        
        string fileRoute = Path.Combine(route, file.FileName);

        using (FileStream fileStream = File.Create(fileRoute))
        {
            await file.OpenReadStream().CopyToAsync(fileStream);
        }

    }

    public async Task SaveFilesAsync(List<IFormFile> files)
    {
        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");

        foreach (var file in files)
        {
            System.Guid guid = System.Guid.NewGuid();
            var fileName = file.FileName + guid.ToString();
            string filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

    }
}
