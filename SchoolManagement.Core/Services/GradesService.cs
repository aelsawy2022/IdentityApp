using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class GradesService : IGradesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GradesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(GradeModel model)
        {
            Grade grade = _mapper.Map<Grade>(model);

            grade.CreationDate = DateTime.Now;
            grade.Id = Guid.NewGuid();
            grade.School = await _unitOfWork.SchoolRepository.GetByIDAsync(grade.School.Id);

            await _unitOfWork.GradeRepository.AddAsync(grade);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Grade grade = await _unitOfWork.GradeRepository.GetByIDAsync(arguments[0]);

            if (grade == null) return false;

            await _unitOfWork.GradeRepository.DeleteAsync(grade);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(GradeModel model)
        {
            Grade grade = _mapper.Map<Grade>(model);

            grade.School = await _unitOfWork.SchoolRepository.GetByIDAsync(grade.School.Id);

            await _unitOfWork.GradeRepository.UpdateAsync(grade);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<GradeViewModel> Initiate(params object[] arguments)
        {
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grades = _mapper.Map<List<GradeModel>>(await _unitOfWork.GradeRepository.GetAsync(x => x.School.Id == (Guid)arguments[0], o => o.OrderBy(g => g.Name)) as List<Grade>);
            gradeViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]));

            return gradeViewModel;
        }

        public async Task<GradeViewModel> InitiateCreate(params object[] arguments)
        {
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grade = new GradeModel();
            gradeViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]));

            return gradeViewModel;
        }

        public async Task<GradeViewModel> InitiateEdit(params object[] arguments)
        {
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grade = _mapper.Map<GradeModel>(await _unitOfWork.GradeRepository.GetByIDAsync(arguments[0]));
            gradeViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[1]));

            return gradeViewModel;
        }
    }
}
