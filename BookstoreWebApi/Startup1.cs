using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(BookstoreWebApi.Startup1))]

namespace BookstoreWebApi
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider()
            };

            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
