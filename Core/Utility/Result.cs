﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    public class Result
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; }
        public string UserMessage { get; set; }

        public void SetError(string message, string userMessage)
        {
            this.Error = true;
            this.Message = message;
            this.UserMessage = userMessage;
        }
    }
}
