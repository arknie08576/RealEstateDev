using RealEstateDev.Entities;

namespace TDev.DataProviders
{
    public interface IRealEstateProvider<T> where T : class, IEntity, new()
    {
        // SELECT
        List<string> GetUniqueName();

        // ORDER BY
        public List<T> OrderByNames();
        public List<T> OrderByValue();

        //Where
        List<T> WhereStartsWith(string prefix);
        List<T> WhereNameIs(string author);

        // FIRST, LAST, SINGLE
        T FirstOrDefaultByNameWithDefault(string name);
        T? FirstByValue();
        T LastByName(string author);
        T SingleById(int id);
        T? SingleOrDefaultById(int id);

        // TAKE
        List<T> TakeRealEstates(int howMany);
        List<T> TakeRealEstates(Range range);

        // DISTINCT
        List<string> DistinctAllNames();
        List<T> DistinctByName();

        // CHUNK
        List<T[]> ChunkRealEstates(int size);
    }
}
