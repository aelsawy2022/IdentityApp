using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class ClassUserService : IClassUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> Create(ClassUsersModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(ClassUsersModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ClassUserVM> Initiate(params object[] arguments)
        {
            ClassUserVM classUserViewModel = new ClassUserVM();
            classUserViewModel.usersFilter = new UsersFilter();

            int currentPage = (int)arguments[3];
            int maxRows = (int)arguments[4];

            var classUsers = (await _unitOfWork.ClassUserRepository.GetAsync(cu => cu.ClassId == (Guid)arguments[2],
                                                o => o.OrderBy(cu => cu.UserId), "User,UserType,Season") as List<ClassUser>).ToList();
            classUserViewModel.ClassUsers = _mapper.Map<List<ClassUsersModel>>(classUsers);

            var allUsers = await _unitOfWork.UserRepository.GetAsync(GenerateAllUsersExpression((Guid)arguments[2]), o => o.OrderBy(u => u.Name), "ClassUsers") as List<User>;
            classUserViewModel.AllUsers = _mapper.Map<List<UsersModel>>(allUsers);

            classUserViewModel.Types = _mapper.Map<List<UserTypeModel>>(await _unitOfWork.UserTypeRepository.GetAllAsync(o => o.OrderBy(ut => ut.Name)) as List<UserType>);

            classUserViewModel.Class = _mapper.Map<ClassModel>(await _unitOfWork.ClassRepository.GetByIDAsync(arguments[2]));
            classUserViewModel.SchoolId = (Guid)arguments[1];
            classUserViewModel.GradeId = (Guid)arguments[0];

            return classUserViewModel;
        }

        private Expression<Func<User, bool>> GenerateAllUsersExpression(Guid classId)
        {
            Expression<Func<User, bool>> _Expression = (
                x => (x.ClassUsers == null || x.ClassUsers.Count == 0)
                || (!x.ClassUsers.Any(cu => cu.ClassId == classId))
            );
            return _Expression;
        }

        public Task<ClassUserVM> InitiateCreate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public Task<ClassUserVM> InitiateEdit(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<ClassUserServiceResponse> AddUsers(ClassUserVM viewModel)
        {
            Season schoolCurrentSeason = await _unitOfWork.SeasonRepository.GetOneAsync(s => s.Current && s.School.Id == viewModel.SchoolId);

            if (schoolCurrentSeason == null)
            {
                return new ClassUserServiceResponse { isSucceded = false, message = "No Seasons added to this school yet, Please add season first and try again" };
            }
            else if (viewModel.SelectedTypeId == null || viewModel.SelectedTypeId == Guid.Empty)
            {
                return new ClassUserServiceResponse { isSucceded = false, message = "You must select user type" };
            }
            else
            {
                foreach (UsersModel user in viewModel.AllUsers.Where(u => u.IsSelected))
                {
                    ClassUser _classUser = new ClassUser();
                    _classUser.UserId = user.Id;
                    _classUser.UserTypeId = viewModel.SelectedTypeId;
                    _classUser.ClassId = viewModel.Class.Id;
                    _classUser.SeasonId = schoolCurrentSeason.Id;

                    await _unitOfWork.ClassUserRepository.AddAsync(_classUser);
                }
                await _unitOfWork.SaveAsync();

                return new ClassUserServiceResponse { isSucceded = true, message = "Users added successfully" };
            }
        }

        public async Task<ClassUserServiceResponse> DeleteUsers(ClassUserVM viewModel)
        {
            foreach (ClassUsersModel classUser in viewModel.ClassUsers.Where(u => u.User.IsSelected))
            {
                await _unitOfWork.ClassUserRepository.DeleteAsync(await _unitOfWork.ClassUserRepository.GetOneAsync(cu => cu.UserId == classUser.User.Id
                                                                    && cu.ClassId == viewModel.Class.Id
                                                                    && cu.SeasonId == classUser.SeasonId
                                                                    && cu.UserTypeId == classUser.UserTypeId));
            }
            await _unitOfWork.ClassUserRepository.SaveAsync();

            return new ClassUserServiceResponse { isSucceded = true, message = "Users added successfully" };
        }
    }

    public class ClassUserServiceResponse
    {
        public bool isSucceded { get; set; }
        public string message { get; set; }
    }
}
