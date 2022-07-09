﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PropertiesListings.Helpers
{
    public class EmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser>where TUser: class
    {
        public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
                 IOptions<EmailConfirmationTokenProviderOptions> options,
                 ILogger<DataProtectorTokenProvider<TUser>> logger):base(dataProtectionProvider, options, logger) { }
    }

    public class EmailConfirmationTokenProviderOptions: DataProtectionTokenProviderOptions
    {
    }
}
