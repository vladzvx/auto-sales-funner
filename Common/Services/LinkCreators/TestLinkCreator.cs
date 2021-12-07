using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    public class TestLinkCreator
    {
        public string Target { get; init; } = Options.Target;
        public string EntityTypeId { get; init; } = Options.EntityTypeId;
        public string StageId { get; init; } = Options.StageId;
        public string StageId2 { get; init; } = Options.StageId2;
        public string CategoryId { get; init; } = Options.CategoryId;
        public string Domain { get; init; } = Options.Domain;
    }
}
