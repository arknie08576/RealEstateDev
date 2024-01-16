using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstateDev.DataProviders
{
    public class RealEstateProvider : IRealEstateProvider
    {
        private readonly IRepository<RealEstate> _RealEstatesRepository;

        public RealEstateProvider(IRepository<RealEstate> realEstatesRepository)
        {
            _RealEstatesRepository = realEstatesRepository;
        }

        //SELECT
        public List<string> GetUniqueName()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Select(x => x.Name.ToString()).Distinct().ToList();
        }

        public List<RealEstate> GetSpecificColumns()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Select(RealEstate => new RealEstate
            {
                Id = RealEstate.Id,
                Area = RealEstate.Area,
                Name = RealEstate.Name,
                Value = RealEstate.Value
            }).ToList();
        }

        public string AnonymousClass()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            var list = RealEstates.Select(RealEstate => new
            {
                Identifier = RealEstate.Id,
                RealEstateName = RealEstate.Name,
                RealEstateValue = RealEstate.Value
            });
            StringBuilder sb = new StringBuilder(2048);
            foreach (var RealEstate in list)
            {
                sb.AppendLine($"Product ID: {RealEstate.Identifier}");
                sb.AppendLine($"    RealEstate Name: {RealEstate.RealEstateName}");
                sb.AppendLine($"    RealEstate Value: {RealEstate.RealEstateValue}");
            }

            return sb.ToString();
        }

        // ORDER BY
        public List<RealEstate> OrderByNames()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.OrderBy(x => x.Name).ToList();
        }
        public List<RealEstate> OrderByNamesAndValueDescending()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Value)
                .ToList();
        }

        public List<RealEstate> OrderByValue()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.OrderBy(x => x.Value).ToList();
        }

        // WHERE

        public List<RealEstate> WhereStartsWith(string prefix)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Where(x => x.Name.StartsWith(prefix)).ToList();
        }

        public List<RealEstate> WhereNameIs(string author)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Where(x => x.Name == author).ToList();
        }

        // FIRST, LAST, SINGLE
        public RealEstate FirstOrDefaultByNameWithDefault(string name)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .FirstOrDefault(
                x => x.Name == name,
                new RealEstate { Id = -1, Name = "NOT FOUND" });

        }

        public RealEstate? FirstByValue()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.OrderBy(x => x.Value).FirstOrDefault();
        }
        public RealEstate LastByName(string name)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Last(x => x.Name == name);
        }

        public RealEstate SingleById(int id)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Single(x => x.Id == id);
        }

        public RealEstate? SingleOrDefaultById(int id)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.SingleOrDefault(x => x.Id == id, 
                new RealEstate { Id = -1, Name = "NOT FOUND" });
        }

        // TAKE
        public List<RealEstate> TakeRealEstates(int howMany)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .OrderBy(x => x.Name)
                .Take(howMany)
                .ToList();
        }

        public List<RealEstate> TakeRealEstates(Range range)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .OrderBy(x => x.Name)
                .Take(range)
                .ToList();
        }

        public List<RealEstate> TakeRealEstatesWhileRealiseDataAfter(DateTime date)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .OrderByDescending(x => x.dateTime)
                .TakeWhile(x => x.dateTime >= date)
                .ToList();
        }

        // SKIP
        public List<RealEstate> SkipRealEstates(int howMany)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .OrderBy(x => x.Name)
                .Skip(howMany)
                .ToList();
        }

        public List<RealEstate> SkipRealEstatesWhileRealiseDataAfter(DateTime date)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
               .OrderByDescending(x => x.dateTime)
               .SkipWhile(x => x.dateTime >= date)
               .ToList();
        }

        // DISTINCT
        public List<string> DistinctAllNames()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
               .Select(x => x.Name.ToString())
               .Distinct()
               .ToList();
        }

        public List<RealEstate> DistinctByName()
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates
                .DistinctBy(x => x.Name)
                .OrderBy(X => X.Name)
                .ToList();
        }

        // CHUNK
        public List<RealEstate[]> ChunkRealEstates(int size)
        {
            var RealEstates = _RealEstatesRepository.GetAll();
            return RealEstates.Chunk(size).ToList();
        }
    }
}
