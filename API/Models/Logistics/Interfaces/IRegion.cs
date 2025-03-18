namespace API.Models.Logistics.Interfaces
{
    public interface IRegion
    {
        int RegionId { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        Result<IRegion> AddRegion(IRegion region);
        Result<IRegion> UpdateRegion(IRegion region);
        Result<IRegion> GetRegionById(int regionId);
        Result<List<IRegion>> GetAllRegions();
        Result<bool> DeleteRegion(int regionId);
    }
}
