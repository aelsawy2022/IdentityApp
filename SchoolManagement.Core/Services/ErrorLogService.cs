using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IRepository<Log> _errorLogRepository;
        private readonly IMapper _mapper;

        public ErrorLogService(IRepository<Log> errorLogRepository, IMapper mapper)
        {
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
        }

        public async Task<List<ErrorLogModel>> GetLogs(ErrorLogFilter filter)
        {
            Expression<Func<Log, bool>> _Expression = null;

            if (filter != null)
            {
                _Expression =
                (
                    x => (filter.TimeStamp != null ? x.TimeStamp.Date == filter.TimeStamp.Value.Date : true)
                );
            }

            List<Log> Logs = await _errorLogRepository.GetAsync(_Expression, o => o.OrderByDescending(al => al.TimeStamp), "", filter.MaxRows, (filter.CurrentPage - 1) * filter.MaxRows) as List<Log>;


            int count = await _errorLogRepository.GetCountAsync(_Expression);

            double pageCount = (double)((decimal)count / Convert.ToDecimal(filter.MaxRows));
            filter.PageCount = (int)Math.Ceiling(pageCount);
            filter.CurrentPage = filter.CurrentPage;

            return _mapper.Map<List<ErrorLogModel>>(Logs);
        }
    }
}
