using NannyModels.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NannyData.Interfaces
{
    public interface IRefreshTokenDAO
    {
        /// <summary>
        /// Adds a new refresh token
        /// </summary>
        /// <param name="token">A refresh token</param>
        /// <returns>The token id</returns>
        public int AddRefreshToken(RefreshToken token);
    }
}
