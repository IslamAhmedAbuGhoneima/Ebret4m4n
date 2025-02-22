using Azure;
using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Repository.Repositories;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IO; 


namespace Ebret4m4n.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddChild([FromBody]AddChildDto dto)
        {
           // var response = new ServiceResponse<Child>();
           
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var child = new Child
            {
                Id =dto.Id,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Weight = dto.Weight,
                Gender = dto.Gender,
                UserId = dto.ParentId
            };

            if(dto.healthReportFiles is not null)
            {
                foreach (var images in dto.healthReportFiles)
                {
                    var image = new HealthReportFile
                    {
                        FilePath = images.FileName,
                        ChildId = dto.Id
                    };
                    child.HealthReportFiles.Add(image);
                }
                List<HealthReportFile> imagesSaved=SaveImages(dto.healthReportFiles);
            }

            if (dto.vaccines is not null)
            {
                foreach (var vaccine in dto.vaccines)
                {
                    var v = new Vaccine
                    {
                        ChildId = dto.Id,
                        Name = vaccine.Name,
                        ChildAge = vaccine.ChildAge
                    };
                    child.Vaccines.Add(v);
                }
            }
             _unitOfWork.ChildRepo.Add(child);
            await _unitOfWork.SaveAsync();

            //response.Data = child;
            //response.Success = true;
            //response.Message = "Child added successfully with image";

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChild(string Id,[FromBody] UpdateChildDto dto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var child = await _unitOfWork.ChildRepo.FindAsync(e=>e.Id==Id,true);
            if (child is null)
            {
                return NotFound();
            }
             child.Name = dto.Name;
             child.Weight = dto.Weight;
             if (!string.IsNullOrEmpty(dto.PatientHistory))
                        child.PatientHistory = dto.PatientHistory;
             
            //Delet files from server and database
            if (dto.deleteImagePaths != null)
            {
                child.HealthReportFiles = child.HealthReportFiles?.Where(h => !dto.deleteImagePaths.Contains(h.FilePath)).ToList();

                foreach (var path in dto.deleteImagePaths)
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);
                }
            }
            //Add new images in server
            if (dto.imageFiles is not null)
            {
                foreach (var images in dto.imageFiles)
                {
                    var image = new HealthReportFile
                    {
                        FilePath = images.FileName,
                        ChildId = Id
                    };
                    child.HealthReportFiles.Add(image);
                }
                List<HealthReportFile> imagesSaved = SaveImages(dto.imageFiles);
            }

            _unitOfWork.ChildRepo.Update(child);
            await _unitOfWork.SaveAsync();
            return Ok();

        }
        private List<HealthReportFile> SaveImages(ICollection<IFormFile> imageFiles)
        {
            var savedFiles = new List<HealthReportFile>();
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            foreach (var imageFile in imageFiles)
            {
                if (imageFile.FileName.Length > 0)
                {
                    string FileName = imageFile.FileName;
                    string FilePath = Guid.NewGuid().ToString() + Path.GetExtension(FileName);
                    string imagePath = Path.Combine(uploadsFolder, FileName);
                    System.IO.File.Copy(FilePath, imagePath);

                    savedFiles.Add(new HealthReportFile { FilePath = imagePath, UploadedOn = DateTime.Now });
                }
            }

            return savedFiles;
        }
        
    }
}
