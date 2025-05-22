using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
using API.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests;

public class UserTesting
{
    private readonly IConfiguration _configuration;
    private readonly IPendingRegistrationService _pendingRegistrationService;    
    private readonly EmailService _emailService;
    public UserTesting()
    {

    }

    [Fact]
    public async Task PostNewUser_EmailAccount_IsNotValid()
    {

    }
}
