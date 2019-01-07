using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.Firebase
{
    public class FirebaseJwtFactory : IFirebaseJwtFactory
    {
        public async Task<string> GenerateToken(string userId, string firebaseServiceAccount)
        {
            try
            {
                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(firebaseServiceAccount)
                    });
                }

                return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
