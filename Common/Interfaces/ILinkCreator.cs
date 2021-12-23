using Common.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ILinkCreator
    {
        public static LogWriter LogWriter;
        public string Create();
    }
}
