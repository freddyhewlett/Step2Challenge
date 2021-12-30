using AutoMapper;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Configuration
{
    public class MainController : Controller
    {
        protected readonly IMapper _mapper;
        protected readonly INotifierService _notifier;

        protected MainController(IMapper mapper, INotifierService notifier)
        {
            _mapper = mapper;
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            if (_notifier.HasError())
            {
                return false;
            }
            return true;
        }
    }
}
