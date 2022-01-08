using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Data
{
    public class WebUIContext : IdentityDbContext
    {
        public WebUIContext(DbContextOptions<WebUIContext> options) : base(options)
        {
        }
    }
}
