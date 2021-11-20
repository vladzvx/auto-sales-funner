using Common.Interfaces;
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
        private readonly ISettings settings;

        public SettingsController(ISettings settings)
        {
            this.settings = settings;
        }

        [HttpPost("update")]
        public async Task<string> Update(ISettings settings)
        {

            foreach (FieldInfo FI in settings.GetType().GetFields())
            {
                var field = this.settings.GetType().GetFields().Where(item => item.Name == FI.Name && item.FieldType == FI.FieldType);
                if (field.Any())
                {
                    FI.SetValue(this.settings, FI.GetValue(this.settings));
                }
            }
            return "ok";
        }

        [HttpPost("get")]
        public async Task<string> Get()
        {
            return JsonSerializer.Serialize(settings);
        }
    }
}
