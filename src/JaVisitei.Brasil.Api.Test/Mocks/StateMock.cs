using System.Collections.Generic;
using JaVisitei.Brasil.Business.ViewModels.Response.State;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class StateMock
    {
        public static List<StateResponse> ReturnStateListMock()
        {
            return new List<StateResponse> {
                ReturnState1Mock(),
                ReturnState2Mock(),
                ReturnState3Mock()
            };
        }

        public static StateResponse ReturnState1Mock()
        {
            return new StateResponse
            {
                Id = "sp_sao_paulo_estado",
                Name = "São Paulo",
                Canvas = "M3453MasC3534636456 546df3a6sd364346345",
                CountryId = "bra_brasil"
            };
        }

        public static StateResponse ReturnState2Mock()
        {
            return new StateResponse
            {
                Id = "pe_pernambuco_estado",
                Name = "Pernambuco",
                Canvas = "M3453MasC3534s636456 546df36sd364346345",
                CountryId = "bra_brasil"
            };
        }

        public static StateResponse ReturnState3Mock()
        {
            return new StateResponse
            {
                Id = "ac_acre_estado",
                Name = "Acre",
                Canvas = "M3453MasC3534636456 546df36sd3s64346345",
                CountryId = "bra_brasil"
            };
        }
    }
}
