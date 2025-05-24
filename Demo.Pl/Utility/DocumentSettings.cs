namespace Demo.Pl.Utility
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            // C:\Users\OWNER\Downloads\C#\DemoMVC\Demo.Pl\wwwroot\Files\Images\
            var folderPath =Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files" , folderName);

            var fileName = $"{Guid.NewGuid()}-{file?.FileName}";

            var filePath=Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath , FileMode.Create))
            {

                file.CopyTo(fileStream);
            };
            return fileName;
        }

        public static void DeleteFile(string fileName , string folderName)
        {
            var filePath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName ,fileName);

            if (File.Exists(filePath) )
            File.Delete(filePath);

            
        }
    }
}
