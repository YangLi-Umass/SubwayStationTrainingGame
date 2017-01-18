using System;
using System.Collections.Generic;
using System.Linq;

namespace PERCEPT.Web.Data.Models.DTO
{
    public class DTO_BaseNavInstructionUnitTest
    {
        public long Id { get; set; }
        public int SourceLandmarkId { get; set; }
        public int DestinationLandmarkId { get; set; }
        public String BaseDirections { get; set; }
        public long BuildingId { get; set; }
        public long Distance { get; set; }
        public int InstructionType { get; set; }
    }
}