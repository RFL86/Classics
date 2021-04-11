using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Helpers
{
    public class Sigep
    {
        public static SigepMaster.enderecoERP GetAddressByCEP(string CEP)
        {
            try
            {
                var ws = new SigepMaster.AtendeClienteClient();
                var address = ws.consultaCEPAsync(CEP).Result;

                if (address.@return != null)
                {
                    return new SigepMaster.enderecoERP()
                    {
                        end = address.@return.end,
                        uf = address.@return.uf,
                        bairro = address.@return.bairro,
                        cidade = address.@return.cidade,
                        cep = address.@return.cep
                    };
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("CEP NAO ENCONTRADO"))
                    return null;

                else
                {
                    SigepMaster.enderecoERP erro = new SigepMaster.enderecoERP();
                    erro.cep = "Error";
                    return erro;
                }

            }
        }
    }
}
