using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class CountryMock
    {
        public static IEnumerable<CountryResponse> ReturnCountryListMock()
        {
            return new List<CountryResponse> {
                ReturnCountry1Mock(),
                ReturnCountry2Mock(),
                ReturnCountry3Mock()
            };
        }

        public static CountryResponse ReturnCountry1Mock()
        {
            return new CountryResponse
            {
                Id = "bra_brasil",
                Name = "Brasil"
            };
        }

        public static CountryResponse ReturnCountry2Mock()
        {
            return new CountryResponse
            {
                Id = "eua_estados_unidos",
                Name = "Estados Unidos da América"
            };
        }

        public static CountryResponse ReturnCountry3Mock()
        {
            return new CountryResponse
            {
                Id = "ita_italia",
                Name = "Itália"
            };
        }
    }
}
