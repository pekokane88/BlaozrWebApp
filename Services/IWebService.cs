using BlazorApp10.Models;

namespace BlazorApp10.Services
{
    public interface IWebService
    {
        Task<ItemValueVo[]> GetItemList();
        Task<bool> AddUpdateItem(ItemValueVo item);
        Task<bool> DeleteItem(ItemValueVo item);
    }
}
