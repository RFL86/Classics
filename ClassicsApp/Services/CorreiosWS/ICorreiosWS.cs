using ClassicsApp.ObjectValue;


namespace ClassicsApp.Services
{
    public interface ICorreiosWS
    {
        AddressModel GetAddress(string CEP);
    }
}
