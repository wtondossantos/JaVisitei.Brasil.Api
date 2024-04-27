using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class IslandMock
    {
        public static IEnumerable<IslandResponse> ReturnIslandListMock()
        {
            return new List<IslandResponse> {
                ReturnIsland1Mock(),
                ReturnIsland2Mock(),
                ReturnIsland3Mock()
            };
        }

        public static IslandResponse ReturnIsland1Mock()
        {
            return new IslandResponse
            {
                Id = "ba_ilha_redonda",
                Name = "Ilha Redonda",
                ArchipelagoId = "ba_arquipelago_de_abrolhos_ilha",
                Canvas = "M3453MasC3534636456 546dssf36sd364346345"
            };
        }
        
        public static IslandResponse ReturnIsland2Mock()
        {
            return new IslandResponse
            {
                Id = "ba_ilha_santa_barbara",
                Name = "Ilha Santa Bárbara",
                ArchipelagoId = "ba_arquipelago_de_abrolhos_ilha",
                Canvas = "M3453MasC3534s636456 546df36sd364346345"
            };
        }

        public static IslandResponse ReturnIsland3Mock()
        {
            return new IslandResponse
            {
                Id = "pe_ilha_de_sao_pedro_e_sao_paulo",
                Name = "Arquipélago de São Pedro e São Paulo",
                ArchipelagoId = "pe_arquipelago_de_sao_pedro_e_sao_paulo_ilha",
                Canvas = "M3453MasC3534636456 54d6df36sd364346345"
            };
        }
    }
}
