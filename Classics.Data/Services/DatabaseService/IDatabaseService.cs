using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Services.DatabaseService
{
    public interface IDatabaseService
    {
        System.Data.DataTable ConvertToInternalTable<T>(List<T> listaParaConverter, string nomeDaTabela);
        string GetTypeName(string text);
    }
}
