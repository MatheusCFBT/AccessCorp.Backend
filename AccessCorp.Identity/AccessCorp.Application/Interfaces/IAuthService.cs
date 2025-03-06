﻿using AccessCorp.Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorp.Application.Interfaces;

public interface IAuthService
{
    Task<Result> LoginAdministrator(AdministratorLoginVM request);
    Task<Result> RegisterAdministrator(AdministratorRegisterVM request);
    Task<Result> LoginDoorman(DoormanLoginVM request);

}