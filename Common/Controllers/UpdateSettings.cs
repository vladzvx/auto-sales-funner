using Common.Interfaces;
using Common.Services.PeriodicWorkers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController
    {
        private readonly CheckerSettings settings1;
        private readonly NewDealsSettings settings2;

        public SettingsController(CheckerSettings settings1, NewDealsSettings settings2)
        {
            this.settings1 = settings1;
            this.settings2 = settings2;
        }

        [HttpPost("update/checker")]
        public async Task<string> Update1(ISettings settings)
        {

            foreach (FieldInfo FI in settings.GetType().GetFields())
            {
                var field = this.settings1.GetType().GetFields().Where(item => item.Name == FI.Name && item.FieldType == FI.FieldType);
                if (field.Any())
                {
                    FI.SetValue(this.settings1, FI.GetValue(settings));
                }
            }
            return "ok";
        }

        [HttpPost("update/newdeals")]
        public async Task<string> Update2(ISettings settings)
        {

            foreach (FieldInfo FI in settings.GetType().GetFields())
            {
                var field = this.settings2.GetType().GetFields().Where(item => item.Name == FI.Name && item.FieldType == FI.FieldType);
                if (field.Any())
                {
                    FI.SetValue(this.settings2, FI.GetValue(settings));
                }
            }
            return "ok";
        }
    }
}
