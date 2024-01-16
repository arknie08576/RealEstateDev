using RealEstateDev.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.DataProviders
{
    public interface IRealEstateProvider
    {
        // SELECT
        List<string> GetUniqueName();
        List<RealEstate> GetSpecificColumns();
        string AnonymousClass();

        // ORDER BY
        public List<RealEstate> OrderByNames();
        public List<RealEstate> OrderByNamesAndValueDescending();
        public List<RealEstate> OrderByValue();

        //Where
        List<RealEstate> WhereStartsWith(string prefix);
        List<RealEstate> WhereNameIs(string author);

        // FIRST, LAST, SINGLE
        RealEstate FirstOrDefaultByNameWithDefault(string name);
        RealEstate? FirstByValue();
        RealEstate LastByName(string author);
        RealEstate SingleById(int id);
        RealEstate? SingleOrDefaultById(int id);

        // TAKE
        List<RealEstate> TakeRealEstates(int howMany);
        List<RealEstate> TakeRealEstates(Range range);
        List<RealEstate> TakeRealEstatesWhileRealiseDataAfter(DateTime date);

        // SKIP
        List<RealEstate> SkipRealEstates(int howMany);
        List<RealEstate> SkipRealEstatesWhileRealiseDataAfter(DateTime date);

        // DISTINCT
        List<string> DistinctAllNames();
        List<RealEstate> DistinctByName();

        // CHUNK
        List<RealEstate[]> ChunkRealEstates(int size);
    }
}
