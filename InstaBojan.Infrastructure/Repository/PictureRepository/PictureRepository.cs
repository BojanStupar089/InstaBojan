using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.PictureRepository
{
    public class PictureRepository : IPictureRepository
    {
        public string UploadPicture(IFormFile picture)
        {
            if (picture == null || picture.Length == 0)
            {
                return null;
            }

            var picturePath = Path.Combine("C:\\Users\\Panonit\\Desktop", picture.FileName);

            using (var stream = new FileStream(picturePath, FileMode.Create))
            { 
            
                picture.CopyTo(stream);
            }

            return picturePath;
        }
    }
}
