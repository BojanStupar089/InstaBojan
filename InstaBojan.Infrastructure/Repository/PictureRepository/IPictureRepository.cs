using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.PictureRepository
{
    public interface IPictureRepository
    {
        string UploadPicture(IFormFile picture);
    }
}
