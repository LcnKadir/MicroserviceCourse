using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using System.Threading;
using FreeCourse.Shared.BaseController;
using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.DTOs;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken) //CancelletionToken will cancel the saving as a result of the user closing the tab during the photo saving process.
                                                                                                         //CancelletionToken, fotoğraf kaydetme işlemi sırasında kullanıcının sekmeyi kapaması sonucunda; kaydetmeyi iptal edecektir.
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));

            }

            return CreateActionResultInstance (Response<PhotoDto>.Fail("photo is empty", 400));
        }
    }
}
