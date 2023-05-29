using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.IFileStorageService
{
    public class LocalFileStorageRepository : IFileStorageRepository
    {  public string SaveFile(IFormFile file)
        {
           if(file == null || file.Length==0) 
            {
                throw new ArgumentException("Invalid file");
            }

           var filePath=Path.Combine("C:\\Users\\Panonit\\Desktop\\pictures", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
