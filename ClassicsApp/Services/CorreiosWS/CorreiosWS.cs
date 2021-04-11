using ClassicsApp.Helpers;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public class CorreiosWS : ICorreiosWS
    {
        public AddressModel GetAddress(string CEP)
        {
            var address = Sigep.GetAddressByCEP(CEP);
            if (address != null)
            {
                if (address.cep != null && address.cep.Equals("Error"))
                {
                    return new AddressModel() { StatusWS = false };
                }
                else
                {
                    return new AddressModel()
                    {
                        AddressZipCode = address.cep,
                        AddressStateCode = address.uf,
                        AddressCity = address.cidade,
                        StatusWS = false
                    };
                }
            }
            else
                return new AddressModel() { StatusWS = false };
        }
    }
}
