using JaVisitei.Brasil.Business.ViewModels.Response.RegionType;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class RegionTypeMock
    {
        public static IEnumerable<RegionTypeResponse> ReturnRegionTypeListMock()
        {
            return new List<RegionTypeResponse> {
                ReturnRegionType1Mock(),
                ReturnRegionType2Mock(),
                ReturnRegionType3Mock()
            };
        }

        public static RegionTypeResponse ReturnRegionType1Mock()
        {
            return new RegionTypeResponse
            {
                Id = 1,
                Name = "Pais"
            };
        }

        public static RegionTypeResponse ReturnRegionType2Mock()
        {
            return new RegionTypeResponse
            {
                Id = 2,
                Name = "Estado"
            };
        }

        public static RegionTypeResponse ReturnRegionType3Mock()
        {
            return new RegionTypeResponse
            {
                Id = 6,
                Name = "Município"
            };
        }
    }
}
