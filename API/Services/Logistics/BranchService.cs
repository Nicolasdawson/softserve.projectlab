using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models;
using API.Services.Logistics;

namespace API.Services.Logistics
{
    public class BranchService : IBranchService
    {
        private readonly IBranch _branch;

        public BranchService(IBranch branch)
        {
            _branch = branch;
        }




    }
}
