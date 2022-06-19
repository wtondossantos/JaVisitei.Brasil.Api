using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class VisitMock
    {
        public static VisitKeyRequest VisitKeyRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                UserId = 1
            };
        }

        public static InsertVisitRequest CreateVisitRequestMock()
        {
            return new InsertVisitRequest
            {
                 Color = "200,100,50",
                 VisitationDate = "02-07-2022",
                 RegionTypeId = 6,
                 RegionId = "ce_aquiraz",
                 UserId = 1
            };
        }

        public static VisitValidator CreatedVisitResponseMock()
        {
            return new VisitValidator
            {
                Data = new VisitResponse
                {
                    UserId = 1,
                    RegionTypeId = 6,
                    RegionId = "ce_aquiraz",
                    Color = "200,100,50",
                    VisitDate = Convert.ToDateTime("02-07-2022"),
                    RegistryDate = DateTime.Now
                },
                Message = "sucess"
            };
        }

        public static VisitValidator DeletedVisitResponseMock()
        {
            return new VisitValidator
            {
                Data = new VisitResponse
                {
                    UserId = 1
                },
                Message = "sucess deletion"
            };
        }

        public static VisitValidator VisitValidatorErrorMock()
        {
            return new VisitValidator
            {
                Errors = new List<string> {
                    "Invalid return"
                }
            };
        }

        public static IEnumerable<VisitResponse> ReturnVisitListMock()
        {
            return new List<VisitResponse> {
                ReturnVisitMunicipalityMock(),
                ReturnVisitIslandMock()
            };
        }

        public static VisitResponse ReturnVisitMunicipalityMock()
        {
            return new VisitResponse
            {
                UserId = 1,
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                Color = "200,100,50",
                VisitDate = Convert.ToDateTime("30-07-2022"),
                RegistryDate = DateTime.Now
            };
        }

        public static VisitResponse ReturnVisitIslandMock()
        {
            return new VisitResponse
            {
                UserId = 1,
                RegionTypeId = 7,
                RegionId = "pe_ilha_de_sao_pedro_e_sao_paulo",
                Color = "200,150,50",
                VisitDate = Convert.ToDateTime("30-07-2022"),
                RegistryDate = DateTime.Now
            };
        }
    }
}
