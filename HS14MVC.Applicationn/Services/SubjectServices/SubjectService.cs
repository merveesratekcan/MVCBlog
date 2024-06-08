using HS14MVC.Applicationn.DTOs.SubjectDTOs;
using HS14MVC.Domain.Entities;
using HS14MVC.Domain.Utilities.Concretes;
using HS14MVC.Domain.Utilities.Interfaces;
using HS14MVC.Infrastructure.Repositories.SubjectRepositories;
using Mapster;


namespace HS14MVC.Applicationn.Services.SubjectServices
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IDataResult<SubjectDTO>> AddAsync(SubjectCreateDTO subjectCreateDTO)
        {
            if (await _subjectRepository.AnyAsync(x=>x.Name.ToLower() == subjectCreateDTO.Name.ToLower()))
            {
                return new ErrorDataResult<SubjectDTO>("Konu Sistemde Kayıtlı");
            }
            var newSubject = subjectCreateDTO.Adapt<Subject>();
            await _subjectRepository.AddAsync(newSubject);
            await _subjectRepository.SaveChangesAsync();
            return new SuccessDataResult<SubjectDTO>(newSubject.Adapt<SubjectDTO>(), "Konu Ekleme Başarılı");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
            {
                return new ErrorResult("Silinecek Konu Bulunamadı");
            }
            await _subjectRepository.DeleteAsync(subject);
            await _subjectRepository.SaveChangesAsync();
            return new SuccessResult("Konu Silme Başarılı");
        }

        public async Task<IDataResult<List<SubjectListDTO>>> GetAllAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            if (subjects.Count() <= 0)
            {
                return new ErrorDataResult<List<SubjectListDTO>>(subjects.Adapt<List<SubjectListDTO>>(), "Listelenecek Konu Bulunmamakta");
            }

            return new SuccessDataResult<List<SubjectListDTO>>(subjects.Adapt<List<SubjectListDTO>>(), "Konu Listeleme Başarılı");
        }

        public async Task<IDataResult<SubjectDTO>> GetByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
            {
                return new ErrorDataResult<SubjectDTO>("Getirilecek Konu Bulunamadı");
            }
            return new SuccessDataResult<SubjectDTO>(subject.Adapt<SubjectDTO>(), "Konu Getirme Başarılı");
        }

        public async Task<IDataResult<SubjectDTO>> UpdateAsync(SubjectUpdateDTO subjectUpdateDTO)
        {
           var subject = await _subjectRepository.GetByIdAsync(subjectUpdateDTO.Id);
            if (subject is null)
            {
                return new ErrorDataResult<SubjectDTO>("Güncellenecek Konu Bulunamadı");
            }
            subject.Name = subjectUpdateDTO.Name;
            await _subjectRepository.UpdateAsync(subject);
            await _subjectRepository.SaveChangesAsync();
            return new SuccessDataResult<SubjectDTO>(subject.Adapt<SubjectDTO>(), "Konu Güncelleme Başarılı");
        }
    }
}
