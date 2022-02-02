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
    public class SeasonService : ISeasonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SeasonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ActivateSeason(Guid seasonId)
        {
            Season season = await _unitOfWork.SeasonRepository.GetByIDAsync(seasonId);

            if (season == null) return false;

            season.Current = true;
            await _unitOfWork.SeasonRepository.UpdateAsync(season);

            Season currentSeason = await _unitOfWork.SeasonRepository.GetOneAsync(s => s.Current);
            if (currentSeason != null)
            {
                currentSeason.Current = false;
                await _unitOfWork.SeasonRepository.UpdateAsync(currentSeason);
            }

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Create(SeasonModel model)
        {
            Season season = _mapper.Map<Season>(model);
            season.CreationDate = DateTime.Now;
            season.Id = Guid.NewGuid();
            season.School = await _unitOfWork.SchoolRepository.GetOneAsync(s => s.Id == season.School.Id, "Seasons");

            if (!season.School.Seasons.Any(s => s.Current)) season.Current = true;

            await _unitOfWork.SeasonRepository.AddAsync(season);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Season season = await _unitOfWork.SeasonRepository.GetByIDAsync(arguments[0]);
            if (season == null) return false;

            await _unitOfWork.SeasonRepository.DeleteAsync(season);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(SeasonModel model)
        {
            Season season = _mapper.Map<Season>(model);
            season.School = await _unitOfWork.SchoolRepository.GetByIDAsync(season.School.Id);

            await _unitOfWork.SeasonRepository.UpdateAsync(season);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<SeasonVM> Initiate(params object[] arguments)
        {
            SeasonVM seasonViewModel = new SeasonVM();
            seasonViewModel.Seasons = _mapper.Map<List<SeasonModel>>(await _unitOfWork.SeasonRepository.GetAsync(a => a.School.Id == (Guid)arguments[0], o => o.OrderBy(a => a.CreationDate)) as List<Season>);
            seasonViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]));

            return seasonViewModel;
        }

        public async Task<SeasonVM> InitiateCreate(params object[] arguments)
        {
            SeasonVM activityViewModel = new SeasonVM();
            activityViewModel.Season = new SeasonModel();
            activityViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]));

            return activityViewModel;
        }

        public async Task<SeasonVM> InitiateEdit(params object[] arguments)
        {
            SeasonVM seasonViewModel = new SeasonVM();
            seasonViewModel.Season = _mapper.Map<SeasonModel>(await _unitOfWork.SeasonRepository.GetByIDAsync(arguments[0]));
            seasonViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[1]));

            return seasonViewModel;
        }
    }
}
