using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public interface IClientKeys
    {
        bool IsUniqueKeyAlreadyGenerate(int UserID);
        void GenerateUniqueKey(out string ClientID, out string ClientSecert);
        int SaveClientIDandClientSecert(ClientKey ClientKeys);
        int UpdateClientIDandClientSecert(ClientKey ClientKeys);
        ClientKey GetGenerateUniqueKeyByUserID(int UserID);
    }
}