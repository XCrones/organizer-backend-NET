using Microsoft.AspNetCore.Mvc;

namespace organizer_backend_NET.Interfaces.IControllers
{
    public interface IBase_Contorller<VM>
    {
        public Task<IActionResult> GetAll();

        public Task<IActionResult> GetOne(int id);

        public Task<IActionResult> Create(VM model);

        public Task<IActionResult> Save(int id, VM todo);

        public Task<IActionResult> Remove(int id);

        public Task<IActionResult> Restore(int id);
    }
}
