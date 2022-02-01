using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IBaseService<TModel, TViewModel> where TModel : class
    {
        Task<TViewModel> Initiate(params object[] arguments);
        Task<TViewModel> InitiateCreate(params object[] arguments);
        Task<bool> Create(TModel model);
        Task<TViewModel> InitiateEdit(params object[] arguments);
        Task<bool> Edit(TModel model);
        Task<bool> Delete(params object[] arguments);
    }
}