﻿
namespace DriveOfCity.IServices.IEmailService
{
    public interface IEmailService
    {
        public Task EnviarEmail(string destinatario, string tipo);
    }
}
