using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface ISeasonService : IBaseService<SeasonModel, SeasonViewModel>
    {
        Task<bool> ActivateSeason(Guid seasonId);
    }
}
