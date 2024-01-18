# pre-requisites

To build and pack the browser bundles, nodejs and npm are required on the developer machine.

- Install NodeJS >= 16 from https://nodejs.org/en
- Once installed, run `npm i --global gulp` in your console.

To compile the server project, .net 7 SDK is required for your target architecture. https://dotnet.microsoft.com/en-us/download/dotnet/7.0

Run `dotnet dev-certs https --trust` and click `ok` on the certificate dialog.

Once installed, run `dotnet run` inside the inner SelfServiceVSC folder.

# additional setup

This project requires the additional repositories to be cloned into the same directory as this repo. 

- https://github.com/AACWarranty/Logger.git
- https://github.com/AACWarranty/FastDB.git

# secrets

The various third party integrations require secret keys which can be set locally on a developer workstation.

```
dotnet user-secrets init
dotnet user-secrets set SCS_API_PASSWORD <password-goes-here>

dotnet user-secrets set SCS_API_PASSWORD hRHi32sxq
```

# helpful test data

<!-- over 36k miles, does not give nice results -->
2021 Toyota Camry 4T1C11AK0MU613764  odo 36016


2022 Ford F150 1FTFW1E55NKD57951 odo 16858
2023 Chevrolet Equinox 2GNAXXEV3L6210189 odo 23689

# production site

https://www.bestpriceprotection.com



