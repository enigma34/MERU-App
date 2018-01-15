using System;
using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public interface IAuthenticate
    {
        ClientKey GetClientKeysDetailsbyCLientIDandClientSecert(string clientID, string clientSecert);
        bool ValidateKeys(ClientKey ClientKeys);
        bool IsTokenAlreadyExists(string deviceId);
        int DeleteGenerateToken(string deviceId);
        int InsertToken(TokenManager token);
        string GenerateToken(ClientKey ClientKeys, DateTime IssuedOn);
    }
}