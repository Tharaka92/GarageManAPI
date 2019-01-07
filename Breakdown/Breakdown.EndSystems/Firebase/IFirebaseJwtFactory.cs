using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.Firebase
{
    public interface IFirebaseJwtFactory
    {
        Task<string> GenerateToken(string userId, string firebaseServiceAccount);
    }
}
