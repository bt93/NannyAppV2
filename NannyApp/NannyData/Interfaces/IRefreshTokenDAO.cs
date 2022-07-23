using NannyModels.Models.Authentication;

namespace NannyData.Interfaces
{
    public interface IRefreshTokenDAO
    {
        /// <summary>
        /// Gets a refresh token
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Refresh token info</returns>
        public RefreshToken GetRefreshToken(string token);

        /// <summary>
        /// Adds a new refresh token
        /// </summary>
        /// <param name="token">A refresh token</param>
        /// <returns>The token id</returns>
        public int AddRefreshToken(RefreshToken token);

        /// <summary>
        /// Updates the refresh token to is used
        /// </summary>
        /// <param name="tokenID">The tokens id</param>
        /// <returns>If successful</returns>
        public int SetRefreshTokenToIsUsed(int tokenID);
    }
}
