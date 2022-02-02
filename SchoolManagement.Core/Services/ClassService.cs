using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(ClassModel model)
        {
            Class _class = _mapper.Map<Class>(model);

            _class.CreationDate = DateTime.Now;
            _class.Id = Guid.NewGuid();
            _class.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(_class.Grade.Id);

            await _unitOfWork.ClassRepository.AddAsync(_class);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Class _class = await _unitOfWork.ClassRepository.GetByIDAsync(arguments[0]);

            if (_class == null) return false;

            await _unitOfWork.ClassRepository.DeleteAsync(_class);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(ClassModel model)
        {
            Class _class = _mapper.Map<Class>(model);

            _class.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(_class.Grade.Id);

            await _unitOfWork.ClassRepository.UpdateAsync(_class);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<ClassVM> Initiate(params object[] arguments)
        {
            ClassVM classViewModel = new ClassVM();
            classViewModel.Classes = _mapper.Map<List<ClassModel>>(await _unitOfWork.ClassRepository.GetAsync(c => c.Grade.Id == (Guid)arguments[0], o => o.OrderBy(c => c.CreationDate), "Grade") as List<Class>);
            classViewModel.Grade = _mapper.Map<GradeModel>(await _unitOfWork.GradeRepository.GetByIDAsync(arguments[0]));
            classViewModel.SchoolId = (Guid)arguments[1];

            return classViewModel;
        }

        public async Task<ClassVM> InitiateCreate(params object[] arguments)
        {
            ClassVM classViewModel = new ClassVM();
            classViewModel.Class = new ClassModel();
            classViewModel.Grade = _mapper.Map<GradeModel>(await _unitOfWork.GradeRepository.GetByIDAsync(arguments[0]));
            classViewModel.SchoolId = (Guid)arguments[1];

            return classViewModel;
        }

        public async Task<ClassVM> InitiateEdit(params object[] arguments)
        {
            ClassVM classViewModel = new ClassVM();
            classViewModel.Class = _mapper.Map<ClassModel>(await _unitOfWork.ClassRepository.GetByIDAsync(arguments[0]));
            classViewModel.Grade = _mapper.Map<GradeModel>(await _unitOfWork.GradeRepository.GetByIDAsync(arguments[1]));
            classViewModel.SchoolId = (Guid)arguments[2];

            return classViewModel;
        }
    }
}
