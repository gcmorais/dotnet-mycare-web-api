﻿namespace MyCare.Communication.Requests
{
    public class RequestBond
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}