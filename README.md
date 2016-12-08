# Contoso-Bank
##Business Problem
Consumer applications often require the ability to allow new users to sign-up for the service through a mobile application using Social Networking providers such as Facebook, Google and so on. Mobile applications generally communicate with its back-end services using APIs.

At the surface this sounds simple. However, there are in fact many services and technlogies required behind the scenes to ensure a seamless sign-up and sign-in user experience. These include Identity Providers such as Facebook, Google, Microsoft Azure Active Directory and others, protocols such as HTTPS, OAuth2, OpenID Connect, while not forgetting APIs (both your own and 3rd party APIs) and mobile platform-specific code (iOS, Android etc.). 

Fortunately Microsoft has put together a suite of tools and services to make integrating these services together easier.

##Solution Overview
The Contoso Bank application is an end-to-end solution involving a Mobile App that allows consumers to sign-up and then sign-in to the Contoso mobile application. Users can sign-up with an email address/password of their choosing (these are stored in Azure Active Directory B2C), or using any number of social network providers such as Google, Facebook, etc. 

Once signed-up, the consumer can sign-in to the mobile application using the identity provider used during sign-up. Authentication is accomplished using OpenID Connect and OAuth 2 over HTTPS. The benefit of using a 3rd party identity provider is that they (e.g. Microsoft, Facebook, Google etc.) are responsible for storing the consumer's credentials securely, thereby reducing Contoso's liability and responsibility for storing these. In fact, the Contoso Bank application never sees the consumer's username or password. The storage and authentication of usernames and passwords are completely the responsibility of the identity providers. 

Upon signing in to the mobile application, a bearer token is assigned behind-the-scenes to the consumer which allows the consumer to securely lookup their bank accounts and balances. This is information is retrieved from a RESTful API secured by Azure Active Directory B2C (or other identity providers). The Contoso Bank REST API receives the bearer token from the mobile application as part of the HTTPS request and uses that to ensure the consumer is authorized to view the data. Normally this information is stored in a database such as Azure SQL Database and secured using Row Level Security. But to keep things simple, the Contoso Bank API  stores this data in memory and does not require a database.

##Solution Components
###Mobile Application
- The mobile application was written in C# using Xamarin.Forms for a single code base supporting iOS, Android, and Windows UWP.
- A Windows 10 computer with Visual Studio 2015, Update 3 and Xamarin installed.
- A Mac OS X with XCode 8.1 and Xamarin Studio 6.1.2
- Microsoft Authentication Library (MSAL) library
- Microsoft Azure Active Directory B2C (same tenant use by the API)

###REST API
- The API was written in C# using ASP.NET WebAPI and ASP.NET MVC for the website hosting the API documentation.
- Azure App Service - API App
- Swagger (Swashbuckle Nuget Package)
- Microsoft Azure Active Directory B2C (same tenant use by the mobile application)

##//TODO
- Add token refresh code to mobile application.
- Add multi-tenancy to object model so user 1 does not see user 2's bank accounts.
- Add pages for CRUD operations to mobile application.
- Add MVVM and data bindings to mobile application.
