﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Handle.Email
{
    public interface IEmailService
    {
        string SendEmail(Request_Message message);
    }
}
