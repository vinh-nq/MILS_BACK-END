using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using MIS.Data.Providers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MIS.API
{/// <summary>
 /// 
 /// </summary>
    public class AuthorizationServiceProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change authentication ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                //Console.WriteLine(response.Content);
                var form = await context.Request.ReadFormAsync();
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                // Provider Accounts acc = new Accounts();
                //Authenticate the user credentials
                if (!string.IsNullOrEmpty(context.UserName))
                {
                    var userLog = new UserProvider().GetByUserName(context.UserName).Result;
                    if (userLog != null)
                    {
                        var user = new UserProvider().GetByUserNameAndPassword(context.UserName, context.Password).Result;
                        if (user != null)
                        {
                            var chucNangs = new PermissionProvider().GetByRoleId(user.RoleId.Value).Result;
                            identity.AddClaim(new Claim(ClaimTypes.Role, "ALL"));
                            foreach (var item in chucNangs)
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, item.FunctionCode));
                            }
                            identity.AddClaim(new Claim("username", context.UserName));
                            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                            var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {
                                    "UserName", context.UserName
                                },
                                {
                                    "UserId", user.UserId.ToString()
                                },
                                {
                                    "FullName", user.FullName
                                }
                            });
                            var ticket = new AuthenticationTicket(identity, props);
                            context.Validated(ticket);
                        }
                        else
                        {
                            context.SetError("invalid_grant", "Mật khẩu không chính xác");
                            return;
                        }
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Tài khoản không tồn tại");
                        return;
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "Tài khoản không được để trống");

                    return;
                }
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", "error code");
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                if (!property.Key.Equals(".issued") && !property.Key.Equals(".expires"))
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }
            }
            return Task.FromResult<object>(null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();
            // copy all properties and set the desired lifetime of refresh token  
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);

            _refreshTokens.TryAdd(guid, refreshTokenTicket);

            // consider storing only the hash of the handle  
            context.SetToken(guid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            // context.DeserializeTicket(context.Token);
            AuthenticationTicket ticket;
            string header = context.OwinContext.Request.Headers["Authorization"];

            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
        }
    }
}